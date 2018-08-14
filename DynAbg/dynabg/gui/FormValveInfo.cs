using System;
using System.ComponentModel;
using System.Windows.Forms;
using FlowCalibrationInterface;
using System.Threading;
using System.Collections.Generic;

namespace DynAbg.gui
{
    internal partial class FormValveInfo : Form
    {
        private IValve valve;

        private FormValveInfo(IValve valve)
        {
            InitializeComponent();
			valve.PropertyChanged += new PropertyChangedEventHandler(valveChanged);
			this.valve = valve;
            showValues();
        }

        private void showValues()
        {
            textBoxID.Text = String.Format("{0:X8}", valve.ValveID);
            textBoxValue.Text = String.Format("{0} %", valve.Value);
            textBoxTemp.Text = String.Format("{0:F2} °C", valve.Temperature);
            textBoxSignal.Text = String.Format("-{0} dB", valve.Signal);
			numericUpDownHub.Value = Convert.ToDecimal(valve.Hub);
            checkBoxBattOK.Checked = valve.Battery;
			checkBoxCommunicationOK.Checked = valve.KommunikationOK;
        }

        private void valveChanged(object sender, PropertyChangedEventArgs args)
        {
			// Alles im try Block, da trotz des Entfernens des EventHandlers
			try
			{
				//Alles im GUI-Thread ausführen
				Invoke((MethodInvoker)delegate
				{
					IValve valve = sender as IValve;

					switch (args.PropertyName)
					{
						case "Value":
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
					}
				});

			}
			catch (Exception)
			{
				Console.WriteLine("Exception");
			}
		}

		private void buttonOpen_Click(object sender, EventArgs e)
		{
			Data.calibration.openValve(valve);
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Data.calibration.closeValve(valve);
		}

		private static Dictionary<IValve, FormValveInfo> dictionary = new Dictionary<IValve,FormValveInfo>();

		internal static FormValveInfo show(IValve valve)
		{
			FormValveInfo formValveInfo;

			if (dictionary.ContainsKey(valve))
			{
				formValveInfo = dictionary[valve];
				formValveInfo.BringToFront();
			}
			else
			{
				formValveInfo = new FormValveInfo(valve);
				formValveInfo.Show();

				dictionary.Add(valve, formValveInfo);
			}

			return formValveInfo;
		}

		internal static void close(IValve valve)
		{
			if (valve == null)
				return;

			if (!dictionary.ContainsKey(valve))
				return;

			FormValveInfo formValveInfo = dictionary[valve];
			formValveInfo.Close();
		}

		private void FormValveInfo_FormClosing(object sender, FormClosingEventArgs e)
		{
			valve.PropertyChanged -= new PropertyChangedEventHandler(valveChanged);
			dictionary.Remove(valve);
			valve = null;
		}

		private void buttonSetHub_Click(object sender, EventArgs e)
		{
			Data.calibration.setValveTravel(valve, Convert.ToDouble(numericUpDownHub.Value));
		}
    }
}
