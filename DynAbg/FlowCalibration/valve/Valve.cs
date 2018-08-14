
using System.ComponentModel;
using FlowCalibrationInterface;
using System;

namespace FlowCalibration
{
    // Valve has 2 commands,open and closing the valve
    internal enum VALVE_COMMAND
    {
        CLOSE,
        OPEN
    }

    //TODO raus

    /*  internal enum VALVE_POWER
    {
        //glaube boolean wäre besser
        POWER_ON, POWER_OFF
    }*/

    public class Valve : IValve
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Learned { get; set; }
        internal VALVE_COMMAND Command { get; set; }

        //TODO raus
        //internal VALVE_POWER Power { get; set; }
        public int FunctionID { get; set; }

        internal double[] Voreinstellungen_vdi { get; set; }
        internal double[] Schrittweite_vdi { get; set; }
        internal double[] Xp_vdi { get; set; }
        internal double[] Kv_vdi { get; set; }
        internal double[] Kvs_vdi { get; set; }
        public DateTime dateTimeReceived { get; set; }
        internal int CommErrorCounter { get; set; }
        internal bool kommunikationOK;
        internal byte travel = 40;


        private bool writebool;
        private int value;
        private double temperature;
        private int desiredvalue;
        private int tempmode;
        private double druckverlust;
        private double voreinstellung;
        private bool battery;
        private int signal;
        private string manufacturer;
        private string tga;
        private double desiredVolumeFlow;

        public double PAnteil { get; set; }
        public double IAnteil { get; set; }

        //TODO neu nach einigen hin und her doch der boolean
        internal bool power_off = false;

        //TODO neu mal gucken ob das so bleibt; soll in sendAnswer gesetzt werden wenn power_off = true ist.
		internal bool power_off_transmitted = false;

        public long ValveID { get; set; }

        //basically a public set ValveID
        public Valve(long valveID)
        {
            this.ValveID = valveID;
        }

        //constructor
        public Valve(long valveID, int signal, bool battery, double temperature, int value)
        {
            this.ValveID = valveID;
            this.signal = signal;
            this.battery = battery;
            this.temperature = temperature;
            this.value = value;
            this.desiredvalue = 0;
            this.tempmode = 0;
            this.writebool = false;
            this.PAnteil = 0;
            this.IAnteil = 0;
        }

        //sets the propertychanged to the named property
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                //method(sender, argument)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //get and set for value, whereas value also calls onpropertychanged oeffnungsgrad 100prozent bedeutet offen
        public int Value
        {
            get
            {
                return value;
            }

            internal set
            {
				bool changed = this.value != value;
                this.value = value;

				if(changed)
					OnPropertyChanged("Value");
            }
        }

        //get and set for temperature, whereas value also calls onpropertychanged
        public double Temperature
        {
            get
            {
                return temperature;
            }

            internal set
            {
				bool changed = this.temperature != value;
                this.temperature = value;

				if (changed)
					OnPropertyChanged("Temperature");
            }
        }

        public int DesiredValue
        {
            get
            {
                return desiredvalue;
            }

            set
            {
                bool changed = this.desiredvalue != value;
                this.desiredvalue = value;

                if (changed)
                    OnPropertyChanged("DesiredValue");
            }
        }

        public int TempMode
        {
            get
            {
                return tempmode;
            }

            internal set
            {
                bool changed = this.tempmode != value;
                this.tempmode = value;

                if (changed)
                    OnPropertyChanged("TempMode");
            }
        }

        //get and set for Pressureloss, whereas value also calls onpropertychanged
        public double Druckverlust
        {
            get
            {
                return druckverlust;
            }

            internal set
            {
				bool changed = this.druckverlust != value;
				this.druckverlust = value;

				if (changed)
					OnPropertyChanged("DP_Ventil");
            }
        }

        //get and set for presets, whereas value also calls onpropertychanged
        public double Voreinstellung
        {
            get
            {
                return voreinstellung;
            }

            internal set
            {
				bool changed = this.voreinstellung != value;
				this.voreinstellung = value;

				if (changed)
					OnPropertyChanged("Voreinstellung");
            }
        }

        //get and set for battery, whereas value also calls onpropertychanged
        public bool Battery
        {
            get
            {
                return battery;
            }

            internal set
            {
				bool changed = this.battery != value;
				this.battery = value;

				if (changed)
					OnPropertyChanged("Battery");
            }
        }

        //get and set for signal, whereas value also calls onpropertychanged
        public int Signal
        {
            get
            {
                return signal;
            }

            internal set
            {
				bool changed = this.signal != value;
				signal = value;

				if (changed)
					OnPropertyChanged("Signal");
            }
        }

        //get and set for manufacturer, whereas value also calls onpropertychanged
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }

            set
            {
				bool changed = false;

				if (manufacturer != null && value != null)
					changed = (this.manufacturer == null && value != null) || (value == null && this.manufacturer != null) || !this.manufacturer.Equals(value);

				manufacturer = value;

				if (changed)
					OnPropertyChanged("Manufacturer");
            }
        }

        //get and set for TGA(targa image file), whereas value also calls onpropertychanged
        public string TGA
        {
            get
            {
                return tga;
            }

            set
            {
				bool changed = false;

				if (tga != null && value != null)
					changed = (this.tga == null && value != null) || (value == null && this.tga != null) || !this.tga.Equals(value);

				tga = value;

				if (changed)
					OnPropertyChanged("TGA");
            }
        }

        //get and set for desiredvolumeflow, whereas value also calls onpropertychanged
        public double DesiredVolumeFlow
        {
            get
            {
                return desiredVolumeFlow;
            }

            set
            {
				bool changed = this.desiredVolumeFlow != value;
				this.desiredVolumeFlow = value;

				if (changed)
					OnPropertyChanged("DesiredVolumeFlow");
            }
        }

        //get and set for communicationsokness, whereas value also calls onpropertychanged
        public bool KommunikationOK
		{
			get
			{
				return kommunikationOK;
			}

			set
			{
				bool changed = this.kommunikationOK != value;
				this.kommunikationOK = value;

				if (changed)
					OnPropertyChanged("KommunikationOK");
			}
		}

        public bool WriteBool
        {
            get
            {
                return writebool;
            }

            set
            {
                bool changed = this.writebool != value;
                this.writebool = value;

                if (changed)
                    OnPropertyChanged("WriteBool");
            }
        }

        //gets the hub speed divided by 10?
        public double Hub
		{
			get
			{
				return travel / 10;
			}
		}
    }
}
