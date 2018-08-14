using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using DynAbg.gui.control;
using FlowCalibrationInterface;
using DynAbg.valve;
using DynAbg.gui;
using DynAbg.gui.Overlay;
using System.IO.Ports;
using System.Diagnostics;
using DynAbg.Generic;
using System.Threading;
using FlowCalibration;
using System.Runtime.InteropServices;
using System.Globalization;

namespace DynAbg
{
    internal partial class FormMain : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        //override close button
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        private bool newDocument = true;
        private String file = "Neues Projekt.xml";
        
        // Anzahl Raeume 
        public int setupraeume;

        // Anzahl Ventile
        public int[] setupventile;


        public FormMain()
        {
            InitializeComponent();


            
            String[] ports = SerialPort.GetPortNames();

            OverlayForm of = new OverlayForm();
            of.ShowDialog();

            if (of.setupraeume != null && of.setupraeume != 0 && of.completed)
            {
                this.setupraeume = of.setupraeume;
                setupventile = new int[this.setupraeume];
                for (int i = 0; i < this.setupraeume; i++)
                {
                    this.setupventile[i] = of.setupventile[i];
                }
                tabControl1.SelectTab(1);
            }
            
        }

        private void init()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (Room room in Data.Rooms)
            {
                RoomPanel roomPanel = new RoomPanel(room);
                roomPanel.RoomPanelDeleted += new RoomPanelDeletedEventHandler(roomPanel_RoomPanelDeleted);
                roomPanel.RoomPanelCopied += new RoomPanelCopiedEventHandler(roomPanel_RoomPanelCopied);
                flowLayoutPanel1.Controls.Add(roomPanel);
                room.init();
            }
        }

        private void roomPanel_RoomPanelCopied(RoomPanel roomPanel)
        {
            Room room = roomPanel.Room;
            EventList<Heater> heaters = room.Heaters;
            EventList<Heater> newHeaters = new EventList<Heater>();

            Room newRoom = new Room(Data.getNewRoomID(), newHeaters);
            newRoom.Height = room.Height;
            newRoom.Name = room.Name;

            Data.Rooms.Add(newRoom);

            foreach (Heater heater in heaters)
            {
                Heater newHeater = new Heater(Data.getNewHeaterID());
                newHeater.ValveInfo = heater.ValveInfo;
                newHeaters.Add(newHeater);
            }

            RoomPanel newRoomPanel = new RoomPanel(newRoom);
            newRoomPanel.RoomPanelDeleted += new RoomPanelDeletedEventHandler(roomPanel_RoomPanelDeleted);
            newRoomPanel.RoomPanelCopied += new RoomPanelCopiedEventHandler(roomPanel_RoomPanelCopied);
            flowLayoutPanel1.Controls.Add(newRoomPanel);
        }

        private void roomPanel_RoomPanelDeleted(RoomPanel roomPanel)
        {
            roomPanel.RoomPanelDeleted -= new RoomPanelDeletedEventHandler(roomPanel_RoomPanelDeleted);
            roomPanel.RoomPanelCopied -= new RoomPanelCopiedEventHandler(roomPanel_RoomPanelCopied);
            flowLayoutPanel1.Controls.Remove(roomPanel);
            roomPanel.Dispose();
        }

        private void info_Click(object sender, EventArgs e)
        {
            new Info().ShowDialog(this);
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            string file = openFileDialog.FileName;

            if (!Data.load(file))
            {
                MessageBox.Show(this, "Die Datei konnte nicht gelesen werden.", "Datei öffnen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Text = String.Format("Dynamischer Abgleich - {0}", Path.GetFileNameWithoutExtension(file));
                this.file = file;
                newDocument = false;
                init();
            }
        }

        private void saveNew_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(file);

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            file = saveFileDialog.FileName;
            newDocument = false;
            this.Text = String.Format("Dynamischer Abgleich - {0}", Path.GetFileNameWithoutExtension(file));
            Data.save(file);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void flowLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
            FlowLayoutPanel panel = sender as FlowLayoutPanel;

            panel.SuspendLayout();

            foreach (Control control in panel.Controls)
            {
                control.SuspendLayout();
                control.Width = panel.Width - panel.Margin.Right - panel.Margin.Left - (panel.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0);
                control.ResumeLayout();
            }

            panel.ResumeLayout();
        }


        private void save_Click(object sender, EventArgs e)
        {
            if (newDocument)
                saveNew_Click(sender, e);
            else
                Data.save(file);
        }

        private void new_Click(object sender, EventArgs e)
        {
            Data.reset();
            newDocument = true;
            this.Text = "Dynamischer Abgleich - Neues Projekt";
            file = "Neues Projekt.xml";
            init();
        }

        private void initTCM_Click(object sender, EventArgs e)
        {
            /*
			 * Auskommentiert, da es Probleme gibt, wenn gerade angelernt wird.
			 * Wenn Zeit ist, dann in die TODO Liste
			 */

            //if (Data.calibration.Initialized)
            //{
            //    Data.calibration.close();
            //    EventList<Room> rooms = Data.Rooms;

            //    foreach (Room room in rooms)
            //        room.removeValves();

            //    toolStripButtonInitTCM.Image = Properties.Resources.ic;
            //    toolStripTextBoxID.Text = "Nich initialisiert -->";
            //    toolStripTextBoxID.BackColor = System.Drawing.SystemColors.Window;
            //    return;
            //}

            toolStripButtonInitTCM.Enabled = false;
            toolStripStatusLabel1.Text = "Initialisiere TCM ...";
            toolStripTextBoxID.Text = "Initialisiere ...";
            toolStripTextBoxID.BackColor = System.Drawing.SystemColors.Window;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(init);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(initComplete);
            worker.RunWorkerAsync();
        }

        private void init(object source, DoWorkEventArgs arguments)
        {
            try
            {
                Data.calibration.init();
                
            }
            catch (CalibrationException exception)
            {
                arguments.Result = exception;
                return;
            }
        }

        private void initComplete(Object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            if (result is CalibrationException)
            {
                CalibrationException exception = result as CalibrationException;

                toolStripStatusLabel1.Text += string.Format(" [FAILED] - {0}", exception.error);
                toolStripTextBoxID.BackColor = Color.Red;
                toolStripTextBoxID.Text = "ERROR";
                toolStripButtonInitTCM.Enabled = true;
                saveValveData.Enabled = false;
                Console.WriteLine("Exception: {0}, {1}", exception.error, exception.Message);
                ErrorBox eb = new ErrorBox();
                eb.ShowDialog();
            }
            else
            {
                //Adjustment.valveMessageReceived += new ValveEventHandler(valveRecevied);


                saveValveData.Enabled = true;
                toolStripStatusLabel1.Text += " [OK]";
                restoreButton.Enabled = true;
                saveValveData.Enabled = true;
                toolStripTextBoxID.BackColor = Color.YellowGreen;
                toolStripTextBoxID.Text = Data.calibration.getTCMID().ToString("X8");
                Data.calibration.PropertyChanged += new PropertyChangedEventHandler(logStart);
                if (flowLayoutPanel3.Controls.Count != 0)
                {
                    flowLayoutPanel3.Controls[0].Hide();
                }

                /*
				 * Auskommentiert, da es Probleme gibt, wenn gerade angelernt wird.
				 * Wenn Zeit ist, dann in die TODO Liste
				 */

                //toolStripButtonInitTCM.Image = Properties.Resources.ic_off;
                //toolStripButtonInitTCM.Enabled = true;
            }

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (Data.calibration != null)
            {
                int i = 1;
                Heater heat = Data.getHeater(1);
                while (heat != null)
                {
                    if (heat.contThread != null)
                    {
                        heat.contThread.Abort();
                    }
                    heat = Data.getHeater(i++);
                }
                Data.calibration.close();
            }
            if (startButton.Text != "Start")
            {
                SA_Terminate_sil();
            }
        }

        private void toolStripButtonRoom_Click(object sender, EventArgs e)
        {
            Room room = new Room(Data.getNewRoomID());
            Data.Rooms.Add(room);
            RoomPanel roomPanel = new RoomPanel(room);
            roomPanel.RoomPanelDeleted += new RoomPanelDeletedEventHandler(roomPanel_RoomPanelDeleted);
            roomPanel.RoomPanelCopied += new RoomPanelCopiedEventHandler(roomPanel_RoomPanelCopied);
            flowLayoutPanel1.Controls.Add(roomPanel);

            //for simplified
            RoomPanelSimplified roomPanelSimple = new RoomPanelSimplified();
            flowLayoutPanel2.Controls.Add(roomPanelSimple);
            ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).rps = (RoomPanelSimplified)flowLayoutPanel2.Controls[flowLayoutPanel2.Controls.Count - 1];
            roomPanel.textBoxName.TextChanged += new EventHandler(roomPanelSimple.nameBoxChanging);
            roomPanel.moduleSoll.TextChanged += new EventHandler(roomPanelSimple.sollModuleChanging);
            roomPanel.moduleIst.TextChanged += new EventHandler(roomPanelSimple.istModuleChanging);
            roomPanel.textBox1.TextChanged += new EventHandler(roomPanelSimple.sollSollChanging);
            roomPanel.moduleID.TextChanged += new EventHandler(roomPanelSimple.moduleIDChanging);
            roomPanelSimple.sollButton.Click += new EventHandler(roomPanel.showGraph_Click);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.setupraeume; i++)
            {
                toolStripButtonRoom_Click(null, null);

                //Heater Hinzufuegen
                for (int j = 0; j < this.setupventile[i]; j++)
                {
                    ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).buttonAddHeater_Click(null, null);
                   
                }
            }
            flowLayoutPanel3.Hide();
            if (setupraeume != 0 && setupventile != null)
            {
                flowLayoutPanel3.Show();
                OverlayStart_5_AnlernenTCM tcmcontrol = new OverlayStart_5_AnlernenTCM();
                tcmcontrol.anlernenButton.Click += new EventHandler(initTCM_Click);
                flowLayoutPanel3.Controls.Add(tcmcontrol);
                tcmcontrol.VisibleChanged += new EventHandler(anlernenModule);
            }
        }

        int modulanlernennummer = 0;
        private void anlernenModule(object sender, EventArgs e)
        {
            if(!((OverlayStart_5_AnlernenTCM)sender).Visible)
            {
                flowLayoutPanel3.Controls.RemoveAt(0);
                OverlayStart_6_AnlernenModule modulcontrol = new OverlayStart_6_AnlernenModule();
                modulcontrol.anlernenButton.Click += new EventHandler(((RoomPanel)flowLayoutPanel1.Controls[modulanlernennummer++]).moduleAnlernen_Click);
                flowLayoutPanel3.Controls.Add(modulcontrol);
                modulcontrol.VisibleChanged += new EventHandler(anlernenModuleNew);
            }
        }

        int setupventileraumcounter = 0;
        int setupventildurchlauf = 0;

        private void anlernenModuleNew(object sender, EventArgs e)
        {
            if (!((OverlayStart_6_AnlernenModule)sender).Visible)
            {
                flowLayoutPanel3.Controls.RemoveAt(0);
                if (modulanlernennummer < setupraeume)
                {
                    OverlayStart_6_AnlernenModule modulcontrol = new OverlayStart_6_AnlernenModule();
                    modulcontrol.anlernenButton.Click += new EventHandler(((RoomPanel)flowLayoutPanel1.Controls[modulanlernennummer++]).moduleAnlernen_Click);
                    flowLayoutPanel3.Controls.Add(modulcontrol);
                    modulcontrol.VisibleChanged += new EventHandler(anlernenModuleNew);
                }
                else
                {
                    OverlayStart_7_AnlernenVentile ventilcontrol = new OverlayStart_7_AnlernenVentile();
                    ventilcontrol.anlernenButton.Click += new EventHandler(((HeaterPanel)(((RoomPanel)flowLayoutPanel1.Controls[0]).flowLayoutPanel1.Controls[0])).buttonLearn_Click);
                    flowLayoutPanel3.Controls.Add(ventilcontrol);
                    ventilcontrol.VisibleChanged += new EventHandler(anlernenVentilNew);
                    if (setupventile[0] == 1)
                    {
                        if (setupraeume != 1)
                        {
                            setupventileraumcounter++;
                        }
                    }
                    else
                    {
                        setupventildurchlauf++;
                    }
                }
            }
        }

        private void anlernenVentilNew(object sender, EventArgs e)
        {
            if (!((OverlayStart_7_AnlernenVentile)sender).Visible)
            {
                flowLayoutPanel3.Controls.RemoveAt(0);
                //Der Fall dass das letzte Ventil eines Raumes..
                if (setupventileraumcounter == setupraeume-1 && setupventildurchlauf == setupventile[setupventileraumcounter] - 1)
                {
                    flowLayoutPanel3.Hide();
                }
                else
                {
                    if (setupventildurchlauf == setupventile[setupventileraumcounter] - 1)
                    {
                        OverlayStart_7_AnlernenVentile ventilcontrol = new OverlayStart_7_AnlernenVentile();
                        ventilcontrol.anlernenButton.Click += new EventHandler(((HeaterPanel)(((RoomPanel)flowLayoutPanel1.Controls[setupventileraumcounter]).flowLayoutPanel1.Controls[setupventildurchlauf])).buttonLearn_Click);
                        flowLayoutPanel3.Controls.Add(ventilcontrol);
                        ventilcontrol.VisibleChanged += new EventHandler(anlernenVentilNew);
                        setupventileraumcounter++;
                        setupventildurchlauf = 0;
                    }
                    else // beliebiges nicht letztes Ventil
                    {
                        OverlayStart_7_AnlernenVentile ventilcontrol = new OverlayStart_7_AnlernenVentile();
                        ventilcontrol.anlernenButton.Click += new EventHandler(((HeaterPanel)(((RoomPanel)flowLayoutPanel1.Controls[setupventileraumcounter]).flowLayoutPanel1.Controls[setupventildurchlauf++])).buttonLearn_Click);
                        flowLayoutPanel3.Controls.Add(ventilcontrol);
                        ventilcontrol.VisibleChanged += new EventHandler(anlernenVentilNew);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void saveValveData_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //saveValveData.Enabled = false;
                    saveValveData.Text = fbd.SelectedPath + "#Ventile: " + Data.calibration.getLearnedValves().Count;
                    Data.calibration.valvedatapath = fbd.SelectedPath;
                    //saveValveData.Enabled = false;
                    foreach (Room room in Data.Rooms)
                    {
                        foreach (Heater heat in room.Heaters)
                        {
                            if (heat.Valve != null)
                            {
                                heat.PropertyChanged -= new PropertyChangedEventHandler(NewPropertyChanged);
                                heat.PropertyChanged += new PropertyChangedEventHandler(NewPropertyChanged);
                            }
                        }
                        if (room.Module != null)
                        {
                            room.Module.PropertyChanged -= new PropertyChangedEventHandler(NewRoomPropertyChanged);
                            room.Module.PropertyChanged += new PropertyChangedEventHandler(NewRoomPropertyChanged);
                        }
                    }
                    startButton.Enabled = true;
                }
            }
        }

        private void NewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IValve)
            {
                IValve valve = sender as IValve;
                bool done = false;
                switch (e.PropertyName)
                {
                    case "WriteBool":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            foreach (Room room in Data.Rooms)
                            {
                                foreach (Heater heat in room.Heaters)
                                {
                                    if (heat.Valve != null && room.Module != null && heat.Valve.ValveID == valve.ValveID)
                                    {
                                        writecrashfile(room, valve);
                                        //writefile(room, valve, room.Module);
                                        done = true;
                                        break;
                                    }
                                }
                                if (done == true)
                                {
                                    break;
                                }
                            }
                        }));
                        break;
                }
            }
        }

        private void NewRoomPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IModule)
            {
                IModule module = sender as IModule;
                bool done = false;
                switch (e.PropertyName)
                {
                    case "NewMSG":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            foreach (Room room in Data.Rooms)
                            {
                                if (room.Module != null && room.Module.ModuleID == module.ModuleID)
                                {
                                    writecrashfile(room, module);
                                    done = true;
                                    break;

                                }
                                if (done == true)
                                {
                                    break;
                                }
                            }
                        }));
                        break;
                }
            }
        }

        private void writecrashfile(Room room, IModule module)
        {

            string path3 = Data.calibration.valvedatapath + "\\" + DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString();
            if (!Directory.Exists(path3))
            {
                Directory.CreateDirectory(path3);
            }
            string path = path3 + "\\Crash.txt";
            string path2 = path3 + "\\OldCrash.txt";
            if (File.Exists(path))
            {
                File.Copy(path, path2, true); // Crash zu OldCrash und jetzt Crash neuschreiben bzw. Umschreiben.
                string[] lines = File.ReadAllLines(path);
                bool found = false;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(module.ModuleID.ToString("X8")))
                    {
                        found = true;
                        lines[i] = module.ModuleID.ToString("X8") + "\t" + module.type + "\t" + module.Signal.ToString() + "\t" + module.Temperature.ToString() + "\t" + module.SetPoint.ToString() + "\t" + module.FunctionID.ToString() + "\t" + module.dateTimeReceived.ToString() + "\t" + room.ID.ToString() + "\t" + room.Name;
                        break;
                    }
                }
                if (found == false)
                {
                    //hinten ran
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(module.ModuleID.ToString("X8") + "\t" + module.type + "\t" + module.Signal.ToString() + "\t" + module.Temperature.ToString() + "\t" + module.SetPoint.ToString() + "\t" + module.FunctionID.ToString() + "\t" + module.dateTimeReceived.ToString() + "\t" + room.ID.ToString() + "\t" + room.Name);
                    }
                }
                else
                {
                    File.WriteAllLines(path, lines);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(module.ModuleID.ToString("X8") + "\t" + module.type + "\t" + module.Signal.ToString() + "\t" + module.Temperature.ToString() + "\t" + module.SetPoint.ToString() + "\t" + module.FunctionID.ToString() + "\t" + module.dateTimeReceived.ToString() + "\t" + room.ID.ToString() + "\t" + room.Name);
                }
            }
        }

        private void writecrashfile(Room room, IValve valve)
        {
            string path3 = Data.calibration.valvedatapath + "\\" + DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString();
            if (!Directory.Exists(path3))
            {
                Directory.CreateDirectory(path3);
            }
            string path = path3 + "\\Crash.txt";
            string path2 = path3 + "\\OldCrash.txt";
            if (File.Exists(path))
            {
                File.Copy(path, path2, true); // Crash zu OldCrash und jetzt Crash neuschreiben bzw. Umschreiben.
                string[] lines = File.ReadAllLines(path);
                bool found = false;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(valve.ValveID.ToString("X8")))
                    {
                        found = true;
                        lines[i] = valve.ValveID.ToString("X8") + "\t" + valve.Signal.ToString() + "\t" + valve.Battery.ToString() + "\t" + valve.Temperature.ToString() + "\t" + valve.Value.ToString() + "\t" + valve.FunctionID.ToString() + "\t" + valve.dateTimeReceived.ToString() + "\t" + valve.DesiredValue.ToString() + "\t" + room.ID.ToString() + "\t" + room.Name;
                        break;
                    }
                }
                if (found == false)
                {
                    //hinten ran
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(valve.ValveID.ToString("X8") + "\t" + valve.Signal.ToString() + "\t" + valve.Battery.ToString() + "\t" + valve.Temperature.ToString() + "\t" + valve.Value.ToString() + "\t" + valve.FunctionID.ToString() + "\t" + valve.dateTimeReceived.ToString() + "\t" + valve.DesiredValue.ToString() + "\t" + room.ID.ToString() + "\t" + room.Name);
                    }
                }
                else
                {
                    File.WriteAllLines(path, lines);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(valve.ValveID.ToString("X8") + "\t" + valve.Signal.ToString() + "\t" + valve.Battery.ToString() + "\t" + valve.Temperature.ToString() + "\t" + valve.Value.ToString() + "\t" + valve.FunctionID.ToString() + "\t" + valve.dateTimeReceived.ToString() + "\t" + valve.DesiredValue.ToString() + "\t" + room.ID.ToString() + "\t" + room.Name);
                }
            }
        }

        private void writefile(Room room, IValve valve, IModule module)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            string path = Data.calibration.valvedatapath + "\\" + DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            int pos = 0;
            for (int i = 0; i < room.Heaters.Count; i++)
            {
                Heater heat = room.Heaters[i];
                if(heat.Valve.ValveID == valve.ValveID)
                {
                    pos = i;
                    break;
                }
            }
            path = path + "\\Ventil" + (pos+1) + "_" + room.Name + ".txt";
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + "\t" + module.ModuleID.ToString("X8", nfi) + "\t" + module.SetPoint.ToString(nfi) + "\t" + room.TSoll.ToString(nfi) + "\t" + Math.Round(module.Temperature, 1).ToString(nfi) + "\t|\t" + valve.DesiredValue.ToString(nfi) + "\t" + room.Hlim.ToString(nfi) + "\t" + Math.Round(valve.Temperature, 1).ToString(nfi) + "\t" + valve.Value.ToString(nfi) + "\t" + Math.Round(valve.PAnteil).ToString(nfi) + "\t" + Math.Round(valve.IAnteil).ToString(nfi));
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("Zeit\tModuleID\tSetpointM\tTSollAlgo\tTemperaturM\t|\tSollHUBV\tHLim\tTemperaturV\tIstHUBV\tPAnteil\tIAnteil");
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + "\t" + module.ModuleID.ToString("X8", nfi) + "\t" + module.SetPoint.ToString(nfi) + "\t" + room.TSoll.ToString(nfi) + "\t" + Math.Round(module.Temperature, 1).ToString(nfi) + "\t|\t" + valve.DesiredValue.ToString(nfi) + "\t" + room.Hlim.ToString(nfi) + "\t" + Math.Round(valve.Temperature, 1).ToString(nfi) + "\t" + valve.Value.ToString(nfi) + "\t" + Math.Round(valve.PAnteil).ToString(nfi) + "\t" + Math.Round(valve.IAnteil).ToString(nfi));
                }
            }
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            if (Data.Rooms.Count != 0)
            {
                MessageBox.Show(this, "Es werden vorhanden Raeume gleich ueberschrieben.", "Load Crashfile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            string file = openFileDialog1.FileName;

            if (!load(file))
            {
                MessageBox.Show(this, "Die Datei konnte nicht gelesen werden.", "Datei öffnen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                restoreButton.Enabled = false;
            }
        }

        bool load(string file)
        {
            string lines;
            List<string> line = new List<string>();
            StreamReader sr = new StreamReader(file);
            new_Click(null, null);
            List<IModule> modules = new List<IModule>();
            List<IValve> valves = new List<IValve>();
            int roomid = 0;
            lines = sr.ReadLine();
            Module modtemp = null;
            Valve valvetemp = null;
            //moduleID	Signal	Temperatur	Setpoint	FunctionID	DataTimeReceived		RoomID	name		

            //ValveID Signal  battery temperature value(hub)  functionid datatimereceived        numeric soll    roomid name
            while(lines != null)
            {
                line.Add(lines);
                lines = sr.ReadLine();
            }
            sr.Close();
            //Sortierung nach Modulen oder Ventil To
            line.Sort((x, y) => (x.Length).CompareTo(y.Length));

            //Sortierung nach Raum
            line.Sort((x, y) => Convert.ToInt32(x.Split('\t')[(x.Split('\t').Length)-2]).CompareTo(Convert.ToInt32(y.Split('\t')[(y.Split('\t').Length) - 2])));

            while (line.Count != 0)
            {
                string[] temp = line[0].Split('\t');
                line.RemoveAt(0);
                if (temp.Length == 9) // 9 fuer module, 10 fuer Ventile
                {
                    modtemp = new Module((long)Convert.ToInt32(temp[0],16), Convert.ToInt32(temp[2]), Convert.ToDouble(temp[3]));
                    modtemp.SetPoint = Convert.ToDouble(temp[4]);
                    modtemp.FunctionID = Convert.ToInt32(temp[5]);
                    modtemp.dateTimeReceived = DateTime.Parse(temp[6]);
                    modtemp.KommunikationOK = true;
                    modtemp.Learned = true;
                    modtemp.dataask = true;
                    modtemp.roomask = true;
                    modtemp.type = Convert.ToInt32(temp[1]);
                    modules.Add(modtemp);
                    ((Calibration)Data.calibration).modulesLock.AcquireWriterLock(-1);
                    ((Calibration)Data.calibration).modules.Add(modtemp);
                    ((Calibration)Data.calibration).modulesLock.ReleaseWriterLock();
                    roomid = Convert.ToInt32(temp[7]);
                }
                else if (temp.Length == 10)
                {
                    valvetemp = new Valve((long)Convert.ToInt32(temp[0], 16), Convert.ToInt32(temp[1]), Convert.ToBoolean(temp[2]), Convert.ToDouble(temp[3]), Convert.ToInt32(temp[4]));
                    valvetemp.FunctionID = Convert.ToInt32(temp[5]);
                    valvetemp.dateTimeReceived = DateTime.Parse(temp[6]);
                    valvetemp.KommunikationOK = true;
                    valvetemp.Learned = true;
                    valves.Add(valvetemp);
                    ((Calibration)Data.calibration).valvesLock.AcquireWriterLock(-1);
                    ((Calibration)Data.calibration).valves.Add(valvetemp);
                    ((Calibration)Data.calibration).valvesLock.ReleaseWriterLock();
                    roomid = Convert.ToInt32(temp[8]);
                }
                else
                {
                    return false;
                }
                if (Data.Rooms.Count <= roomid)
                {
                    int anzahl = roomid - Data.Rooms.Count + 1;
                    for (int i = 0; i < anzahl; i++)
                    {
                        Room room = new Room(Data.getNewRoomID());
                        Data.Rooms.Add(room);
                        RoomPanel roomPanel = new RoomPanel(room);
                        roomPanel.RoomPanelDeleted += new RoomPanelDeletedEventHandler(roomPanel_RoomPanelDeleted);
                        roomPanel.RoomPanelCopied += new RoomPanelCopiedEventHandler(roomPanel_RoomPanelCopied);
                        flowLayoutPanel1.Controls.Add(roomPanel);
                    }
                }

                if (temp.Length == 9) // 9 fuer module, 10 fuer Ventile
                {
                    Data.Rooms[roomid].Name = temp[8];

                    ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).learnModuleManual(modtemp); //fehler
                    ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).textBoxNameChange(temp[8]);
                }
                else if (temp.Length == 10)
                {
                    Data.Rooms[roomid].Name = temp[9];
                    ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).textBoxNameChange(temp[9]);
                    ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).buttonAddHeater_Click(null, null);
                    ((RoomPanel)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).getLastHP().learnValveManual(valvetemp);
                    valvetemp.DesiredValue = Convert.ToInt32(temp[7]);
                }
                
            }

            

            return true;
        }

        bool running = false;
        private Thread algoThread;
        private void startButton_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                //Start
                updateProfilButton.Enabled = true;
                toolStripButtonNew.Enabled = false;
                toolStripButtonOpen.Enabled = false;
                toolStripButtonSave.Enabled = false;
                toolStripButtonRoom.Enabled = false;
                startButton.Text = "Stop";
                algoThread = new Thread(new ThreadStart(algoStart));
                algoThread.Name = "Algorithmusstart";
                algoThread.Start();
                running = true;
            }
            else
            {
                //Stop..
                updateProfilButton.Enabled = false;
                startButton.Text = "Start";
                algoThread.Abort();
                running = false;
                toolStripButtonNew.Enabled = true;
                toolStripButtonOpen.Enabled = true;
                toolStripButtonSave.Enabled = true;
                toolStripButtonRoom.Enabled = true;

                //Schileße Writer..
                SA_Terminate_sil();
            }
        }


        private void algoStart()
        {
            unsafe
            {
                // Initialisierung
                //int ventilzahl = Data.calibration.getLearnedValves().Count;

               
                double[] Bedienparam = createBedienParam();
                copyObjekt();
                if (Data.Rooms.Count == 0)
                {
                    return;
                }
                double abtastzeit = (double)((int)intervallStart.Value * 60);
                double[] ventilraumliste = new double[Data.Rooms.Count];
                double[] y1 = new double[Data.Rooms.Count]; // Solltemp unnoetig
                double[] y2 = new double[Data.Rooms.Count]; // hlim
                double[] u1 = new double[Data.Rooms.Count]; // Tist von Modulen
                double[] u2 = new double[Data.Rooms.Count]; // Hub von Raum bzw. vom Ventil
                
                foreach (Room rooms in Data.Rooms)
                {
                    ventilraumliste[rooms.ID] = rooms.ID + 1;
                    fixed (double* p = &(rooms.RWork[0]))
                    {
                        PI_Init(0.35, 1000, (int)intervallStart.Value * 60, p);
                    }
                    foreach (Heater heat in rooms.Heaters)
                    {
                        if (heat.Valve != null)
                        {
                            u2[rooms.ID] = Math.Round((double)heat.Valve.Value / 100.0,2);
                        }
                    }
                    if (rooms.Module != null)
                    {
                        u1[rooms.ID] = Math.Round((double)rooms.Module.SetPoint,2);
                    }
                    double setpoint = Math.Round((double)rooms.Module.SetPoint, 2);

                    foreach (roomData rd in rooms.dataRoom)
                    {
                        if ((int)DateTime.Now.DayOfWeek == (int)rd.Wochentag)
                        {
                            if (DateTime.Now.Hour * 60 + DateTime.Now.Minute <= Convert.ToInt32(rd.Stunde) * 60 + Convert.ToInt32(rd.Minute))
                            {
                                setpoint = Math.Round((double)rd.Temperatur, 2);
                                break;
                            }
                        }
                    }
                    Debug.WriteLine("RaumID: " + rooms.ID + " sendet SetPoint: " + setpoint);
                }
                //ggf ohne fixed probieren

                fixed (double* y1pointer = &(y1[0])) 
                {
                    fixed (double* y2pointer = &(y2[0]))
                    {
                        fixed (double* u1pointer = &(u1[0]))
                        {
                            fixed (double* u2pointer = &(u2[0]))
                            {
                                fixed (double* bedienpointer = &(Bedienparam[0]))
                                {
                                    fixed (double* ventilpointer = &(ventilraumliste[0]))
                                    {
                                        SA_Init_sil(y1pointer, y2pointer, u1pointer, u2pointer, (short) 60, bedienpointer, ventilpointer, false, true, Data.Rooms.Count);
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (Room rooms in Data.Rooms)
                {
                    if (rooms.Module != null)
                    {
                        u1[rooms.ID] = Math.Round((double)rooms.Module.SetPoint, 2);
                    }
                    foreach (Heater heat in rooms.Heaters)
                    {
                        u2[rooms.ID] = Math.Round(((double)heat.Valve.Value / 100.0), 2);
                    }
                }
                fixed (double* y1pointer = y1)
                {
                    fixed (double* y2pointer = y2)
                    {
                        fixed (double* u1pointer = u1)
                        {
                            fixed (double* u2pointer = u2)
                            {
                                SA_Output_sil(y1pointer, y2pointer, u1pointer, u2pointer);
                            }
                        }
                    }
                }
                foreach (Room rooms in Data.Rooms)
                {
                    if (rooms.Module != null)
                    {
                        rooms.Hlim = y2[rooms.ID];
                        rooms.TSoll = y1[rooms.ID];
                    }
                }


                // Main Berechnung

                while (Thread.CurrentThread.ThreadState != System.Threading.ThreadState.Aborted)
                {
                    // HLims berechnen
                    for (int i = 0; i < (int)intervallStart.Value; i++)
                    {
                        Thread.Sleep(60000);
                        foreach (Room rooms in Data.Rooms)
                        {
                            if (rooms.Module != null)
                            {
                                u1[rooms.ID] = Math.Round((double)rooms.Module.Temperature,2);
                            }
                            foreach(Heater heat in rooms.Heaters)
                            {
                                u2[rooms.ID] = Math.Round(((double)heat.Valve.Value/100.0),2);
                            }
                        }
                        fixed (double* y1pointer = y1)
                        {
                            fixed (double* y2pointer = y2)
                            {
                                fixed (double* u1pointer = u1)
                                {
                                    fixed (double* u2pointer = u2)
                                    {
                                        SA_Output_sil(y1pointer, y2pointer, u1pointer, u2pointer);
                                    }
                                }
                            }
                        }
                        foreach (Room rooms in Data.Rooms)
                        {
                            if (rooms.Module != null)
                            {
                                Debug.WriteLine("Berechneter HLim von Raum " + rooms.ID + " ist " + rooms.Hlim);
                                rooms.Hlim = y2[rooms.ID];
                                rooms.TSoll = y1[rooms.ID];
                                foreach (Heater heat in rooms.Heaters)
                                {
                                    if (heat.Valve != null)
                                    {
                                        writefile(rooms, heat.Valve, rooms.Module);
                                    }
                                }
                            }
                        }
                    }


                    foreach (Room rooms in Data.Rooms)
                    {
                        double[] hubcalc = new double[1];
                        double[] pcalc = new double[1];
                        double[] icalc = new double[1];
                        if (rooms.Module != null)
                        {
                            fixed (double* hubpointer = hubcalc)
                            {
                                fixed (double* panteilpointer = pcalc)
                                {
                                    fixed (double* ianteilpointer = icalc)
                                    {
                                        fixed (double* rworkpointer = rooms.RWork)
                                        {
                                            /*
                                            double setpoint = Math.Round((double)rooms.Module.SetPoint, 2);

                                            foreach (roomData rd in rooms.dataRoom)
                                            {
                                                if ((int)DateTime.Now.DayOfWeek == (int)rd.Wochentag)
                                                {
                                                    if (DateTime.Now.Hour * 60 + DateTime.Now.Minute <= Convert.ToInt32(rd.Stunde) * 60 + Convert.ToInt32(rd.Minute))
                                                    {
                                                        setpoint = Math.Round((double)rd.Temperatur, 2);
                                                        break;
                                                    }
                                                }
                                            }*/
                                            double setpoint = rooms.TSoll;
                                            Debug.WriteLine("RaumID: " + rooms.ID + " sendet SetPoint: " + setpoint);
                                            PI_Output(hubpointer, panteilpointer, ianteilpointer, rworkpointer, (double)rooms.Module.Temperature, setpoint, y2[rooms.ID]);
                                        }
                                    }
                                }
                            }
                        }
                        foreach (Heater heat in rooms.Heaters)
                        {
                            if (heat.Valve != null)
                            {
                                heat.Valve.DesiredValue = (int)(Math.Round(hubcalc[0] * 100));
                                heat.Valve.PAnteil = 100*pcalc[0];
                                heat.Valve.IAnteil = 100*icalc[0];
                            }
                        }
                        if (DateTime.Now.Hour == 23 && DateTime.Now.Minute >= 50)
                        {
                            copyObjekt();
                        }
                    }
                }
            }
        }


        [DllImport("PI_Regler.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void PI_Init(double Param_Kp, double Param_Tn, double Abtastzeit6min, double* RWork);

        [DllImport("PI_Regler.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void PI_Output(double* Hub, double* P_Anteil, double* I_Anteil, double* RWork, double Tist, double Tsoll, double Hlim);

        [DllImport("Algorithmus.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SA_Init_sil(double* y1, double* y2, double* u1, double* u2, short ABTASTZEIT, double* BedienParam, double* VENTILRAUMLISTE, bool aufheizoptimierung, bool ausgabe, int Anz_Raum);

        [DllImport("Algorithmus.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SA_Output_sil(double* y1, double* y2, double* u1, double* u2); // TSoll, HLim, Tist Hub Position

        [DllImport("Algorithmus.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SA_Update_sil(double* BedienParam, bool Aufheiz);

        [DllImport("Algorithmus.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SA_Terminate_sil();

        private void copyObjekt()
        {
            string path = "Objektkenndata.txt";
            string path2 = Data.calibration.valvedatapath + "\\" + DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString("00") + "\\Objektkenndata.txt";
            if (File.Exists(path))
            {
                File.Copy(path, path2, true);
            }
        }

        /*% Belegungstabelle der Uhrzeit in Abstand aller 10 Minuten in 5_3 Format:
        %  0-00:00;   1-00:10;   2-00:20;   3-00:30;   4-00:40;   5-00:50;   6 &   7: frei
        %  8-01:00;   9-01:10;  10-01:20;  11-01:30;  12-01:40;  13-01:50;  14 &  15: frei
        % 16-02:00;  17-02:10;  18-02:20;  19-02:30;  20-02:40;  21-02:50;  22 &  23: frei
        % 24-03:00;  25-03:10;  26-03:20;  27-03:30;  28-03:40;  29-03:50;  30 &  31: frei
        % 32-04:00;  33-04:10;  34-04:20;  35-04:30;  36-04:40;  37-04:50;  38 &  39: frei
        % 40-05:00;  41-05:10;  42-05:20;  43-05:30;  44-05:40;  45-05:50;  46 &  47: frei
        % 48-06:00;  49-06:10;  50-06:20;  51-06:30;  52-06:40;  53-06:50;  54 &  55: frei
        % 56-07:00;  57-07:10;  58-07:20;  59-07:30;  60-07:40;  61-07:50;  62 &  63: frei
        % 64-08:00;  65-08:10;  66-08:20;  67-08:30;  68-08:40;  69-08:50;  70 &  71: frei
        % 72-09:00;  73-09:10;  74-09:20;  75-09:30;  76-09:40;  77-09:50;  78 &  79: frei
        % 80-10:00;  81-10:10;  82-10:20;  83-10:30;  84-10:40;  85-10:50;  86 &  87: frei
        % 88-11:00;  89-11:10;  90-11:20;  91-11:30;  92-11:40;  93-11:50;  94 &  95: frei
        % 96-12:00;  97-12:10;  98-12:20;  99-12:30; 100-12:40; 101-12:50; 102 & 103: frei
        %104-13:00; 105-13:10; 106-13:20; 107-13:30; 108-13:40; 109-13:50; 110 & 111: frei
        %112-14:00; 113-14:10; 114-14:20; 115-14:30; 116-14:40; 117-14:50; 118 & 119: frei
        %120-15:00; 121-15:10; 122-15:20; 123-15:30; 124-15:40; 125-15:50; 126 & 127: frei
        %128-16:00; 129-16:10; 130-16:20; 131-16:30; 132-16:40; 133-16:50; 134 & 135: frei
        %136-17:00; 137-17:10; 138-17:20; 139-17:30; 140-17:40; 141-17:50; 142 & 143: frei
        %144-18:00; 145-18:10; 146-18:20; 147-18:30; 148-18:40; 149-18:50; 150 & 151: frei
        %152-19:00; 153-19:10; 154-19:20; 155-19:30; 156-19:40; 157-19:50; 158 & 159: frei
        %160-20:00; 161-20:10; 162-20:20; 163-20:30; 164-20:40; 165-20:50; 166 & 167: frei
        %168-21:00; 169-21:10; 170-21:20; 171-21:30; 172-21:40; 173-21:50; 174 & 175: frei
        %176-22:00; 177-22:10; 178-22:20; 179-22:30; 180-22:40; 181-22:50; 182 & 183: frei
        %184-23:00; 185-23:10; 186-23:20; 187-23:30; 188-23:40; 189-23:50;    ab 190: frei
        */
        private double[] createBedienParam()
        {
            double[] temp = new double[Data.Rooms.Count * 12 * 7 * 2];
            int position = 0;
            foreach (Room rooms in Data.Rooms)
            {
                if(rooms.Module != null)
                {
                    rooms.sort();
                    for (int tag = 1; tag <= 7; tag++)
                    {
                        List<roomData> newlist = rooms.roomdataDay(tag);
                        int anzahlpunkte = 0;
                        for (int i = 0; i < newlist.Count; i++)
                        {
                            roomData rd = newlist[i];
                            byte Minute = rd.Minute;
                            byte Stunde = rd.Stunde;

                            int MinuteRound = (Minute / 10);
                            int StundeRound = (int)Stunde;

                            if (i == newlist.Count - 1) // auffuellen, wenn man kurz vorm dem letzten punkt ist
                            {
                                
                                for (int zahl = 1; zahl < 12-anzahlpunkte; zahl++)
                                {
                                    if (((int)temp[position - 2]) % 8 != 5)
                                    {
                                        temp[position] = temp[position - 2] + 1;
                                    }
                                    else
                                    {
                                        temp[position] = temp[position - 2] + 3;
                                    }
                                    position++;
                                    temp[position++] = (double)rd.Temperatur;
                                }
                            }

                            if (anzahlpunkte < 12)
                            {
                                temp[position++] = 8 * StundeRound + MinuteRound;
                                temp[position++] = (double)rd.Temperatur;
                                anzahlpunkte++;
                            }
                        }
                    }
                }
            }
            writeparam(temp);
            return temp;
        }

        private void writeparam(double[] bedienparam)
        {
            string path = Data.calibration.valvedatapath + "\\" + DateTime.Now.Day.ToString("00") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\Bedienparam.txt";
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("------------");
                    for (int i = 0; i < bedienparam.Length; i++)
                    {
                        if(bedienparam[i] == 0)
                        {
                            sw.Write(Environment.NewLine);
                        }
                        sw.Write(bedienparam[i] + ", ");
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    for (int i = 0; i < bedienparam.Length; i++)
                    {
                        if (bedienparam[i] == 0)
                        {
                            sw.Write(Environment.NewLine);
                        }
                        sw.Write(bedienparam[i] + ", ");
                    }
                }
            }
        }



        private void updateProfilButton_Click(object sender, EventArgs e)
        {
            double[] Bedienparam = createBedienParam();
            unsafe
            {
                fixed (double* bedienpointer = &(Bedienparam[0]))
                {
                    SA_Update_sil(bedienpointer, ((int)aufheizNumeric.Value) == 1 ? true : false);
                }
            }
        }




        private void logStart(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ICalibration)
            {
                ICalibration calib = sender as ICalibration;
                switch (e.PropertyName)
                {
                    case "NewMSG":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            dataGridView2.Rows.Add(new object[] { "Receive", calib.lastMessageData, calib.lastEnOceanID, calib.lastTime.ToString() });
                        }));
                        break;
                }
            }
        }



        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            tabControl1.Height = ClientRectangle.Height - 75;
            tabControl1.Width = ClientRectangle.Width;
            dataGridView2.Height = ClientRectangle.Height - 100;
            dataGridView2.Width = ClientRectangle.Width - 25;
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = ClientRectangle.Width - 350;
            dataGridView2.Columns[2].Width = 65;
            dataGridView2.Columns[3].Width = 115;
        }

        string startPath = String.Format(@"{0}\option.txt", Application.StartupPath);
        private void saveOption_Click(object sender, EventArgs e)
        {
            if (File.Exists(startPath))
            {
                File.Delete(startPath);
            }
            using (StreamWriter sw = new StreamWriter(startPath, true))
            {
                sw.WriteLine("Midnight Calibration" + "\t" + ((midnightRecalibration.Checked) ? "1" : "0"));
                sw.WriteLine("Manueller Sollwert" + "\t" + ((manualSoll.Checked) ? "1" : "0"));
                sw.WriteLine("Simplify GUI" + "\t" + ((simplifyCheck.Checked) ? "1" : "0"));
            }
            option_setup();
        }

        public void loadOption_Click(object sender, EventArgs e)
        {
            if (File.Exists(startPath))
            {
                using (StreamReader sr = new StreamReader(startPath, true))
                {
                    //midnight Recalibration
                    string stringOption = sr.ReadLine();
                    if(stringOption.Split('\t')[1] == "1")
                    {
                        midnightRecalibration.Checked = true;
                    }
                    else
                    {
                        midnightRecalibration.Checked = false;
                    }

                    //manual Soll
                    stringOption = sr.ReadLine();
                    if (stringOption.Split('\t')[1] == "1")
                    {
                        manualSoll.Checked = true;
                    }
                    else
                    {
                        manualSoll.Checked = false;
                    }
                    stringOption = sr.ReadLine();
                    if (stringOption.Split('\t')[1] == "1")
                    {
                        simplifyCheck.Checked = true;
                    }
                    else
                    {
                        simplifyCheck.Checked = false;
                    }
                }
            }
            option_setup();
        }

        private void option_setup()
        {
            Data.calibration.midnightrecalib = midnightRecalibration.Checked;

            foreach(Room rooms in Data.Rooms)
            {
                foreach(Heater heat in rooms.Heaters)
                {
                    heat.setManual(manualSoll.Checked);
                }
            }

            Data.calibration.manualSoll = manualSoll.Checked;
            Data.calibration.simplifyCheck = simplifyCheck.Checked;
        }

        private void overlayStart1_Load(object sender, EventArgs e)
        {

        }
    }
}
