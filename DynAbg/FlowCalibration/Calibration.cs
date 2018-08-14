using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using FlowCalibrationInterface;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text;
using FlowCalibration.tools;
using FlowCalibration.tcm;

namespace FlowCalibration
{
    //error event handler
    public delegate void ErrorEventHandler(CalibrationException exception);

    // calibration class
    public class Calibration : ICalibration
    {
        // maximum Fehler
        private const int allowedErrors = 3;

        // Tester
        public Thread allSyncThread { get; set; }

        //Teilung in Demo und nicht Demo
        private TCM tcm;
        private TCMEvent tcmEvent = new TCMEvent();

        public string valvedatapath { get; set; }

        private Thread checkThread;
        public ReaderWriterLock valvesLock = new ReaderWriterLock();

        public ReaderWriterLock modulesLock = new ReaderWriterLock();

        // Wenn demo ein Random Generator wird genutzt


        public event ErrorEventHandler errorOccurred;

        private BackgroundWorker worker;
        public List<Valve> valves;

        public List<Module> modules;

        public bool midnightrecalib { get; set; }

        public bool manualSoll { get; set; }

        public bool simplifyCheck { get; set; }



        // Konstruktor erstellet Calibration mit standardwerten 
        public Calibration()
        {
        }

        // Initialisator. Falls Bereits initialisiert, mache nichts. kontinuirlich abgefragt
        public void init()
        {
            if (Initialized)
                return;

            // Falls keine Demo, dann erstelle neues TCM und versuche zu connecten
            tcm = new TCM();

            tcm.connect();

            // Bei fehler eine Exception werfen
            if (!tcm.isConnected())
                throw new CalibrationException(Error.NO_TCM_FOUND, tcm.getLastErrorMessage());


            tcm.messageReceived += new MessageEventHandler(tcmMessageReceived);
            tcm.errorOccurred += new tcm.ErrorEventHandler(tcmError);

            // Liste von Valves
            valves = new List<Valve>();
            modules = new List<Module>();
            Initialized = true;         // Initialisierung auf true setzen

            //checkthread
            checkThread = new Thread(new ThreadStart(CheckThread));
            checkThread.Name = "Valve communication check";
            checkThread.Start();
        }

        // ueberprueft thread
        private void CheckThread()
        {
            // ueberprueft immer und immer wieder..
            // Solang Threading nicht tot
            while (Thread.CurrentThread.ThreadState != System.Threading.ThreadState.Aborted)
            {
                Thread.Sleep(30 * 1000);
                DateTime datetime = DateTime.Now;

                // Mit dem Valveobjekt kann erst nach dem lock gearbeitet werden,
                // da es sonst zu einer race condition kommen kann,
                // wenn mit dem GUI-Thread synchronisiert wird.


                valvesLock.AcquireReaderLock(-1);
                modulesLock.AcquireReaderLock(-1);

                // In try weil der Thread abgebrochen werden kann und dann eine Exception geworfen wird.
                try
                {
                    foreach (Valve valve in valves)
                        // Falls nicht antwortet, kommunikationsok auf falsch setzen
                        if (valve.dateTimeReceived.AddSeconds(60 * 12.1).Ticks < datetime.Ticks)
                            valve.KommunikationOK = false;
                    foreach (Module module in modules)
                    {
                        if (module.type == 0)
                        {
                            if (module.dateTimeReceived.AddSeconds(60 * 2.1).Ticks < datetime.Ticks)
                                module.KommunikationOK = false;
                        }
                        else //module von alteko
                        {
                            if (module.dateTimeReceived.AddSeconds(60 * 30.1).Ticks < datetime.Ticks)
                                module.KommunikationOK = false;
                        }
                    }
                }
                finally
                {
                    valvesLock.ReleaseReaderLock();
                    modulesLock.ReleaseReaderLock();
                }
            }
        }

        // Gibt zurrueck, ob valve am lesen einer Nachricht ist
        private static bool isValveLearnMessage(TCMMessage message)
        {
            return message.learnMessage && !message.regularData && message.orgByte == 0x07 && message.function == 0x20 && message.type == 0x01 && (message.manufacturer == 0x0A || message.manufacturer == 0x34);
        }

        private static bool isModuleLearnMessage(TCMMessage message)
        {
            return message.learnMessage && !message.regularData && message.orgByte == 0xD4 && message.function == 0x10 && message.type == 0x01 && message.manufacturer == 0x0A || message.learnMessage && !message.regularData && message.orgByte == 0x07 && message.function == 0x10 && message.type == 0x03 && message.manufacturer == 0x2D;
        }

        // uebrprueft gueltigkeit der nachricht
        private static bool isValveMessage(TCMMessage message, long id)
        {
            return !message.learnMessage && message.regularData && message.objectID == id;
        }

        private static bool isModuleMessage(TCMMessage message, long id)
        {
            return !message.learnMessage && message.regularData && message.objectID == id;
        }

        // erstelle naechste freie id, falls existent und erreichbar
        private int getNextID()
        {
            List<Valve> valves;

            //Kopie erzeugen und dann sortieren
            valves = new List<Valve>(this.valves);

            valves.Sort(delegate (Valve valve1, Valve valve2)
            {
                return valve1.FunctionID.CompareTo(valve2.FunctionID);
            });

            List<Module> modules;

            //Kopie erzeugen und dann sortieren
            modules = new List<Module>(this.modules);

            modules.Sort(delegate (Module module1, Module module2)
            {
                return module1.FunctionID.CompareTo(module2.FunctionID);
            });

            for (int i = 0; i < 127; i++)
            {
                bool exist = false;
                foreach (Valve valve in valves)
                {
                    if (valve.FunctionID == i)
                    {
                        exist = true;
                        break;
                    }
                }
                foreach (Module module in modules)
                {
                    if (module.FunctionID == i)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    return i;
                }
            }
            return -1;
        }

        private int getNextModuleID()
        {
            return getNextID();

        }

        // gibt zurueck ob es die valve mit der ID gibt
        private bool getValve(long valveID, out Valve valve)
        {
            valve = null;
            valvesLock.AcquireReaderLock(-1);

            foreach (Valve tmpValve in valves)
            {
                if (tmpValve.ValveID == valveID)
                {
                    valve = tmpValve;
                    break;
                }
            }

            valvesLock.ReleaseReaderLock();
            return valve != null;
        }

        private bool getModule(long valveID, out Module module)
        {
            module = null;
            modulesLock.AcquireReaderLock(-1);

            foreach (Module tmpModule in modules)
            {
                if (tmpModule.ModuleID == valveID)
                {
                    module = tmpModule;
                    break;
                }
            }

            modulesLock.ReleaseReaderLock();
            return module != null;
        }

        // eigentlich analog zu getvalve, hier keine referenzen
        private bool hasValve(long id)
        {
            bool hasValve = false;

            valvesLock.AcquireReaderLock(-1);

            foreach (Valve tmpValve in valves)
            {
                if (tmpValve.ValveID == id)
                {
                    hasValve = true;
                    break;
                }
            }

            valvesLock.ReleaseReaderLock();
            return hasValve;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string lastMessageData { get; set; }
        public string lastEnOceanID { get; set; }
        public DateTime lastTime { get; set; }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                //method(sender, argument)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // checkt ob die erhaltende message in ordnung ist und schreibt sie in die valve als info rein, sofern die valve angelernt ist
        private void tcmMessageReceived(TCMMessage message)
        {
            
            if (message.objectID.ToString("X8") != "00000000")
            {
                Debug.WriteLine(string.Format("Message received: {0} from ID: {1} um {2}", TCM.getHexString(message.message), message.objectID.ToString("X8"), message.datum_uhrzeit));
                lastMessageData = TCM.getHexString(message.message);
                lastEnOceanID = message.objectID.ToString("X8");
                lastTime = message.datum_uhrzeit;
                OnPropertyChanged("NewMSG");
            }
            Valve valve = null;
            Module module = null;

            lock (tcmEvent)
            {
                tcmEvent.notify(message);
            }

            if (message.learnMessage)
                return;

            if (!getValve(message.objectID, out valve) && !getModule(message.objectID, out module))
                return;

            if (valve != null)
            {
                if (valve.Learned)
                    sendAnswer(valve);



                if (valve.Learned)
                {
                    valve.Signal = message.signalStrength;
                    valve.Value = message.byte3;
                    valve.Temperature = Math.Round(((40D / 255D) * (double)message.byte1), 1);
                    valve.Battery = TCMMessage.readBits((byte)message.byte2, 3, 1) == 1;
                    valve.KommunikationOK = true;
                    valve.dateTimeReceived = message.datum_uhrzeit;
                    valve.WriteBool = !valve.WriteBool;
                }
            }

            bool sendalrdy = false;


            if (module != null)
            {
                module.Signal = message.signalStrength;
                module.KommunikationOK = true;
                module.dateTimeReceived = message.datum_uhrzeit;
                module.NewMSG = !module.NewMSG;
                if (module.type == 1)
                {
                    module.Temperature = 40 - (double)((40D / 255D) * (double)message.byte1);
                    module.SetPoint = (double)(8D + ((double)(30 - 8) / 255D) * (double)message.byte2);
                }

                // 1. Auslesen
                else if (module.Learned)
                {
                    switch (message.message[7])
                    {
                        // XxX0 = vollstaendige Nachricht XxX1 = unvollstaendig.
                        case 0x00: // General Message -> 2 Byte
                            module.generalmessage = message;
                            break;

                        case 0x20: // Data Message -> 8 Byte
                            if (module.datamessage == null)
                            {
                                //Initiator! WICHTIG

                                module.timeask = true;
                                module.configsend = true;
                                module.Intervall = 1;
                            }
                            module.datamessage = message;
                            //Speicherung von Informationen
                            if (!module.datasend)
                            {
                                module.Signal = message.signalStrength;
                                module.Temperature = Math.Round((double)((40D / 255D) * (double)message.byte0), 1);
                                module.SetPoint = Math.Round(((double)((40D / 255D) * (double)message.byte1)), 1);
                                module.KommunikationOK = true;
                                module.dateTimeReceived = message.datum_uhrzeit;
                                module.NewMSG = !module.NewMSG;
                            }
                            else
                            {
                                sendAnswer(module, module.datamessage);
                                Thread.Sleep(50);
                                sendalrdy = true;
                                module.datasend = false;
                                module.dataask = true;
                            }
                            break;

                        case 0x40: // Configuration Message -> 8 Byte
                            module.configmessage = message;
                            //Speicherung von Informationen
                            if (!module.configsend)
                            {
                                module.Intervall = Convert.ToInt32((Convert.ToString(message.byte5, 2).PadLeft(8, '0')).Substring(0, 6), 2);
                            }
                            else
                            {
                                sendAnswer(module, module.configmessage);
                                Thread.Sleep(50);
                                sendalrdy = true;
                                module.configsend = false;
                                module.configask = true;
                            }
                            break;
                        case 0x60: // Room Control Message -> 6 Byte
                            module.roommessage = message;
                            if (!module.roomsend)
                            {
                                module.EcoSetPoint = Math.Round(((double)((40D / 255D) * (double)message.byte2)), 1);
                                module.ComSetPoint = Math.Round(((double)((40D / 255D) * (double)message.byte1)), 1);
                            }
                            else
                            {
                                sendAnswer(module, module.roommessage);
                                Thread.Sleep(50);
                                sendalrdy = true;
                                module.roomsend = false;
                                module.roomask = true;
                            }
                            break;
                        case 0x81:
                            // Time Program Setup -> 6 Byte
                            module.timemessage = message;
                            sendalrdy = true;

                            //Speicherung von Informationen

                            if (!module.timesend)
                            {
                                if (module.timeask == true)
                                {
                                    module.clearTime();
                                    module.timeask = false;
                                }
                                byte emin = Convert.ToByte((Convert.ToString(message.byte4, 2).PadLeft(8, '0')).Substring(2, 6), 2);
                                byte estun = Convert.ToByte((Convert.ToString(message.byte3, 2).PadLeft(8, '0')).Substring(3, 5), 2);
                                byte smin = Convert.ToByte((Convert.ToString(message.byte2, 2).PadLeft(8, '0')).Substring(2, 6), 2);
                                byte sstun = Convert.ToByte((Convert.ToString(message.byte1, 2).PadLeft(8, '0')).Substring(3, 5), 2);
                                byte per = Convert.ToByte((Convert.ToString(message.byte0, 2).PadLeft(8, '0')).Substring(0, 4), 2);
                                byte mod = Convert.ToByte((Convert.ToString(message.byte0, 2).PadLeft(8, '0')).Substring(4, 2), 2);
                                byte del = Convert.ToByte((Convert.ToString(message.byte0, 2).PadLeft(8, '0')).Substring(7, 1), 2);
                                module.addTime(emin, estun, smin, sstun, per, mod, del);
                            }
                            break;
                        case 0x80: // Time Program Setup -> 6 Byte
                            module.timemessage = message;

                            if (!module.timesend)
                            {
                                if (module.timeask == true)
                                {
                                    module.clearTime();
                                    module.timeask = false;
                                }
                                //Speicherung von Informationen
                                byte emin2 = Convert.ToByte((Convert.ToString(message.byte4, 2).PadLeft(8, '0')).Substring(2, 6), 2);
                                byte estun2 = Convert.ToByte((Convert.ToString(message.byte3, 2).PadLeft(8, '0')).Substring(3, 5), 2);
                                byte smin2 = Convert.ToByte((Convert.ToString(message.byte2, 2).PadLeft(8, '0')).Substring(2, 6), 2);
                                byte sstun2 = Convert.ToByte((Convert.ToString(message.byte1, 2).PadLeft(8, '0')).Substring(3, 5), 2);
                                byte per2 = Convert.ToByte((Convert.ToString(message.byte0, 2).PadLeft(8, '0')).Substring(0, 4), 2);
                                byte mod2 = Convert.ToByte((Convert.ToString(message.byte0, 2).PadLeft(8, '0')).Substring(4, 2), 2);
                                byte del2 = Convert.ToByte((Convert.ToString(message.byte0, 2).PadLeft(8, '0')).Substring(7, 1), 2);
                                module.addTime(emin2, estun2, smin2, sstun2, per2, mod2, del2);
                                if (module.timedone == false)
                                {
                                    module.NewTime = !module.NewTime;
                                    module.timedone = true;
                                }
                                if (module.timeask2 == false)
                                {
                                    if (module.NewTime == true)
                                    {
                                        module.NewTime = false;
                                    }
                                    else
                                    {
                                        module.NewTime = true;
                                    }
                                    module.timeask2 = true;
                                }
                                else
                                {
                                    module.timeask = true;
                                }
                            }
                            else
                            {
                                // sendAnswer(module, module.timemessage); //loeschnachricht alles

                                //Thread.Sleep(50);
                                //sendalrdy = true;
                                module.incomplete = true;
                                module.timesend = false;
                                module.timeask = true;
                            }
                            break;
                    }
                    // 2. Erst nach Informationen fragen, wenn man will, dann genau eine Informationen senden.
                    if (sendalrdy == true)
                    {

                    }
                    else if (module.incomplete)
                    {
                        if (module.timeprogsend.Count != 0)
                        {
                            if (module.timeprogsend.Count == 1 || module.timeprogsend[1].deletion == 1)
                            {
                                // letzte nachricht des tages
                                sendAnswerTime(module, module.timemessage, module.timeprogsend[0].eMinute, module.timeprogsend[0].eStunde, module.timeprogsend[0].sMinute, module.timeprogsend[0].sStunde, module.timeprogsend[0].period, module.timeprogsend[0].mode, module.timeprogsend[0].deletion, true);
                                module.timeprogsend.RemoveAt(0);
                            }
                            else
                            {
                                // Nachrichten senden
                                sendAnswerTime(module, module.timemessage, module.timeprogsend[0].eMinute, module.timeprogsend[0].eStunde, module.timeprogsend[0].sMinute, module.timeprogsend[0].sStunde, module.timeprogsend[0].period, module.timeprogsend[0].mode, module.timeprogsend[0].deletion, false);
                                module.timeprogsend.RemoveAt(0);
                            }

                            if (module.timeprogsend.Count == 0)
                            {
                                module.timeprogsend.Clear();
                                module.incomplete = false;
                            }
                        }
                        else
                        {
                            // Der Fall sollte i.d.R. nicht auftreten
                            askAnswer(module, 0);
                            module.incomplete = false;
                        }
                    }
                    else if (module.dataask)
                    {
                        askAnswer(module, 1);
                        module.dataask = false;
                    }
                    else if (module.configask)
                    {
                        askAnswer(module, 2);
                        module.configask = false;
                    }
                    else if (module.roomask)
                    {
                        askAnswer(module, 3);
                        module.roomask = false;
                    }
                    else if (module.timeask)
                    {
                        askAnswer(module, 4);
                        module.timeask2 = false;

                    }
                    else if (module.datasend) //basically what we do is just resending the message. for most accuracy you should always make a request for information before you send.
                    {
                        askAnswer(module, 1);
                    }
                    else if (module.configsend)
                    {
                        askAnswer(module, 2);
                    }
                    else if (module.roomsend)
                    {
                        askAnswer(module, 3);
                    }
                    else if (module.timesend)
                    {
                        askAnswer(module, 4);
                    }
                    else
                    {
                        askAnswer(module, 0);
                    }
                }
            }
        }


        public void pubsendAnswer(IValve valve)
        {
            sendAnswer(valve as Valve);
        }

        // Sendet signal an valve
        private void sendAnswer(Valve valve)
        {
            long id = tcm.getID() + valve.FunctionID;
            byte[] data = new byte[] { 0xA5, 0x5A, 0x6B, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            //data[04] = 0x00;// (byte)(valve.Command == COMMAND.OPEN ? 0x64 : 0x00);
            data[05] = 0x00;//valve.travel; //hier soll später mal der Hub eingetragen werden, abhängig von Bit sf in Byte1 (1 = service ==> dann Hub übertragbar)
            if (valve.TempMode == 1)
            {
                //data[06] = Convert.ToByte(String.Format("00000101"), 2);
                data[04] = Convert.ToByte(String.Format(Convert.ToString((long)Math.Round((double)valve.DesiredValue / ((double)40 / (double)255)), 2)), 2);
                data[06] = Convert.ToByte(String.Format("00000101"), 2);
            }
            else
            {
                data[04] = Convert.ToByte(String.Format(Convert.ToString((long)valve.DesiredValue, 2)), 2);
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute <= 10 && DateTime.Now.Minute >= 0 && midnightrecalib)
                {
                    data[06] = Convert.ToByte(String.Format("11000001"), 2);
                }
                else
                {
                    data[06] = Convert.ToByte(String.Format("00000000"), 2);
                }
            }
            //data[06] = Convert.ToByte(String.Format("00{0}{1}0001", valve.Command == VALVE_COMMAND.OPEN ? "1" : "0", valve.Command == VALVE_COMMAND.OPEN ? "0" : "1"), 2);// sf ist hier immer gesetzt
            //TODO neu
            //Das Poweroff spielt sich auf bit0 ab. Aktor erhält Poweroff, fährt noch Ventil; Nach Abschluss der
            //der gewünschten Stellung für das Ventil wird Poweroff durchgeführt
            //data[07] = Convert.ToByte(String.Format("0000100{0}", valve.power_off == true ? "1" : "0"), 2);
            data[07] = Convert.ToByte(String.Format("00001000"), 2);
            //TODO raus     
            //data[07] = Convert.ToByte(String.Format("0000100{0}", valve.Power == VALVE_POWER.POWER_OFF ? "1" :"0"));
            //data[07] = 0x08;

            data[08] = (byte)(id >> 24);
            data[09] = (byte)(id >> 16);
            data[10] = (byte)(id >> 8);
            data[11] = (byte)(id);
            data = Tools.setChecksum(data); //array erhöhen und letztes als Summe 
            bool versendet = tcm.writeCommandModule(data, valve.ValveID); 

            Debug.WriteLine(string.Format("Message Sending: {0}", TCM.getHexString(data)));

            //TODO neu bei Poweroff sich den boolean von writeCommmand
            if (valve.power_off == true)
                valve.power_off_transmitted = versendet;



        }

        private void sendAnswer(Module module, TCMMessage message)
        {
            long id = tcm.getID() + module.FunctionID;
            byte[] data = new byte[3 + message.message[2]];
            Array.Copy(message.message, 3, data, 0, data.Length);
            StringBuilder strBuildernex = new StringBuilder(Convert.ToString(message.message[7], 2).PadLeft(8, '0'));
            strBuildernex[6] = '0';
            strBuildernex[7] = '1';
            data[4] = Convert.ToByte(strBuildernex.ToString(), 2);

            switch (message.message[7])
            {
                case 0x20: // Data Message -> 8 Byte
                    Debug.Write(string.Format("Message Sending Data Module: "));
                    StringBuilder strBuilder = new StringBuilder(Convert.ToString(data[data.Length - 8], 2).PadLeft(8, '0'));
                    strBuilder[6] = '1';
                    data[data.Length - 8] = Convert.ToByte(String.Format(strBuilder.ToString()), 2);
                    data[data.Length - 7] = Convert.ToByte(String.Format(Convert.ToString((long)Math.Round((double)module.SetPoint / ((double)40 / (double)255)), 2)), 2); //set point
                    // data[data.Length - 6] = 0; // room temperature
                    break;

                case 0x40: // Configuration Message -> 8 Byte
                    Debug.Write(string.Format("Message Sending Config Module: "));
                    StringBuilder strBuilder2 = new StringBuilder(Convert.ToString(module.Intervall, 2));
                    strBuilder2.Append('1');
                    strBuilder2.Append('0');
                    string str = strBuilder2.ToString();
                    data[6] = Convert.ToByte(String.Format(str), 2);
                    StringBuilder strBuilder3 = new StringBuilder(Convert.ToString(data[data.Length - 6], 2).PadLeft(8, '0'));
                    strBuilder3[7] = '0';
                    data[data.Length - 6] = Convert.ToByte(String.Format(strBuilder3.ToString()), 2);
                    break;

                case 0x60: // Room Control Message -> 6 Byte
                    Debug.Write(string.Format("Message Sending Room Module: "));
                    data[data.Length - 8] = Convert.ToByte(String.Format(Convert.ToString((long)(Math.Round(module.EcoSetPoint / ((double)40 / (double)255)))), 2));
                    data[data.Length - 7] = Convert.ToByte(String.Format(Convert.ToString((long)Math.Round(module.ComSetPoint / ((double)40 / (double)255))), 2));
                    StringBuilder strBuilder4 = new StringBuilder(Convert.ToString(data[data.Length - 6], 2).PadLeft(8, '0'));
                    strBuilder4[6] = '1';
                    strBuilder4[7] = '1';
                    data[data.Length - 6] = Convert.ToByte(String.Format(strBuilder4.ToString()), 2);
                    break;
                case 0x80: // Time Program Setup -> 6 Byte
                    Debug.Write(string.Format("Message Sending Time Module: "));
                    StringBuilder strBuilderT = new StringBuilder(Convert.ToString(data[data.Length - 6], 2).PadLeft(8, '0'));
                    strBuilderT[7] = '1';
                    strBuilderT[0] = '0';
                    strBuilderT[1] = '0';
                    strBuilderT[2] = '0';
                    strBuilderT[3] = '0';
                    data[data.Length - 10] = Convert.ToByte(String.Format("00000000"), 2);
                    data[data.Length - 9] = Convert.ToByte(String.Format("00000000"), 2);
                    data[data.Length - 8] = Convert.ToByte(String.Format("00000000"), 2);
                    data[data.Length - 7] = Convert.ToByte(String.Format("00000000"), 2);
                    data[data.Length - 6] = Convert.ToByte(String.Format(strBuilderT.ToString()), 2);
                    strBuilderT = new StringBuilder(Convert.ToString(data[data.Length - 11], 2).PadLeft(8, '0'));
                    strBuilderT[7] = '1';
                    data[data.Length - 11] = Convert.ToByte(String.Format(strBuilderT.ToString()), 2);
                    break;
            }

            data[data.Length - 5] = (byte)(id >> 24);
            data[data.Length - 4] = (byte)(id >> 16);
            data[data.Length - 3] = (byte)(id >> 8);
            data[data.Length - 2] = (byte)(id);
            data = Tools.setChecksum(data); //array erhöhen und letztes als Summe 
            bool versendet = tcm.writeCommandModule(data, module.ModuleID);

            Debug.WriteLine(string.Format("{0}", TCM.getHexString(data)));
        }

        private void sendAnswerTime(Module module, TCMMessage message, byte emin, byte ehour, byte smin, byte shour, byte per, byte mod, byte del, bool end)
        {
            long id = tcm.getID() + module.FunctionID;
            byte[] data = new byte[3 + message.message[2]];
            Array.Copy(message.message, 3, data, 0, data.Length);

            StringBuilder strBuilderT = new StringBuilder(Convert.ToString(data[data.Length - 6], 2).PadLeft(8, '0'));
            if (del == (byte)0)
            {
                strBuilderT[7] = '0';
            }
            else
            {
                strBuilderT[7] = '1';
            }
            strBuilderT[0] = '0';
            strBuilderT[1] = '0';
            strBuilderT[2] = '0';
            strBuilderT[3] = '0';
            switch (per)
            {
                case 0:
                    break;
                case 1:
                    strBuilderT[3] = '1';
                    break;
                case 2:
                    strBuilderT[2] = '1';
                    break;
                case 3:
                    strBuilderT[2] = '1';
                    strBuilderT[3] = '1';
                    break;
                case 4:
                    strBuilderT[1] = '1';
                    break;
                case 5:
                    strBuilderT[1] = '1';
                    strBuilderT[3] = '1';
                    break;
                case 6:
                    strBuilderT[1] = '1';
                    strBuilderT[2] = '1';
                    break;
                case 7:
                    strBuilderT[1] = '1';
                    strBuilderT[2] = '1';
                    strBuilderT[3] = '1';
                    break;
                case 8:
                    strBuilderT[0] = '1';
                    break;
                case 9:
                    strBuilderT[0] = '1';
                    strBuilderT[3] = '1';
                    break;
                case 10:
                    strBuilderT[0] = '1';
                    strBuilderT[2] = '1';
                    break;
                case 11:
                    strBuilderT[0] = '1';
                    strBuilderT[2] = '1';
                    strBuilderT[3] = '1';
                    break;
                case 12:
                    strBuilderT[0] = '1';
                    strBuilderT[1] = '1';
                    break;
                case 13:
                    strBuilderT[0] = '1';
                    strBuilderT[1] = '1';
                    strBuilderT[3] = '1';
                    break;
                case 14:
                    strBuilderT[0] = '1';
                    strBuilderT[1] = '1';
                    strBuilderT[2] = '1';
                    break;
                case 15:
                    strBuilderT[0] = '1';
                    strBuilderT[1] = '1';
                    strBuilderT[2] = '1';
                    strBuilderT[3] = '1';
                    break;
            }
            data[data.Length - 6] = Convert.ToByte(String.Format(strBuilderT.ToString()), 2);


            data[data.Length - 10] = emin; //emin
            data[data.Length - 9] = ehour; //ehour
            data[data.Length - 8] = smin; //smin
            data[data.Length - 7] = shour; //shour



            strBuilderT = new StringBuilder(Convert.ToString(data[data.Length - 11], 2).PadLeft(8, '0'));
            if (end == false)
            {
                strBuilderT[7] = '1';
            }
            else
            {
                strBuilderT[7] = '0';
            }
            data[data.Length - 11] = Convert.ToByte(String.Format(strBuilderT.ToString()), 2);


            data[data.Length - 5] = (byte)(id >> 24);
            data[data.Length - 4] = (byte)(id >> 16);
            data[data.Length - 3] = (byte)(id >> 8);
            data[data.Length - 2] = (byte)(id);
            data = Tools.setChecksum(data); //array erhöhen und letztes als Summe 
            bool versendet = tcm.writeCommandModule(data, module.ModuleID);

            Debug.WriteLine(string.Format("Message Sending Module: {0}", TCM.getHexString(data)));
        }

        private void askAnswer(Module module, int messagetype)
        {
            byte[] data = new byte[] { 0xD4, 0x5A, 0x6B, 0xD2, 1, 2, 0x00, 0x00, 0x00, 0x00, 0x00 };
            long id = tcm.getID();
            id += module.FunctionID;

            data[4] = Convert.ToByte(String.Format("00000000"), 2);
            switch (messagetype)
            {
                case 0: //Acknowledge Request
                    data[5] = Convert.ToByte(String.Format("00000000"), 2);
                    Debug.Write("Nachricht erhalten und wird anerkannt:");
                    break;
                case 1: //Data Request
                    data[5] = Convert.ToByte(String.Format("00001001"), 2);
                    Debug.Write("Data Ask: ");
                    break;
                case 2: //Configuration Request
                    data[5] = Convert.ToByte(String.Format("00010001"), 2);
                    Debug.Write("Configuration Ask: ");
                    break;
                case 3: //Room control setup request
                    data[5] = Convert.ToByte(String.Format("00011001"), 2);
                    Debug.Write("Room Ask: ");
                    break;
                case 4: //timeprogram request
                    data[5] = Convert.ToByte(String.Format("00100001"), 2);
                    Debug.Write("Time Programm Ask: ");
                    break;
            }
            data[6] = (byte)(id >> 24);
            data[7] = (byte)(id >> 16);
            data[8] = (byte)(id >> 8);
            data[9] = (byte)(id);
            data = Tools.setChecksum(data);

            tcm.writeCommandModule(data, module.ModuleID);
            Debug.WriteLine(string.Format("{0}", TCM.getHexString(data)));
        }

        // Exception error falls tcm fehler
        private void tcmError(string errorMessage)
        {
            Trace.WriteLine(string.Format("TCM error: {0}", errorMessage));

            if (errorOccurred != null)
                errorOccurred(new CalibrationException(Error.TCM, errorMessage));
        }

        #region ICalibrate

        public bool Initialized { get; private set; }

        public long getTCMID()
        {
            if (!Initialized)
                throw new CalibrationException(Error.NOT_INITIALIZED);


            return tcm.getID();
        }

        // versucht alles zu schließen angefangen von den ventilen bis zum tcm modul
        public void close()
        {
            if (!Initialized)
                return;

            //TODO Ventil ausschalten
            //TOOD neu
            //Dient zur Überwachung was schon versendet worden ist
            // bool[] sendedPowerOff = new bool[valves.Count];


            //Bit umschalten, der Aktor empfängt dann als nächstes die Ausschaltfunktion

            valvesLock.AcquireReaderLock(-1);

            foreach (Valve valve in valves)
                valve.power_off = true;

            valvesLock.ReleaseReaderLock();

            bool powerOff = true;

            DateTime startTime = DateTime.Now;

            do
            {
                //Weiß nicht ob das so toll ist, aber irgendwie muss gewartet werden
                //damit man eine Chance erhält auf den Aktor zu antworten
                powerOff = true;

                //Vielleicht hier alles Ventile einzeln rauswerfen, die bereits sich schon "abgemeldet" haben
                valvesLock.AcquireReaderLock(-1);

                foreach (Valve valve in valves)
                    powerOff &= valve.power_off_transmitted;

                valvesLock.ReleaseReaderLock();

                Thread.Sleep(100);
            }
            while (!powerOff && startTime.AddSeconds(2).Ticks > DateTime.Now.Ticks);

            try { tcm.disconnect(); } catch (Exception) { }

            valvesLock.AcquireWriterLock(-1);
            valves.Clear();
            valvesLock.ReleaseWriterLock();

            modulesLock.AcquireWriterLock(-1);
            modules.Clear();
            modulesLock.ReleaseWriterLock();

            checkThread.Abort();
            if (allSyncThread != null)
            {
                allSyncThread.Abort();
            }
            Initialized = false;
        }


        // ueberprueft zunaechst ob es eine Valve ist, falls ja dann oeffne valve
        public void openValve(IValve valve)
        {
            if (!(valve is Valve))
                throw new CalibrationException(Error.UNKNOWN_VALVE_OBJECT);

            (valve as Valve).Command = VALVE_COMMAND.OPEN;
        }

        // ueberprueft zunaechst ob es eine Valve ist, falls ja dann schliesse valve
        public void closeValve(IValve valve)
        {
            if (!(valve is Valve))
                throw new CalibrationException(Error.UNKNOWN_VALVE_OBJECT);

        }

        //aendert Valve Temperatur

        public void changeDesiredValue(IValve valve, int des)
        {
            if (!(valve is Valve))
                throw new CalibrationException(Error.UNKNOWN_VALVE_OBJECT);

            (valve as Valve).DesiredValue = des;
        }

        public void changeTempMode(IValve valve, int des)
        {
            if (!(valve is Valve))
                throw new CalibrationException(Error.UNKNOWN_VALVE_OBJECT);

            (valve as Valve).TempMode = des;
        }



        // sets valve turn speed
        public void setValveTravel(IValve valve, double travel)
        {
            if (!(valve is Valve))
                throw new CalibrationException(Error.UNKNOWN_VALVE_OBJECT);

            travel = Math.Max(Math.Min(travel, 4), 1);
            (valve as Valve).travel = (byte)(travel * 10);
        }

        public void deactivateModule(IModule module)
        {
            modulesLock.AcquireWriterLock(-1);
            modules.Remove((module as Module));
            modulesLock.ReleaseWriterLock();
        }

        // valve deaktivieren und dann aus liste entfernen
        public void deactivateValve(IValve valve)
        {

            if (!(valve is Valve))
                throw new CalibrationException(Error.UNKNOWN_VALVE_OBJECT);

            //TODO neu Ventil ausschalten
            //bool für poweroff setzen und mal schauen was passiert
            (valve as Valve).power_off = true;
            //jetzt abwarten ob das Signal versendet worden ist
            bool fertig = false;
            //Zaehler ansonsten kann es vielleicht passieren das man auf Sankt Nimmerleinstag wartet
            int zaehler = 0;

            do
            {
                //Kurze Zeit innehalten...dem Aktor die Chance geben sich mal wieder zu melden
                System.Threading.Thread.Sleep(2000);
                zaehler++;

                if ((valve as Valve).power_off_transmitted == true || zaehler > 3)
                    fertig = true;
            }
            while (!fertig);

            valvesLock.AcquireWriterLock(-1);
            valves.Remove((valve as Valve));
            valvesLock.ReleaseWriterLock();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        // anlernen mit timeout in ms
        public IModule anlernenModule(int timeout)
        {
            if (!Initialized)
                throw new CalibrationException(Error.NOT_INITIALIZED);

            TCMMessage message;
            DateTime startTime = System.DateTime.Now;

            bool resultOK = false;

            // ueberprueft tcmevents und schaut ob nachricht richtig angenkommen ist mit timelimit
            do
            {
                tcmEvent.wait(timeout);

                lock (tcmEvent)
                {
                    message = tcmEvent.message;
                }

                resultOK = message != null && isModuleLearnMessage(message);
            }
            while (!resultOK && startTime.AddMilliseconds(timeout).Ticks > System.DateTime.Now.Ticks);

            // wenn fehler, dann wurde nichts angelernt
            if (!resultOK)
                throw new CalibrationException(Error.NO_LEARN_MESSAGE);

            Module module;

            // ueberprueft ob es die valve schon gibt. true bedeutet valve mit der id gibt es schon
            if (getModule(message.objectID, out module))
                throw new CalibrationException(Error.MODULE_ALREADY_LEARNED);

            // also erstelle neue valve und setze die function id als naechstes id ein
            module = new Module(message.objectID, message.signalStrength, 0);

            int functionID = getNextModuleID();

            if (functionID == -1)
                throw new CalibrationException(Error.NO_FREE_IDS);

            module.FunctionID = functionID;

            if (message.manufacturer != 0x2D)
            {   //Normal modul
                byte[] data = new byte[] { 0xD4, 0x5A, 0x6B, 0xD4, 1, 2, 3, 4, 5, 6, 7, 0x00, 0x00, 0x00, 0x00, 0x00 };
                long id = tcm.getID();
                id += module.FunctionID;

                data[4] = Convert.ToByte(String.Format("10010001"), 2); // oder 10011000
                data[5] = (byte)message.byte5;
                data[6] = (byte)message.byte4;
                data[7] = (byte)message.byte3;
                data[8] = (byte)message.byte2;
                data[9] = (byte)message.byte1;
                data[10] = (byte)message.byte0;

                data[11] = (byte)(id >> 24);
                data[12] = (byte)(id >> 16);
                data[13] = (byte)(id >> 8);
                data[14] = (byte)(id);
                data = Tools.setChecksum(data);

                tcm.writeCommandModule(data, module.ModuleID);
                Debug.WriteLine(string.Format("Message Anlern Module Sending: {0}", TCM.getHexString(data)));
                do
                {
                    tcmEvent.wait(timeout);

                    lock (tcmEvent)
                    {
                        message = tcmEvent.message;
                    }

                    resultOK = message != null && isModuleMessage(message, module.ModuleID);
                }
                while (!resultOK && startTime.AddMilliseconds(timeout).Ticks > System.DateTime.Now.Ticks);

                if (!resultOK)
                    throw new CalibrationException(Error.NO_LEARN_MESSAGE);

                module.Temperature = (double)((40D / 255D) * (double)message.byte0);
            }
            else
            {
                byte[] data = new byte[] { 0xA5, 0x5A, 0x6B, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                long id = tcm.getID();
                id += module.FunctionID;

                data[08] = (byte)(id >> 24);
                data[09] = (byte)(id >> 16);
                data[10] = (byte)(id >> 8);
                data[11] = (byte)(id);
                data = Tools.setChecksum(data);
                data = message.message;
                tcm.writeCommandModule(data, module.ModuleID);
                Debug.WriteLine(string.Format("Message Anlern Module Sending: {0}", TCM.getHexString(data)));
                module.Temperature = (double)((40D / 255D) * (double)message.byte1);
                module.SetPoint = (double)(8D + ((double)(30 - 8) / 255D) * (double)message.byte2);
                module.type = 1;

            }

            // uebrprueft nochmals, ob valve noch verbunden ist, nachdem kommando uebertragen wurde


            // uebertraegt die erhaltenen daten als valve ein und speichert sie lokal ab
            module.Signal = message.signalStrength;
            module.Learned = true;
            module.KommunikationOK = true;
            module.dateTimeReceived = message.datum_uhrzeit;




            modulesLock.AcquireWriterLock(-1);
            modules.Add(module);
            modulesLock.ReleaseWriterLock();

            return module;
        }

        // anlernen mit timeout in ms
        public IValve anlernen(int timeout)
        {
            if (!Initialized)
                throw new CalibrationException(Error.NOT_INITIALIZED);

            TCMMessage message;
            DateTime startTime = System.DateTime.Now;

            bool resultOK = false;

            // ueberprueft tcmevents und schaut ob nachricht richtig angenkommen ist mit timelimit
            do
            {
                tcmEvent.wait(timeout);

                lock (tcmEvent)
                {
                    message = tcmEvent.message;
                }

                resultOK = message != null && isValveLearnMessage(message);
            }
            while (!resultOK && startTime.AddMilliseconds(timeout).Ticks > System.DateTime.Now.Ticks);

            // wenn fehler, dann wurde nichts angelernt
            if (!resultOK)
                throw new CalibrationException(Error.NO_LEARN_MESSAGE);

            Valve valve;

            // ueberprueft ob es die valve schon gibt. true bedeutet valve mit der id gibt es schon
            if (getValve(message.objectID, out valve))
                throw new CalibrationException(Error.VALVE_ALREADY_LEARNED);

            // also erstelle neue valve und setze die function id als naechstes id ein
            valve = new Valve(message.objectID, message.signalStrength, true, 0, 0);
            int functionID = getNextID();

            if (functionID == -1)
                throw new CalibrationException(Error.NO_FREE_IDS);

            valve.FunctionID = functionID;

            // ueberprueft verbindung mit valve durchs senden von nachricht? ggf zum auslesen von daten?
            //byte[] data = new byte[] { 0xA5, 0x5A, 0x6B, 0x07, 0x80, 0x0F, 0xFF, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00 }; <-- alte version
            byte[] data = new byte[] { 0xA5, 0x5A, 0x6B, 0x07, 0x80, 0x08, 0x0A, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00 };
            long id = tcm.getID();
            id += valve.FunctionID;

            data[08] = (byte)(id >> 24);
            data[09] = (byte)(id >> 16);
            data[10] = (byte)(id >> 8);
            data[11] = (byte)(id);
            data = Tools.setChecksum(data);

            tcm.writeCommandModule(data, valve.ValveID);
            Debug.WriteLine(string.Format("Message Anlern Sending: {0}", TCM.getHexString(data)));
            // uebrprueft nochmals, ob valve noch verbunden ist, nachdem kommando uebertragen wurde
            /*do
            {
                tcmEvent.wait(timeout);

                lock (tcmEvent)
                {
                    message = tcmEvent.message;
                }

                resultOK = message != null && isValveMessage(message, valve.ValveID);
            }
            while (!resultOK && startTime.AddMilliseconds(timeout).Ticks > System.DateTime.Now.Ticks);

            if (!resultOK)
                throw new CalibrationException(Error.NO_LEARN_MESSAGE);
                */
            // uebertraegt die erhaltenen daten als valve ein und speichert sie lokal ab
            valve.Signal = message.signalStrength;
            valve.Value = message.byte3;
            valve.Temperature = (double)((40D / 255D) * (double)message.byte1);
            valve.Battery = TCMMessage.readBits((byte)message.byte2, 3, 1) == 1;
            valve.Learned = true;
            valve.KommunikationOK = true;
            valve.dateTimeReceived = DateTime.Now;




            valvesLock.AcquireWriterLock(-1);
            valves.Add(valve);
            valvesLock.ReleaseWriterLock();

            return valve;
        }

        // gerlernte valves als neue liste zurueckgeben wiedergeben kein nutzen gehabt
        public List<IValve> getLearnedValves()
        {
            List<IValve> copiedValves;

            valvesLock.AcquireReaderLock(-1);
            copiedValves = new List<IValve>(valves.ToArray());
            valvesLock.ReleaseReaderLock();

            return copiedValves;
        }

        #endregion ICalibrate
    }
}
