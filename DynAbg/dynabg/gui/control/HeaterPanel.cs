using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FlowCalibrationInterface;
using System.Threading;
using DynAbg.valve;

namespace DynAbg.gui.control
{
	delegate void HeaterPanelAddedEventHandler(HeaterPanel heaterPanel);
	delegate void HeaterPanelDeletedEventHandler(HeaterPanel heaterPanel);
	delegate void HeaterPanelCopiedEventHandler(HeaterPanel heaterPanel);
	delegate void HeaterPanelLearningEventHandler(bool learning);

    internal partial class HeaterPanel : UserControl
    {
        public event HeaterPanelDeletedEventHandler HeaterPanelDeleted;
		public event HeaterPanelCopiedEventHandler HeaterPanelCopied;
		public event HeaterPanelLearningEventHandler HeaterPanelLearning;

        public Heater Heater { get; private set; }

		private FormValveInfo formValveInfo = null;

        

        public HeaterPanel(Heater heater)
        {
            InitializeComponent();
			this.Disposed += new EventHandler(HeaterPanel_Disposed);
			this.Heater = heater;

            label1.Text = String.Format("{0}", Heater.ID + 1);
			//textBoxSoll.Text = String.Format("{0:0}", Heater.DesiredVolumeFlow);

            Heater.PropertyChanged += new PropertyChangedEventHandler(heaterPropertyChanged);
        }

		private void HeaterPanel_Disposed(object sender, EventArgs e)
		{
			FormValveInfo.close(Heater.Valve);
		}

        private void heaterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
			if (sender is IValve)
			{
				IValve valve = sender as IValve;

				switch (e.PropertyName)
				{
					// Die Voreinstellung wird von einem anderen Thread eingestellt,
					// deshalb muss hier mit BeginInvoke gearbeitet werden
					// Vielleicht von anderer Stelle aus erledigen
					case "Voreinstellung":
						this.BeginInvoke(((MethodInvoker)delegate
						{
							//textBoxVE.Text = String.Format("{0:0.0}", valve.Voreinstellung);
						}));

						break;

					case "KommunikationOK":
					case "Battery":
					case "Signal":
						this.BeginInvoke(((MethodInvoker)delegate
						{
                            if(valve.Battery && valve.KommunikationOK && valve.Signal < 85)
                            {
                                textBoxID.BackColor = Color.YellowGreen;
                            }
                            else if(valve.Battery && valve.KommunikationOK)
                            {
                                textBoxID.BackColor = Color.Orange;
                            }
                            else
                            {
                                textBoxID.BackColor = Color.Red;
                            }
						}));

						break;
                    case "Value":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            isValue.Text = String.Format("{0} %", valve.Value);
                        }));

                        break;

                    case "Temperature":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            isTemp.Text = String.Format("{0:F1} °C", valve.Temperature);
                        }));

                        break;

                    case "Valve":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            if (valve.Battery && valve.KommunikationOK && valve.Signal < 85)
                            {
                                textBoxID.BackColor = Color.YellowGreen;
                            }
                            else if (valve.Battery && valve.KommunikationOK)
                            {
                                textBoxID.BackColor = Color.Orange;
                            }
                            else
                            {
                                textBoxID.BackColor = Color.Red;
                            }
                            isValue.Text = String.Format("{0} %", valve.Value);
                            isTemp.Text = String.Format("{0:F1} °C", valve.Temperature);
                        }));

                        break;
                    case "DesiredValue":
                        this.BeginInvoke(((MethodInvoker)delegate
                        {
                            numericSoll.Value = valve.DesiredValue;
                            numericSoll.Text = String.Format("{0:F2}", valve.DesiredValue);
                        }));
                        break;

                    case "True":
                        numericSoll.ReadOnly = false;
                        numericSoll.Increment = 1;
                        break;

                    case "False":
                        numericSoll.ReadOnly = true;
                        numericSoll.Increment = 0;
                        break;
                }
			}
			else
			{
				switch (e.PropertyName)
				{
					case "ID":
						label1.Text = String.Format("{0}", Heater.ID + 1);
						break;

					case "DesiredVolumeFlow":
						//textBoxSoll.Text = String.Format("{0:0}", Heater.DesiredVolumeFlow);
						break;

                    case "True":
                        numericSoll.ReadOnly = false;
                        numericSoll.Increment = 1;
                        break;

                    case "False":
                        numericSoll.ReadOnly = true;
                        numericSoll.Increment = 0;
                        break;

                    case "Valve":
						if (Heater.Valve == null)
						{
							if (formValveInfo != null)
								formValveInfo.Close();

                            buttonLearn.Enabled = true;
							textBoxID.Text = "";
							textBoxID.BackColor = System.Drawing.SystemColors.Window;
						}

						break;

				}
			}
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Heater.PropertyChanged -= new PropertyChangedEventHandler(heaterPropertyChanged);

            if (HeaterPanelDeleted != null)
                HeaterPanelDeleted(this);
        }

        public void buttonLearn_Click(object sender, EventArgs e)
        {
			if (Data.calibration == null || !Data.calibration.Initialized)
            {
                MessageBox.Show(this, "Das Anlernen ist nicht möglich,\nda das TCM Modul nicht initialisiert wurde.", "Anlernen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

			if (Heater.Valve != null)
			{
				Data.calibration.deactivateValve(Heater.Valve);
				FormValveInfo.close(Heater.Valve);
                numericSoll.Value = 0;
			}

			buttonDelete.Enabled = false;
            buttonLearn.Enabled = false;
            textBoxID.BackColor = System.Drawing.SystemColors.Window;
            textBoxID.Text = "Lerne ...";
			
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(learnValve);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(learnValveComplete);
            worker.RunWorkerAsync();

			HeaterPanelLearningEventHandler eventHandler = HeaterPanelLearning;

			if (eventHandler != null)
				eventHandler(true);
        }

        private void learnValve(object source, DoWorkEventArgs arguments)
        {
            string error = null;
            IValve valve = null;

            try
            {
				valve = Data.calibration.anlernen(120000);
            }
            catch (CalibrationException exception)
            {
                switch (exception.error)
                {
                    case Error.NO_FREE_IDS:
                        error = "Keine freien IDs vorhanden";
                        break;

                    case Error.NO_LEARN_MESSAGE:
                        error = "Kein Anlerntelegramm empfangen";
                        break;

                    default:
                        Console.WriteLine("Unknown exception: {0}", exception.error);
                        error = "Unbekannter Fehler";
                        break;
                }

                arguments.Result = error;
                return;
            }

            arguments.Result = valve;
        }

        private void learnValveComplete(Object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;

            if (result is string)
            {
                textBoxID.BackColor = Color.Red;
                textBoxID.Text = result as string;
                //toolStripStatusLabel1.Text += " [FAILED]";
            }
            else
            {
                IValve valve = result as IValve;
                textBoxID.BackColor = Color.YellowGreen;
                textBoxID.Text = valve.ValveID.ToString("X8");
                Heater.Valve = valve;
                Heater.Valve.DesiredValue = Convert.ToInt32(numericSoll.Value);

				//FormMain.calibration.openValve(valve);
                isTemp.Text = String.Format("{0:F1} °C", Heater.Valve.Temperature);
                isValue.Text = String.Format("{0} %", Heater.Valve.Value);
                //toolStripStatusLabel1.Text += " [OK]";

                FormMain mainforms = (FormMain)(this.FindForm());
                if (((FormMain)mainforms).flowLayoutPanel3.Controls.Count != 0)
                {
                    ((FormMain)mainforms).flowLayoutPanel3.Controls[0].Hide();
                }
            }

			buttonDelete.Enabled = true;
			buttonLearn.Enabled = true;

			HeaterPanelLearningEventHandler eventHandler = HeaterPanelLearning;

			if (eventHandler != null)
				eventHandler(false);
        }

        public void learnValveManual(IValve valv)
        {
            object result = valv;

            if (result is string)
            {
                textBoxID.BackColor = Color.Red;
                textBoxID.Text = result as string;
                //toolStripStatusLabel1.Text += " [FAILED]";
            }
            else
            {
                IValve valve = result as IValve;
                textBoxID.BackColor = Color.YellowGreen;
                textBoxID.Text = valve.ValveID.ToString("X8");
                Heater.Valve = valve;

                //FormMain.calibration.openValve(valve);
                isTemp.Text = String.Format("{0:F1} °C", Heater.Valve.Temperature);
                isValue.Text = String.Format("{0} %", Heater.Valve.Value);
                //toolStripStatusLabel1.Text += " [OK]";
            }

            buttonDelete.Enabled = true;
            buttonLearn.Enabled = true;

            HeaterPanelLearningEventHandler eventHandler = HeaterPanelLearning;

            if (eventHandler != null)
                eventHandler(false);
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
			formValveInfo = FormValveInfo.show(Heater.Valve);
        }

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			if (HeaterPanelCopied != null)
				HeaterPanelCopied(this);
		}


        private void numericSoll_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown2 = sender as NumericUpDown;
            if (Heater.Valve != null)
            {
                Data.calibration.changeDesiredValue(Heater.Valve, Convert.ToInt32(numericUpDown2.Value));
            }
        }
        

        private void Mode_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown3 = sender as NumericUpDown;
            Data.calibration.changeTempMode(Heater.Valve, Convert.ToInt32(numericUpDown3.Value));
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
