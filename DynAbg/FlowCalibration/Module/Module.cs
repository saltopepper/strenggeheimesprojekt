
using System.ComponentModel;
using FlowCalibrationInterface;
using System;
using FlowCalibration.tcm;
using System.Collections.Generic;

namespace FlowCalibration
{
    // Module has 2 commands,open and closing the Module
    internal enum Module_COMMAND
    {
        CLOSE,
        OPEN
    }

    //TODO raus

    /*  internal enum Module_POWER
    {
        //glaube boolean wäre besser
        POWER_ON, POWER_OFF
    }*/

    public class Module : IModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Learned { get; set; }

        public int FunctionID { get; set; }
      
        public DateTime dateTimeReceived { get; set; }
        internal int CommErrorCounter { get; set; }

        bool kommunikationOK;
        
        private double temperature;
        private int desiredvalue;
        
        private int signal;
        private string manufacturer;

        private bool windowopen;
        private double setpoint;
        private double ecosetpoint;
        private double comsetpoint;
        private int intervall;

        private bool newtime;
        private bool newmsg;

        //last 5 Message Types und ob Nachricht versendet werden soll

        public int type { get; set; }
        public TCMMessage generalmessage { get; set; }
        public TCMMessage datamessage { get; set; }
        public bool datasend { get; set; }
        public bool dataask { get; set; }
        public TCMMessage configmessage { get; set; }
        public bool configsend { get; set; }
        public bool configask { get; set; }
        public TCMMessage roommessage { get; set; }
        public bool roomsend { get; set; }
        public bool roomask { get; set; }
        public TCMMessage timemessage { get; set; }
        public bool timesend { get; set; }
        public bool timeask { get; set; }
        public bool timeask2 { get; set; }

        public List<timeProgram> timeprog { get; set; }

        public bool first { get; set; }

        public bool incomplete { get; set; }

        public bool timedone { get; set; }


        public long ModuleID { get; private set; }

        public List<timeProgram> timeprogsend { get; set; }

        //basically a public set ModuleID
        public Module(long ModuleID)
        {
            timedone = false;
            this.ModuleID = ModuleID;
            datasend = false;
            configsend = false;
            roomsend = false;
            timesend = false;
            dataask = false;
            configask = false;
            roomask = false;
            timeask = false;
            first = false;
            timeask2 = true;
            newtime = false;
            incomplete = false; 
            timeprog = new List<timeProgram>();
            timeprogsend = new List<timeProgram>();
            newmsg = false;
            type = 0;
        }

        //constructor
        public Module(long ModuleID, int signal, double temperature)
        {
            timedone = false;
            this.ModuleID = ModuleID;
            this.signal = signal;
            this.temperature = temperature;
            this.desiredvalue = 0;
            this.setpoint = 0;
            datasend = false;
            configsend = false;
            roomsend = false;
            timesend = false;
            dataask = false;
            configask = false;
            roomask = false;
            timeask = false;
            first = false;
            newmsg = false;
            incomplete = false;
            timeask2 = true;
            newtime = false;
            type = 0;
            timeprog = new List<timeProgram>();
            timeprogsend = new List<timeProgram>();
        }


        public void clearTime()
        {
            this.timeprog.Clear();
        }

        public void addTime(byte emin, byte estun, byte smin, byte sstun, byte per, byte mod, byte del)
        {
            timeprog.Add(new timeProgram(emin, estun, smin, sstun, per, mod, del));
        }

        //sets the propertychanged to the named property
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                //method(sender, argument)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //get and set for value, whereas value also calls onpropertychanged oeffnungsgrad 100prozent bedeutet offen
        

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

            internal set
            {
                bool changed = this.desiredvalue != value;
                this.desiredvalue = value;

                if (changed)
                    OnPropertyChanged("DesiredValue");
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

        public bool WindowOpen
        {
            get
            {
                return windowopen;
            }

            set
            {
                bool changed = this.windowopen != value;
                this.windowopen = value;

                if (changed)
                    OnPropertyChanged("WindowOpen");
            }
        }

        public double SetPoint
        {
            get
            {
                return setpoint;
            }

            set
            {
                bool changed = this.setpoint != value;
                this.setpoint = value;

                if (changed)
                    OnPropertyChanged("SetPoint");
            }
        }

        public int Intervall
        {
            get
            {
                return intervall;
            }

            set
            {
                bool changed = this.intervall != value;
                this.intervall = value;

                if (changed)
                    OnPropertyChanged("Intervall");
            }
        }

        public double EcoSetPoint
        {
            get
            {
                return ecosetpoint;
            }

            set
            {
                bool changed = this.ecosetpoint != value;
                this.ecosetpoint = value;

                if (changed)
                    OnPropertyChanged("EcoSetPoint");
            }
        }

        public double ComSetPoint
        {
            get
            {
                return comsetpoint;
            }

            set
            {
                bool changed = this.comsetpoint != value;
                this.comsetpoint = value;

                if (changed)
                    OnPropertyChanged("ComSetPoint");
            }
        }

        public bool NewTime
        {
            get
            {
                return newtime;
            }

            set
            {
                bool changed = this.newtime != value;
                this.newtime = value;

                if (changed)
                    OnPropertyChanged("NewTime");
            }
        }

        public bool NewMSG
        {
            get
            {
                return newmsg;
            }

            set
            {
                bool changed = this.newmsg != value;
                this.newmsg = value;

                if (changed)
                    OnPropertyChanged("NewMSG");
            }
        }
    }
}
