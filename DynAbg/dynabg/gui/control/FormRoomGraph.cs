using System;
using System.ComponentModel;
using System.Windows.Forms;
using FlowCalibrationInterface;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using FlowCalibration;

namespace DynAbg.gui
{
    internal partial class FormRoomGraph : Form
    {
        private Room room;
        private bool normal;
        bool dailyreset = false;
        bool newday = false;

        private FormRoomGraph(Room room)
        {
            InitializeComponent();
            if(room.Module != null)
            {
                if(room.Module.type == 0)
                {
                    sendToModul.Enabled = true;
                    moduleLoad.Enabled = true;
                }
            }
            this.room = room;
            normal = true;
            for (int i = 0; i < 7; i++)
            {
                checkedListBox1.SetItemChecked(i, room.checkedList[i]);
            }
            showValues();
        }

        public DataGridView getDGV()
        {
            return this.dataGridView1;
        }

        public void sort()
        {
            room.sort();
        }

        private void showValues() // VentilID -> Wochentag -> Zeit -> Temperatur
        {
            //sort
            sort();
            if (room.dataRoom.Count > 1)
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 24;
                chart1.ChartAreas[0].AxisX.Interval = 4;
                chart1.ChartAreas[0].AxisX.Title = "Zeit in Stunden";
                chart1.ChartAreas[0].AxisY.Minimum = 10;
                chart1.ChartAreas[0].AxisY.Maximum = 30;
                chart1.ChartAreas[0].AxisY.Interval = 5;
                chart1.ChartAreas[0].AxisY.Title = "Temperatur in °C";

                foreach (Series test in chart1.Series)
                {
                    test.BorderWidth = 5;
                    test.MarkerStyle = MarkerStyle.Circle;
                    test.MarkerSize = 22;
                }
                //sort
                foreach (roomData temp in (normal ? room.dataRoom : room.nutzdataRoom))
                    // jeden ungeraden punkt?
                {
                    switch (temp.Wochentag)
                    {
                        case roomData.Day.Montag:
                            chart1.Series["Montag"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        case roomData.Day.Dienstag:
                            chart1.Series["Dienstag"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        case roomData.Day.Mittwoch:
                            chart1.Series["Mittwoch"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        case roomData.Day.Donnerstag:
                            chart1.Series["Donnerstag"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        case roomData.Day.Freitag:
                            chart1.Series["Freitag"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        case roomData.Day.Samstag:
                            chart1.Series["Samstag"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        case roomData.Day.Sonntag:
                            chart1.Series["Sonntag"].Points.AddXY(temp.calculateTime() / (double)3600, temp.Temperatur);
                            break;
                        default:
                            throw new InvalidOperationException("Tag Fehlerhaft.");
                    }
                }
            }

            //(int RaumID, Day Wochentag, byte Stunde, byte Minute, short Temperatur, List<IValve> Ventilliste)
            dataGridView1.Columns.Add("Punktindex", "Punktindex");
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.Columns.Add("RaumID", "RaumID");
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.Gray;

            DataGridViewComboBoxColumn cday = new DataGridViewComboBoxColumn();
            var listday = new List<string>() { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag" };
            cday.DataSource = listday;
            cday.HeaderText = "Day";
            cday.DataPropertyName = "Day";
            
            //dataGridView1.Columns.Add("Day", "Day (1-7)");


            //dataGridView1.Columns.Add(cday);


            //dataGridView1.Columns.Add("StundeMinute", "Time (hh:ss)");

            DataGridViewComboBoxColumn chour = new DataGridViewComboBoxColumn();
            var listchour = new List<string>() { };
            for (int i = 0; i <= 23; i++)
            {
                listchour.Add(i.ToString());
            }
            chour.DataSource = listchour;
            chour.HeaderText = "Stunde";
            chour.DataPropertyName = "Stunde";
            //dataGridView1.Columns.Add("Stunde", "Stunde (0-23)");

            DataGridViewComboBoxColumn cmin = new DataGridViewComboBoxColumn();
            var listcmin = new List<string>() {};
            for (int i = 0; i <= 5; i++)
            {
                listcmin.Add((i*10).ToString());
            }
            cmin.DataSource = listcmin;
            cmin.HeaderText = "Minute";
            cmin.DataPropertyName = "Minute";
            //dataGridView1.Columns.Add("Minute", "Minute (0-59)");

            DataGridViewComboBoxColumn ctemp = new DataGridViewComboBoxColumn();
            var listctemp = new List<string>() {  };
            for (int i = 15; i <= 25; i++)
            {
                listctemp.Add(i.ToString());
            }
            ctemp.DataSource = listctemp;
            ctemp.HeaderText = "Temp";
            ctemp.DataPropertyName = "Temp";
            //dataGridView1.Columns.Add("Temp", "Temp (0-40)");

            dataGridView1.Columns.AddRange(cday, chour, cmin, ctemp);


            int zahl = 1;
            int index = 0;
            foreach (roomData temp in (normal ? room.dataRoom : room.nutzdataRoom))
            {
                if ((index + 1) % 2 == 1)
                {
                    dataGridView1.Rows.Add(new object[] { zahl++, temp.RaumID, temp.Wochentag.ToString(), temp.Stunde.ToString(), temp.Minute.ToString(), temp.Temperatur.ToString() });
                }
                index++;
                // jeden ungeraden punkt?
            }

            //Default Width
            foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
            {
                dgvc.Width = 75;
            }
            dataGridView1.Columns[2].Width = 75 * 6 + 10 - 5 * 67;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 50;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 33;
            }
            hideAll();
            updateGraph();

        }


        private void roomChanged(object sender, PropertyChangedEventArgs args)
        {
            // Alles im try Block, da trotz des Entfernens des EventHandlers
            try
            {
                //Alles im GUI-Thread ausführen
                Invoke((MethodInvoker)delegate
                {
                    Room room = sender as Room;

                    switch (args.PropertyName)
                    {
                        /*case "Value":
                            textBoxValue.Text = String.Format("{0} %", valve.Value);
                            break;

                        case "Temperature":
                            textBoxTemp.Text = String.Format("{0:F2} °C", valve.Temperature);
                            break;

                        case "Signal":
                            textBoxSignal.Text = String.Format("-{0} dB", valve.Signal);
                            break;

                        case "Battery":
                            checkBoxBattOK.Checked = valve.Battery;
                            break;

                        case "KommunikationOK":
                            checkBoxCommunicationOK.Checked = valve.KommunikationOK;
                            break;
                            */
                    }
                });

            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }

        internal static FormRoomGraph show(Room room)
        {
            FormRoomGraph FormRoomGraph;

            FormRoomGraph = new FormRoomGraph(room);
            FormRoomGraph.Show();


            return FormRoomGraph;
        }

        private void FormRoomGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            room.PropertyChanged -= new PropertyChangedEventHandler(roomChanged);
            room = null;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void refresh()
        {
            if(DateTime.Now.Hour == 0)
            {
                if (newday == false)
                {
                    newday = true;
                    dailyreset = false;
                }
                if(dailyreset == false)
                {
                    room.dataRoom = room.nutzdataRoom;
                    dailyreset = true;
                }
            }
            else
            {
                newday = false;
            }
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            showValues();
        }

        private bool checkRestriction()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    int notneeded = 0;
                    if (row.Cells[3].Value != null && !int.TryParse(Convert.ToString(row.Cells[3].Value), out notneeded))
                    {
                        MessageBox.Show(this, "Mindestens Eingabefehler in der Datenbank. [String in Stunde]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (row.Cells[4].Value != null && !int.TryParse(Convert.ToString(row.Cells[4].Value), out notneeded))
                    {
                        MessageBox.Show(this, "Mindestens Eingabefehler in der Datenbank. [String in Minute]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (row.Cells[5].Value != null && !int.TryParse(Convert.ToString(row.Cells[5].Value), out notneeded))
                    {
                        MessageBox.Show(this, "Mindestens Eingabefehler in der Datenbank. [String in Temperatur]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    //< 8 && Convert.ToByte(row.Cells[3].Value), Convert.ToByte(row.Cells[4].Value), short.Parse(Convert.ToString(row.Cells[5].Value)))){

                    if (!int.TryParse(Convert.ToString(row.Cells[2].Value), out notneeded))
                    {
                        if (!(Convert.ToString(row.Cells[2].Value) == "Montag" || Convert.ToString(row.Cells[2].Value) == "Dienstag" || Convert.ToString(row.Cells[2].Value) == "Mittwoch" || Convert.ToString(row.Cells[2].Value) == "Donnerstag" || Convert.ToString(row.Cells[2].Value) == "Freitag" || Convert.ToString(row.Cells[2].Value) == "Samstag" || Convert.ToString(row.Cells[2].Value) == "Sonntag")) {
                            MessageBox.Show(this, "Mindestens Eingabefehler in der Datenbank. [Falscher String oder Leer in Tagspalte]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                    else if (!(0 < Convert.ToInt32(row.Cells[2].Value) && Convert.ToInt32(row.Cells[2].Value) < 8))
                    {
                        MessageBox.Show(this, "Mindestens ein Wertigkeitsfehler in der Datenbank. [Tag Spalte | Min: 1 (Montag), Max: 7(Sonntag) oder den Tag eingeben]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (!(Convert.ToByte(row.Cells[3].Value) >= 0 && Convert.ToByte(row.Cells[3].Value) <= 23))
                    {
                        MessageBox.Show(this, "Mindestens ein Wertigkeitsfehler in der Datenbank. [Stundenspalte | Min: 0, Max: 23]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (!(Convert.ToByte(row.Cells[4].Value) >= 0 && Convert.ToByte(row.Cells[4].Value) < 60))
                    {
                        MessageBox.Show(this, "Mindestens ein Wertigkeitsfehler in der Datenbank. [Minutenspalte | Min: 0, Max: 59]", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (row.Cells[5].Value != null && !(Convert.ToByte(row.Cells[5].Value) >= 0 && Convert.ToByte(row.Cells[5].Value) <= 40))
                    {
                        MessageBox.Show(this, "Mindestens ein Wertigkeitsfehler in der Datenbank. [Temperatur Spalte | Min: 0, Max: 40] ", "Datenbankfehler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
            }

            return true;
        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            if (!checkRestriction())
            {
                return;
            }
            room.dataRoom = new List<roomData>();
            room.nutzdataRoom = new List<roomData>();

            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                count++;
                if (count != dataGridView1.Rows.Count)
                {
                    /*
                     * dataGridView1.Columns.Add("Punktindex", "Punktindex");
                     * dataGridView1.Columns.Add("RaumID", "RaumID");
                     * dataGridView1.Columns.Add("Day", "Day");
                     * dataGridView1.Columns.Add("Stunde", "Stunde");
                     * dataGridView1.Columns.Add("Minute", "Minute");
                     * dataGridView1.Columns.Add("Temp", "Temp");
                    */

                    room.addData(room.ID, (roomData.Day)Enum.Parse(typeof(roomData.Day), Convert.ToString(row.Cells[2].Value)), row.Cells[3].Value != null ? Convert.ToByte(row.Cells[3].Value) : (byte)0, row.Cells[4].Value != null ? Convert.ToByte(row.Cells[4].Value) : (byte)0, row.Cells[5].Value != null ? short.Parse(Convert.ToString(row.Cells[5].Value)) : (short)15);

                    room.nutzaddData(room.ID, (roomData.Day)Enum.Parse(typeof(roomData.Day), Convert.ToString(row.Cells[2].Value)), row.Cells[3].Value != null ? Convert.ToByte(row.Cells[3].Value) : (byte)0, row.Cells[4].Value != null ? Convert.ToByte(row.Cells[4].Value) : (byte)0, row.Cells[5].Value != null ? short.Parse(Convert.ToString(row.Cells[5].Value)) : (short)15);
                    //room.addData(room.ID, row.Cells[2].Value != null ? (int.TryParse(Convert.ToString(row.Cells[2].Value), out int notneeded) == true ? (roomData.Day)Convert.ToInt32(row.Cells[2].Value) : (roomData.Day)Enum.Parse(typeof(roomData.Day), Convert.ToString(row.Cells[2].Value))) : (roomData.Day)1, row.Cells[3].Value != null ? Convert.ToByte(row.Cells[3].Value) : (byte)0, row.Cells[4].Value != null ? Convert.ToByte(row.Cells[4].Value) : (byte)0, row.Cells[5].Value != null ? short.Parse(Convert.ToString(row.Cells[5].Value)) : (short)15);
                    //room.nutzaddData(room.ID, row.Cells[2].Value != null ? (int.TryParse(Convert.ToString(row.Cells[2].Value), out int notneeded2) == true ? (roomData.Day)Convert.ToInt32(row.Cells[2].Value) : (roomData.Day)Enum.Parse(typeof(roomData.Day), Convert.ToString(row.Cells[2].Value))) : (roomData.Day)1, row.Cells[3].Value != null ? Convert.ToByte(row.Cells[3].Value) : (byte)0, row.Cells[4].Value != null ? Convert.ToByte(row.Cells[4].Value) : (byte)0, row.Cells[5].Value != null ? short.Parse(Convert.ToString(row.Cells[5].Value)) : (short)15);
                }
            }
            sort();
            int indexx = 0;
            List<roomData> newtemp = new List<roomData>();
            foreach (roomData rd in room.dataRoom)
            {
                newtemp.Add(rd);
                if (indexx + 1 == room.dataRoom.Count)
                {
                    if (rd.Stunde == 23 && rd.Minute == 50)
                    {
                        //nichts
                    }
                    else
                    {
                        newtemp.Add(new roomData(rd.RaumID, rd.Wochentag, (byte)23, (byte)50, rd.Temperatur));
                    }
                }
                else
                {
                    roomData rd2 = room.dataRoom[indexx + 1];
                    if (rd.Wochentag == rd2.Wochentag)
                    {
                        newtemp.Add(new roomData(rd.RaumID, rd.Wochentag, rd2.Stunde, rd2.Minute, rd.Temperatur));
                    }
                    else
                    {
                        if (rd.Stunde == 23 && rd.Minute == 50)
                        {
                            //nichts
                        }
                        else
                        {
                            newtemp.Add(new roomData(rd.RaumID, rd.Wochentag, (byte)23, (byte)50, rd.Temperatur));
                        }
                    }
                }
                indexx++;
            }
            room.dataRoom = newtemp;
            room.nutzdataRoom = newtemp;
            refresh();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (ClientRectangle.Width / 2 < 75 * 6)
            {
                var width = (ClientRectangle.Width / 2) + 50;
                dataGridView1.Left = 0;
                dataGridView1.Width = width;
                chart1.Left = width;
                chart1.Width = ClientRectangle.Width - width;
                sendToModul.Left = ClientRectangle.Width - 120;
                sendToModul.Top = ClientRectangle.Height - 220;
                moduleLoad.Left = ClientRectangle.Width - 120;
                moduleLoad.Top = ClientRectangle.Height - 180;
                switchbutton.Left = ClientRectangle.Width - 120;
                switchbutton.Top = ClientRectangle.Height - 140;
                buttonSaveAll.Left = ClientRectangle.Width - 120;
                buttonSaveAll.Top = ClientRectangle.Height - 100;
                buttonRefresh.Left = ClientRectangle.Width - 120;
                buttonRefresh.Top = ClientRectangle.Height - 60;
                checkedListBox1.Left = ClientRectangle.Width - 120;
                checkedListBox1.Top = ClientRectangle.Height - 400;
            }
            else
            {
                var width = ClientRectangle.Width - 75 * 6;
                dataGridView1.Left = 0;
                dataGridView1.Width = 75 * 6 + 10;
                chart1.Left = 75 * 6 + 10;
                chart1.Width = width - 10;
                sendToModul.Left = ClientRectangle.Width - 120;
                sendToModul.Top = ClientRectangle.Height - 220;
                moduleLoad.Left = ClientRectangle.Width - 120;
                moduleLoad.Top = ClientRectangle.Height - 180;
                switchbutton.Left = ClientRectangle.Width - 120;
                switchbutton.Top = ClientRectangle.Height - 140;
                buttonSaveAll.Left = ClientRectangle.Width - 120;
                buttonSaveAll.Top = ClientRectangle.Height - 100;
                buttonRefresh.Left = ClientRectangle.Width - 120;
                buttonRefresh.Top = ClientRectangle.Height - 60;
                checkedListBox1.Left = ClientRectangle.Width - 120;
                checkedListBox1.Top = ClientRectangle.Height - 400;
            }
        }

        void hideAll()
        {
            foreach (Series ser in chart1.Series)
            {
                ser.Enabled = false;
            }
        }

        void updateGraph()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    chart1.Series[checkedListBox1.Items[i].ToString()].Enabled = true;
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(evalList), null);
            hideAll();
            updateGraph();
        }

        private void evalList()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; ++i)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    room.checkedList[i] = true;
                }
                else
                {
                    room.checkedList[i] = false;
                }
            }
        }

        private void switchbutton_Click(object sender, EventArgs e)
        {
            if (normal)
            {
                switchbutton.Text = "Nutzprofil";
                normal = false;
                refresh();
            }
            else
            {
                switchbutton.Text = "Sollprofil";
                normal = true;
                refresh();
            }
        }

        private void moduleLoad_Click(object sender, EventArgs e)
        {
            room.dataRoom = convertlist(room.Module.timeprog);
            room.nutzdataRoom = convertlist(room.Module.timeprog);
            refresh();
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
                            roomdatas.Add(new roomData(room.ID, (roomData.Day)temp, tptemp.sStunde, tptemp.sMinute, (short)Math.Round(room.Module.EcoSetPoint)));
                        }
                        roomdatas.Add(new roomData(room.ID, (roomData.Day)temp, tptemp.sStunde, tptemp.sMinute, (short)Math.Round(room.Module.ComSetPoint)));
                        roomdatas.Add(new roomData(room.ID, (roomData.Day)temp, tptemp.eStunde, tptemp.eMinute, (short)Math.Round(room.Module.ComSetPoint)));
                        if (tptemp.sStunde == 23 && tptemp.sMinute == 59)
                        {

                        }
                        else
                        {
                            roomdatas.Add(new roomData(room.ID, (roomData.Day)temp, tptemp.eStunde, tptemp.eMinute, (short)Math.Round(room.Module.EcoSetPoint)));
                        }
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                //Anfang und endpunkte
                bool exist = false;
                foreach(roomData rd in roomdatas)
                {
                    if((int) rd.Wochentag == i && rd.Stunde == 0 && rd.Minute == 0)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    roomdatas.Add(new roomData(room.ID, (roomData.Day)i, 0, 0, (short)Math.Round(room.Module.EcoSetPoint)));
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
                    roomdatas.Add(new roomData(room.ID, (roomData.Day)i, 23, 59, (short)Math.Round(room.Module.EcoSetPoint)));
                }
            }
            return roomdatas;
        }

        public short newecosetpoint;
        public short newcomsetpoint;
        private void sendToModul_Click(object sender, EventArgs e)
        {
            if (room.Module != null)
            {
                buttonSaveAll_Click(sender, e);
                room.Module.timeprogsend = convertlist(room.dataRoom);
                room.Module.EcoSetPoint = newecosetpoint;
                room.Module.ComSetPoint = newcomsetpoint;
                room.Module.roomsend = true;
                room.Module.timesend = true;
            }
        }

        private List<timeProgram> convertlist(List<roomData> dataR)
        {
            List<timeProgram> tp = new List<timeProgram>();
            newecosetpoint = 0;
            newcomsetpoint = 0;
            int j = 1;
            for(int i = 0; i < dataR.Count; i++)
            {
                if((int)(dataR[i].Wochentag) == j)
                {
                    tp.Add(new timeProgram((byte)0, (byte)0, (byte)0, (byte)0, (byte)(j + 2), (byte)0, (byte)1));
                    j++;
                }
                if(newecosetpoint == 0)
                {
                    newecosetpoint = dataR[i].Temperatur;
                }
                if(newecosetpoint != dataR[i].Temperatur)
                {
                    newcomsetpoint = dataR[i].Temperatur;
                }
                if (dataR[i].Temperatur == newecosetpoint)
                {
                    // fuege erstmal nichts hinzu.
                }
                else //Im Falle, dass ein Punkt nicht auf Eco Mode ist
                {
                    tp.Add(new timeProgram((byte) dataR[i+1].Minute, (byte)dataR[i+1].Stunde, (byte)dataR[i].Minute, (byte)dataR[i].Stunde, (byte)((int)(dataR[i].Wochentag)+2),(byte)0,(byte)0));
                    i++;
                }
            }
            return tp;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
