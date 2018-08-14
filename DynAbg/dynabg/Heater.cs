using System.ComponentModel;
using FlowCalibrationInterface;
using System.Runtime.Serialization;
using DynAbg.valve;
using System.Threading;
using System;

namespace DynAbg
{
    internal class Heater
    {
        public event PropertyChangedEventHandler PropertyChanged;

		//[DataMember]
        private IValve valve;
        [DataMember]
        private int id;
		[DataMember]
        private double defaultSettings = 0;
		[DataMember]
		private ValveInfo valveInfo;

        // Tester
        public Thread contThread;

        public Heater(IValve valve)
		{
			this.Valve = valve;
		}

        public double DefaultSettings
        {
            get
            {
                return defaultSettings;
            }

            set
            {
                defaultSettings = value;
				OnPropertyChanged("DefaultSettings");
            }
        }

        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        
        public void setManual(bool b)
        {
            if (b)
            {
                OnPropertyChanged("True");
            }
            else
            {
                OnPropertyChanged("False");
            }
        }

        internal IValve Valve
        {
            get
            {
                return valve;
            }

            set
            {
				if (valve != null)
					valve.PropertyChanged -= new PropertyChangedEventHandler(valvePropertyChanged);

				if (valveInfo != null)
				{
					value.Manufacturer = valveInfo.Manufacturer;
					value.TGA = valveInfo.TGA;
				}

				if (value != null)
					value.PropertyChanged += new PropertyChangedEventHandler(valvePropertyChanged);

				this.valve = value;
                OnPropertyChanged("Valve");
            }
        }

		public ValveInfo ValveInfo
		{
			get
			{
				return valveInfo;
			}

			set
			{
				valveInfo = value;

				if (valve != null)
				{
					if (value != null)
					{
						valve.Manufacturer = value.Manufacturer;
						valve.TGA = value.TGA;
					}
					else
					{
						valve.Manufacturer = null;
						valve.TGA = null;
					}
				}

				OnPropertyChanged("ValveInfo");
			}
		}

        public Heater(int id)
        {
            this.ID = id;
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

		private void valvePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(sender, e);
		}
    }
}
