using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DynAbg.Generic;

using FlowCalibrationInterface;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DynAbg.gui.control
{
    delegate void RoomPanelAddedEventHandler(RoomPanel roomPanel);
    delegate void RoomPanelDeletedEventHandler(RoomPanel roomPanel);
    delegate void RoomPanelCopiedEventHandler(RoomPanel roomPanel);


    internal partial class RoomPanel : UserControl
    {
        private bool newDocument = true;
        private String file = "Raumdaten.txt";
        private String file2 = "Nutzraumdaten.txt";
        public event RoomPanelDeletedEventHandler RoomPanelDeleted;
        public event RoomPanelCopiedEventHandler RoomPanelCopied;

        private FormRoomGraph formRoomGraph = null;

        private Point mousePosition;
        public Room Room { get; private set; }

        public RoomPanelSimplified rps;

        public RoomPanel(Room room)
        {
            this.Room = room;
            InitializeComponent();

            this.Room.PropertyChanged += new PropertyChangedEventHandler(Room_PropertyChanged);

            if (room.Height != 0)
                this.Height = room.Height;


            groupBoxTitle.Text = String.Format("Raum: {0}", room.ID + 1);
            textBoxName.Text = room.Name;

            foreach (Heater heater in room.Heaters)
            {
                HeaterPanel heaterPanel = new HeaterPanel(heater);
                heaterPanel.HeaterPanelDeleted += new HeaterPanelDeletedEventHandler(heaterPanelDeleted);
                heaterPanel.HeaterPanelCopied += new HeaterPanelCopiedEventHandler(heaterPanelCopied);
                heaterPanel.HeaterPanelLearning += new HeaterPanelLearningEventHandler(heaterPanel_HeaterPanelLearning);
                flowLayoutPanel1.Controls.Add(heaterPanel);
            }
        }

        private int learnCount = 0;

        private void heaterPanel_HeaterPanelLearning(bool learning)
        {
            if (learning)
            {
                learnCount++;
                buttonDelete.Enabled = false;
            }
            else
            {
                learnCount--;

                if (learnCount == 0)
                    buttonDelete.Enabled = true;
            }

        }


        private void Room_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Room)
            {
                Room room = sender as Room;
                switch (e.PropertyName)
                {
                    case "ID":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            groupBoxTitle.Text = String.Format("Raum: {0}", room.ID + 1);
                            textBoxName.Text = room.Name;
                        }));
                        break;

                    case "Temperature":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            moduleIst.Text = String.Format("{0:F1} °C", room.Module.Temperature);
                        }));

                        break;

                    case "SetPoint":// ungenutzt
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            moduleSoll.Text = String.Format("{0:F1} °C", Math.Round(2*room.Module.SetPoint)/2.0D);
                            if (Room.Module.first == false || Room.nutzdataRoom.Count == 0)
                            {
                                Room.Module.first = true;
                            }
                            else
                            {
                                byte nexthour = room.nutzdataRoom[room.nextPoint()].Stunde;
                                byte nextminute = room.nutzdataRoom[room.nextPoint()].Minute;

                                int a = room.nextPoint();
                                if (room.nutzdataRoom[a].Temperatur != room.Module.SetPoint)
                                {
                                    room.nutzdataRoom.RemoveAt(Room.nextPoint());
                                }
                                room.nutzaddDataAt(room.ID, (roomData.Day)((int)(DateTime.Now.DayOfWeek)), (byte)(DateTime.Now.Hour), (byte)(DateTime.Now.Minute), room.previousTemp(), a);
                                room.nutzaddDataAt(room.ID, (roomData.Day)((int)(DateTime.Now.DayOfWeek)), (byte)(DateTime.Now.Hour), (byte)(DateTime.Now.Minute), (short)Math.Round(room.Module.SetPoint), a + 1);
                                room.nutzaddDataAt(room.ID, (roomData.Day)((int)(DateTime.Now.DayOfWeek)), nexthour, nextminute, (short)Math.Round(room.Module.SetPoint), a + 2);
                                if (formRoomGraph != null && !formRoomGraph.IsDisposed)
                                {
                                    formRoomGraph.refresh();
                                }
                            }
                            //room.dataRoom.Add(new roomData(Room.ID, (roomData.Day) 1, room.Module.
                        })); //(int) (new DateTime().DayOfWeek)

                        break;
                    case "KommunikationOK":
                    case "Battery":
                    case "Module":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            if (room.Module.KommunikationOK && room.Module.Signal < 85)
                            {
                                moduleID.BackColor = Color.YellowGreen;
                            }
                            else if (room.Module.KommunikationOK)
                            {
                                moduleID.BackColor = Color.Orange;
                            }
                            else
                            {
                                moduleID.BackColor = Color.Red;
                            }
                            moduleIst.Text = String.Format("{0:F1} °C", room.Module.Temperature);
                            moduleSoll.Text = String.Format("{0:F1} °C", Math.Round(2*room.Module.SetPoint)/2.0D);
                        }));

                        break;

                    case "NewTime":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            room.nutzdataRoom = convertlist(room.Module.timeprog);
                            room.dataRoom = convertlist(room.Module.timeprog);
                            if (formRoomGraph != null && !formRoomGraph.IsDisposed)
                            {
                                formRoomGraph.refresh();
                            }
                        }));

                        break;


                }
            }
            if (sender is IModule)
            {
                IModule module = sender as IModule;
                switch (e.PropertyName)
                {
                    case "Temperature":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            moduleIst.Text = String.Format("{0:F1} °C", module.Temperature);
                        }));

                        break;
                    case "KommunikationOK":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            if (module.KommunikationOK && module.Signal < 85)
                            {
                                moduleID.BackColor = Color.YellowGreen;
                            }
                            else if (module.KommunikationOK)
                            {
                                moduleID.BackColor = Color.Orange;
                            }
                            else
                            {
                                moduleID.BackColor = Color.Red;
                            }
                        }));
                        break;
                    case "Battery":
                    case "Module":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            if (module.KommunikationOK && module.Signal < 85)
                            {
                                moduleID.BackColor = Color.YellowGreen;
                            }
                            else if (module.KommunikationOK)
                            {
                                moduleID.BackColor = Color.Orange;
                            }
                            else
                            {
                                moduleID.BackColor = Color.Red;
                            }
                            moduleIst.Text = String.Format("{0:F1} °C", module.Temperature);
                            moduleSoll.Text = String.Format("{0:F1} °C", Math.Round(2*module.SetPoint)/2.0D);
                            double setpoint = Math.Round((double)module.SetPoint, 2);

                            foreach (roomData rd in Room.dataRoom)
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
                            textBox1.Text = setpoint.ToString();
                            
                        }));

                        break;

                    case "NewTime":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            showGraph.Enabled = true;
                            if (module.timeprog.Count != 0)
                            {
                                Room.nutzdataRoom = convertlist(module.timeprog);
                                Room.dataRoom = convertlist(module.timeprog);
                            }
                            if (formRoomGraph != null && !formRoomGraph.IsDisposed)
                            {
                                formRoomGraph.refresh();
                            }
                        }));

                        break;

                    case "NewMSG":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            //
                            double setpoint = Math.Round((double)module.SetPoint, 2);

                            foreach (roomData rd in Room.dataRoom)
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
                            textBox1.Text = setpoint.ToString();
                        }));

                        break;

                    case "SetPoint"://genutzt
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            moduleSoll.Text = String.Format("{0:F1} °C", Math.Round(module.SetPoint*2)/2.0D);
                            if (Room.Module.first == false || Room.nutzdataRoom.Count == 0)
                            {
                                Room.Module.first = true;
                            }
                            else
                            { //genutzt

                                byte nexthour = Room.nutzdataRoom[Room.nextPoint()].Stunde;
                                byte nextminute = Room.nutzdataRoom[Room.nextPoint()].Minute;
                                int a = Room.nextPoint();
                                Room.nutzdataRoom.RemoveAt(Room.nextPoint());

                                Room.nutzaddDataAt(Room.ID, (roomData.Day)((int)(DateTime.Now.DayOfWeek)), (byte)(DateTime.Now.Hour), (byte)(DateTime.Now.Minute), Room.previousTemp(), a);
                                Room.nutzaddDataAt(Room.ID, (roomData.Day)((int)(DateTime.Now.DayOfWeek)), (byte)(DateTime.Now.Hour), (byte)(DateTime.Now.Minute), (short)Math.Round(module.SetPoint), a + 1);
                                Room.nutzaddDataAt(Room.ID, (roomData.Day)((int)(DateTime.Now.DayOfWeek)), nexthour, nextminute, (short)Math.Round(Room.Module.SetPoint), a + 2);
                                if (formRoomGraph != null && !formRoomGraph.IsDisposed)
                                {
                                    formRoomGraph.refresh();
                                }
                            }
                        }));

                        break;
                }
            }
        }

        private void pictureBoxResize_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Height = Math.Max(Parent.Size.Height - Margin.Bottom - Margin.Top, MinimumSize.Height);
        }

        private void pictureBoxResize_MouseDown(object sender, MouseEventArgs e)
        {
            mousePosition = e.Location;
        }

        private void pictureBoxResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            Point newMousePosition = e.Location;
            int x = newMousePosition.X - mousePosition.X;
            int y = newMousePosition.Y - mousePosition.Y;
            int minWidth = this.MinimumSize.Width;
            int minHeight = this.MinimumSize.Height;
            int width = this.Size.Width + x;
            int height = this.Size.Height + y;

            Height = Math.Max(height, minHeight);
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

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Room.Name = textBox.Text;
        }

        public void textBoxNameChange(string txt)
        {
            textBoxName.Text = txt;
        }

        private void RoomPanel_SizeChanged(object sender, EventArgs e)
        {
            Room.Height = Height;
        }

        public void buttonAddHeater_Click(object sender, EventArgs e)
        {
            Heater heater = new Heater(Data.getNewHeaterID());
            Room.Heaters.Add(heater);
            HeaterPanel heaterPanel = new HeaterPanel(heater);
            heaterPanel.HeaterPanelDeleted += new HeaterPanelDeletedEventHandler(heaterPanelDeleted);
            heaterPanel.HeaterPanelCopied += new HeaterPanelCopiedEventHandler(heaterPanelCopied);
            heaterPanel.HeaterPanelLearning += new HeaterPanelLearningEventHandler(heaterPanel_HeaterPanelLearning);
            heater.setManual(Data.calibration.manualSoll);
            flowLayoutPanel1.Controls.Add(heaterPanel);

            //Simplified
            //HeaterPanelSimplified roomPanelSimple = new HeaterPanelSimplified(heater.ID+1);

            rps.addHeater(heater.ID+1);
            heaterPanel.textBoxID.TextChanged += new EventHandler(((HeaterPanelSimplified)rps.tableLayoutPanel1.Controls[rps.tableLayoutPanel1.Controls.Count - 1]).textChanged);
            heaterPanel.numericSoll.TextChanged += new EventHandler(((HeaterPanelSimplified)rps.tableLayoutPanel1.Controls[rps.tableLayoutPanel1.Controls.Count - 1]).numericSollChanged);
            heaterPanel.isValue.TextChanged += new EventHandler(((HeaterPanelSimplified)rps.tableLayoutPanel1.Controls[rps.tableLayoutPanel1.Controls.Count - 1]).isValueChanged);
            heaterPanel.isTemp.TextChanged += new EventHandler(((HeaterPanelSimplified)rps.tableLayoutPanel1.Controls[rps.tableLayoutPanel1.Controls.Count - 1]).isTempChanged);
            heaterPanel.Mode.TextChanged += new EventHandler(((HeaterPanelSimplified)rps.tableLayoutPanel1.Controls[rps.tableLayoutPanel1.Controls.Count - 1]).modusChanged);


            /*
             *             ()(((FormMain)mainforms).flowLayoutPanel2.Controls[this.Room.ID]).Hide();
            roomPanel.textBoxName.TextChanged += new EventHandler(roomPanelSimple.nameBoxChanging);
            roomPanel.moduleSoll.TextChanged += new EventHandler(roomPanelSimple.sollModuleChanging);
            roomPanel.moduleIst.TextChanged += new EventHandler(roomPanelSimple.istModuleChanging);
            roomPanel.textBox1.TextChanged += new EventHandler(roomPanelSimple.sollSollChanging);
            roomPanel.moduleID.TextChanged += new EventHandler(roomPanelSimple.moduleIDChanging);
            roomPanelSimple.sollButton.Click += new EventHandler(roomPanel.showGraph_Click);
            roomPanel.buttonAddHeater.Click += new EventHandler(roomPanelSimple.addHeater);
             * */


        }

        public HeaterPanel getLastHP()
        {
            return (HeaterPanel)(flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]);
        }

        private void heaterPanelDeleted(HeaterPanel heaterPanel)
        {
            heaterPanel.HeaterPanelDeleted -= new HeaterPanelDeletedEventHandler(heaterPanelDeleted);
            heaterPanel.HeaterPanelCopied -= new HeaterPanelCopiedEventHandler(heaterPanelCopied);
            heaterPanel.HeaterPanelLearning -= new HeaterPanelLearningEventHandler(heaterPanel_HeaterPanelLearning);
            heaterPanel.Dispose();

            EventList<Heater> heaters = Room.Heaters;
            heaters.Remove(heaterPanel.Heater);

            if (heaterPanel.Heater.Valve != null)
                Data.calibration.deactivateValve(heaterPanel.Heater.Valve);

            flowLayoutPanel1.Controls.Remove(heaterPanel);
        }

        private void heaterPanelCopied(HeaterPanel heaterPanel)
        {
            Heater heater = heaterPanel.Heater;

            Heater heaterNew = new Heater(Data.getNewHeaterID());
            heaterNew.ValveInfo = heater.ValveInfo;

            HeaterPanel heaterPanelNew = new HeaterPanel(heaterNew);
            heaterPanelNew.HeaterPanelDeleted += new HeaterPanelDeletedEventHandler(heaterPanelDeleted);
            heaterPanelNew.HeaterPanelCopied += new HeaterPanelCopiedEventHandler(heaterPanelCopied);
            heaterPanelNew.HeaterPanelLearning += new HeaterPanelLearningEventHandler(heaterPanel_HeaterPanelLearning);

            Room.Heaters.Add(heaterNew);

            flowLayoutPanel1.Controls.Add(heaterPanelNew);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Alle Heizkörper löschen um die Eventhandler zu entfernen
            foreach (Heater heater in Room.Heaters)
                if (heater.Valve != null)
                    Data.calibration.deactivateValve(heater.Valve);

            Room.Heaters.Clear();
            Room.PropertyChanged -= new PropertyChangedEventHandler(Room_PropertyChanged);

            Data.Rooms.Remove(Room);
            Data.sortRoomIDs(Room.ID);

            RoomPanelDeletedEventHandler eventHandler = RoomPanelDeleted;

            if (eventHandler != null)
                eventHandler(this);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            RoomPanelCopiedEventHandler eventHandler = RoomPanelCopied;

            if (eventHandler != null)
                eventHandler(this);
        }

        public void showGraph_Click(object sender, EventArgs e)
        {
            if (Room.dataRoom.Count == 0)
            {
                if (moduleID.BackColor == Color.YellowGreen)
                {
                    Room.nutzdataRoom = convertlist(Room.Module.timeprog);
                    Room.dataRoom = convertlist(Room.Module.timeprog);
                }
            }
            if (formRoomGraph == null)
            {
                formRoomGraph = FormRoomGraph.show(Room);
            }
            else
            {
                if (formRoomGraph.IsDisposed)
                    formRoomGraph = FormRoomGraph.show(Room);
                formRoomGraph.BringToFront();
            }
        }

        private List<roomData> convertlist(List<timeProgram> tp)
        {
            List<roomData> roomdatas = new List<roomData>();

            foreach (timeProgram tptemp in tp)
            {
                if (tptemp.deletion == 0)
                {
                    byte tag = tptemp.period;
                    int[] iarray = new int[] { };
                    switch (tag)
                    {
                        case 0:
                            iarray = new int[] { 1, 2, 3, 4, 5, 6, 7 };
                            break;
                        case 1:
                            iarray = new int[] { 1, 2, 3, 4, 5, };
                            break;
                        case 2:
                            iarray = new int[] { 6, 7 };
                            break;
                        case 3:
                            iarray = new int[] { 1 };
                            break;
                        case 4:
                            iarray = new int[] { 2 };
                            break;
                        case 5:
                            iarray = new int[] { 3 };
                            break;
                        case 6:
                            iarray = new int[] { 4 };
                            break;
                        case 7:
                            iarray = new int[] { 5 };
                            break;
                        case 8:
                            iarray = new int[] { 6 };
                            break;
                        case 9:
                            iarray = new int[] { 7 };
                            break;
                        case 10:
                            iarray = new int[] { 1, 2, 3 };
                            break;
                        case 11:
                            iarray = new int[] { 2, 3, 4 };
                            break;
                        case 12:
                            iarray = new int[] { 3, 4, 5 };
                            break;
                        case 13:
                            iarray = new int[] { 5, 6 };
                            break;
                        case 14:
                            iarray = new int[] { 5, 6, 7 };
                            break;
                        case 15:
                            iarray = new int[] { 5, 6, 7, 1 };
                            break;
                    }
                    foreach (int temp in iarray)
                    {
                        if (tptemp.sStunde == 0 && tptemp.sMinute == 0)
                        {

                        }
                        else
                        {
                            roomdatas.Add(new roomData(Room.ID, (roomData.Day)temp, tptemp.sStunde, tptemp.sMinute, (short)Math.Round(Room.Module.EcoSetPoint)));
                        }
                        roomdatas.Add(new roomData(Room.ID, (roomData.Day)temp, tptemp.sStunde, tptemp.sMinute, (short)Math.Round(Room.Module.ComSetPoint)));
                        roomdatas.Add(new roomData(Room.ID, (roomData.Day)temp, tptemp.eStunde, tptemp.eMinute, (short)Math.Round(Room.Module.ComSetPoint)));
                        if (tptemp.sStunde == 23 && tptemp.sMinute == 59)
                        {

                        }
                        else
                        {
                            roomdatas.Add(new roomData(Room.ID, (roomData.Day)temp, tptemp.eStunde, tptemp.eMinute, (short)Math.Round(Room.Module.EcoSetPoint)));
                        }
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                //Anfang und endpunkte
                bool exist = false;
                foreach (roomData rd in roomdatas)
                {
                    if ((int)rd.Wochentag == i && rd.Stunde == 0 && rd.Minute == 0)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    roomdatas.Add(new roomData(Room.ID, (roomData.Day)i, 0, 0, (short)Math.Round(Room.Module.EcoSetPoint)));
                }
            }
            for (int i = 1; i <= 7; i++)
            {
                //Anfang und endpunkte
                bool exist = false;
                foreach (roomData rd in roomdatas)
                {
                    if ((int)rd.Wochentag == i && rd.Stunde == 23 && rd.Minute == 59)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    roomdatas.Add(new roomData(Room.ID, (roomData.Day)i, 23, 59, (short)Math.Round(Room.Module.EcoSetPoint)));
                }
            }
            return roomdatas;
        }

        private void roomDataSave_Click(object sender, EventArgs e)
        {
            if (formRoomGraph != null && !formRoomGraph.IsDisposed)
            {
                MessageBox.Show(this, "Bitte erst das Sollprofilfenster abspeichern und dann schließen.", "Open Graph", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (newDocument)
                saveNew_Click(sender, e);
            else
                save(file);

        }


        private void saveNew_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = Path.GetFileName(file);

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            file = saveFileDialog.FileName;
            newDocument = false;
            this.Text = String.Format("Dynamischer Abgleich - {0}", Path.GetFileName(file));
            save(file);
        }

        internal void save(string file)
        {

            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(file);

                //Write a line of text
                foreach (roomData rdata in Room.dataRoom)
                {
                    sw.WriteLine((int)rdata.Wochentag + "\t" + rdata.Stunde + "\t" + rdata.Minute + "\t" + rdata.Temperatur);
                }

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }


        private void roomDataLoad_Click(object sender, EventArgs e)
        {
            if (formRoomGraph != null && !formRoomGraph.IsDisposed)
            {
                MessageBox.Show(this, "Bitte erst das Sollprofilfenster schließen. Achtung! alle vorhanden Daten werden dann gelöscht.", "Open Graph", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            string file = openFileDialog.FileName;

            if (!load(file))
            {
                MessageBox.Show(this, "Die Datei konnte nicht gelesen werden.", "Datei öffnen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Text = String.Format("Dynamischer Abgleich - {0}", Path.GetFileNameWithoutExtension(file));
                this.file = file;
                newDocument = false;
            }
        }

        bool load(string file)
        {
            string line;
            StreamReader sr = new StreamReader(file);
            List<roomData> rooms = new List<roomData>();
            List<roomData> nutzrooms = new List<roomData>();

            line = sr.ReadLine();

            while (line != null)
            {
                string[] temp = line.Split('\t');
                if (temp.Length == 4)
                {
                    rooms.Add(new roomData(Room.ID, (roomData.Day)Convert.ToInt32(temp[0]), Convert.ToByte(temp[1]), Convert.ToByte(temp[2]), Convert.ToByte(temp[3])));
                    nutzrooms.Add(new roomData(Room.ID, (roomData.Day)Convert.ToInt32(temp[0]), Convert.ToByte(temp[1]), Convert.ToByte(temp[2]), Convert.ToByte(temp[3])));
                }
                else
                {
                    return false;
                }
                line = sr.ReadLine();
            }


            sr.Close();

            if (rooms == null)
                return false;

            Room.dataRoom = rooms;
            Room.nutzdataRoom = nutzrooms;

            if (formRoomGraph == null)
            {
                //formRoomGraph = FormRoomGraph.show(Room);
            }
            else
            {
                //if (formRoomGraph.IsDisposed)
                //formRoomGraph = FormRoomGraph.show(Room);
                //formRoomGraph.BringToFront();
                if (!formRoomGraph.IsDisposed)
                {
                    formRoomGraph.refresh();
                }
            }

            return true;
        }


        public void moduleAnlernen_Click(object sender, EventArgs e)
        {
            if (Data.calibration == null || !Data.calibration.Initialized)
            {
                MessageBox.Show(this, "Das Anlernen ist nicht möglich,\nda das TCM Modul nicht initialisiert wurde.", "Anlernen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (formRoomGraph != null && !formRoomGraph.IsDisposed)
            {
                MessageBox.Show(this, "Bitte erst das Sollprofilfenster schließen. Achtung! alle vorhanden Daten werden dann gelöscht.", "Open Graph", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Room.Module != null)
            {
                Data.calibration.deactivateModule(Room.Module);
            }

            showGraph.Enabled = false;
            moduleAnlernen.Enabled = false;
            moduleID.BackColor = System.Drawing.SystemColors.Window;
            moduleID.Text = "Lerne ...";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(learnModule);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(learnModuleComplete);
            worker.RunWorkerAsync();
            /*
            HeaterPanelLearningEventHandler eventHandler = HeaterPanelLearning;

            if (eventHandler != null)
                eventHandler(true);
            */
        }

        private void learnModule(object source, DoWorkEventArgs arguments)
        {
            string error = null;
            IModule module = null;

            try
            {
                module = Data.calibration.anlernenModule(60000);
            }
            catch (CalibrationException exception)
            {
                switch (exception.error)
                {
                    case Error.NO_FREE_IDS:
                        error = "Keine freien IDs vorhanden";
                        showGraph.Enabled = true;
                        break;

                    case Error.NO_LEARN_MESSAGE:
                        error = "Kein Anlerntelegramm empfangen";
                        showGraph.Enabled = true;
                        break;

                    default:
                        Console.WriteLine("Unknown exception: {0}", exception.error);
                        error = "Unbekannter Fehler";
                        showGraph.Enabled = true;
                        break;
                }

                arguments.Result = error;
                return;
            }

            arguments.Result = module;
        }

        private void learnModuleComplete(Object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;

            if (result is string)
            {
                showGraph.Enabled = true;
                moduleID.BackColor = Color.Red;
                moduleID.Text = result as string;
                //toolStripStatusLabel1.Text += " [FAILED]";
                ErrorBox eb = new ErrorBox();
                eb.ShowDialog();
            }
            else
            {
                IModule module = result as IModule;
                moduleID.BackColor = Color.YellowGreen;
                moduleID.Text = module.ModuleID.ToString("X8");
                Room.Module = module;
                if(Room.Module.type == 1)
                {
                    Room.Module.NewTime = !module.NewTime;
                }
                //FormMain.calibration.openValve(valve);
                moduleIst.Text = String.Format("{0:F1} °C", Room.Module.Temperature);
                moduleSoll.Text = String.Format("{0:F1} °C", Math.Round(2*Room.Module.SetPoint)/2.0D);
                //toolStripStatusLabel1.Text += " [OK]";

                FormMain mainforms = (FormMain)(this.FindForm());
                ((FormMain)mainforms).flowLayoutPanel3.Controls[0].Hide();

            }
            moduleID.BackColor = Color.YellowGreen;
            moduleAnlernen.Enabled = true;
            /*
            HeaterPanelLearningEventHandler eventHandler = HeaterPanelLearning;

            if (eventHandler != null)
                eventHandler(false);
                */
        }

        public void learnModuleManual(IModule modulez)
        {
            object result = modulez;

            if (result is string)
            {
                moduleID.BackColor = Color.Red;
                moduleID.Text = result as string;
                //toolStripStatusLabel1.Text += " [FAILED]";
            }
            else
            {
                IModule module = result as IModule;
                moduleID.BackColor = Color.YellowGreen;
                moduleID.Text = module.ModuleID.ToString("X8");
                Room.Module = module;

                //FormMain.calibration.openValve(valve);
                moduleIst.Text = String.Format("{0:F1} °C", Room.Module.Temperature);
                moduleSoll.Text = String.Format("{0:F1} °C", Math.Round(2*Room.Module.SetPoint)/2.0D);
                //toolStripStatusLabel1.Text += " [OK]";
            }

            moduleID.BackColor = Color.YellowGreen;

            moduleAnlernen.Enabled = true;
            /*
            HeaterPanelLearningEventHandler eventHandler = HeaterPanelLearning;

            if (eventHandler != null)
                eventHandler(false);
                */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void moduleIst_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
