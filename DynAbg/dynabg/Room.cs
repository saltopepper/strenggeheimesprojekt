using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DynAbg.Generic;
using System.Collections.Generic;
using FlowCalibrationInterface;
using FlowCalibration;

namespace DynAbg
{
	internal class Room
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

        private IModule module;

        internal IModule Module
        {
            get
            {
                return module;
            }

            set
            {
                if (module != null)
                    module.PropertyChanged -= new PropertyChangedEventHandler(modulePropertyChanged);

                if (value != null)
                    value.PropertyChanged += new PropertyChangedEventHandler(modulePropertyChanged);

                this.module = value;
                OnPropertyChanged("Module");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void modulePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(sender, e);
        }

        #endregion Events

        #region Properties

        [DataMember]
		public string Name
		{
			get
			{
				return (name == null || name.Length == 0 ? "Unbenannt" : name);
			}

			set
			{
				name = value;

				PropertyChangedEventHandler eventHandler = PropertyChanged;

				if (eventHandler != null)
					eventHandler(this, new PropertyChangedEventArgs("Name"));
			}
		}

		[DataMember]
		public int ID
		{
			get
			{
				return id;
			}

			set
			{
				id = value;

				PropertyChangedEventHandler eventHandler = PropertyChanged;

				if (eventHandler != null)
					eventHandler(this, new PropertyChangedEventArgs("ID"));
			}
		}

		[DataMember]
		public int Height { get; set; }

		[DataMember]
		public EventList<Heater> Heaters
		{
			get
			{
				return heaters;
			}

			private set
			{
				if (heaters != null)
					heaters.ItemsChanged -= new EventHandler(heaters_Changed);

				heaters = (value == null ? new EventList<Heater>() : value);
				heaters.ItemsChanged += new EventHandler(heaters_Changed);

				foreach (Heater heater in heaters)
					heater.PropertyChanged += new PropertyChangedEventHandler(Heater_PropertyChanged);

				PropertyChangedEventHandler eventHandler = PropertyChanged;

				if (eventHandler != null)
					eventHandler(this, new PropertyChangedEventArgs("Heaters"));
			}
		}

        public List<roomData> dataRoom { get; set; }

        public List<roomData> nutzdataRoom { get; set; }

        public double[] RWork;

        public double Hlim { get; set; }

        public double TSoll { get; set; }

        public bool[] checkedList;


        #endregion Properties

        #region Constructors

        public Room(int id)
			: this(id, "", new EventList<Heater>())
		{
		}

		public Room(int id, String name)
			: this(id, name, new EventList<Heater>())
		{
		}

		public Room(int id, EventList<Heater> heaters)
			: this(id, "", heaters)
		{
		}

		public Room(int id, String name, EventList<Heater> heaters)
		{
			this.ID = id;
			this.Name = name;
			this.Heaters = heaters;
            init();
		}

        public void init() // VentilID -> Wochentag -> PunktID -> Zeit = temp
        {
            this.RWork = new double[5];
            this.Hlim = 1;
            dataRoom = new List<roomData>();
            nutzdataRoom = new List<roomData>();
            checkedList = new bool[7];
            //Testdaten
        }

        public void addData(int RaumID, roomData.Day Wochentag, byte Stunde, byte Minute, short Temperatur) //(int RaumID, Day Wochentag, byte Stunde, byte Minute, short Temperatur)
        {
            foreach (roomData test in dataRoom)
            {
                if (test.RaumID == RaumID && test.Wochentag == Wochentag && test.Stunde == Stunde && test.Minute == Minute && test.Temperatur == Temperatur)
                {
                    return;
                }
            }
            dataRoom.Add(new roomData(RaumID, Wochentag, Stunde, Minute, Temperatur));
        }

        public List<roomData> nutzdataToday()
        {
            List<roomData> newlist = new List<roomData>();

            foreach(roomData test in nutzdataRoom)
            {
                if((int) test.Wochentag == (int)(DateTime.Now.DayOfWeek))
                {
                    newlist.Add(test);
                }
            }
            return newlist;
        }

        public List<roomData> nutzdataDay(int tag)
        {
            List<roomData> newlist = new List<roomData>();

            foreach (roomData test in nutzdataRoom)
            {
                if ((int)test.Wochentag == tag)
                {
                    newlist.Add(test);
                }
            }
            return newlist;
        }

        public List<roomData> roomdataDay(int tag)
        {
            List<roomData> newlist = new List<roomData>();

            foreach (roomData test in dataRoom)
            {
                if ((int)test.Wochentag == tag)
                {
                    newlist.Add(test);
                }
            }
            return newlist;
        }

        public void nutzaddData(int RaumID, roomData.Day Wochentag, byte Stunde, byte Minute, short Temperatur) //(int RaumID, Day Wochentag, byte Stunde, byte Minute, short Temperatur)
        {
            foreach (roomData test in nutzdataRoom)
            {
                if (test.RaumID == RaumID && test.Wochentag == Wochentag && test.Stunde == Stunde && test.Minute == Minute && test.Temperatur == Temperatur)
                {
                    return;
                }
            }
            nutzdataRoom.Add(new roomData(RaumID, Wochentag, Stunde, Minute, Temperatur));
        }

        public void nutzaddDataAt(int RaumID, roomData.Day Wochentag, byte Stunde, byte Minute, short Temperatur, int index) //(int RaumID, Day Wochentag, byte Stunde, byte Minute, short Temperatur)
        {
            nutzdataRoom.Insert(index, new roomData(RaumID, Wochentag, Stunde, Minute, Temperatur));
        }



        public short previousTemp()
        {
            int day = (int)(DateTime.Now.DayOfWeek);
            byte hour = (byte)(DateTime.Now.Hour);
            byte minute = (byte)(DateTime.Now.Minute);
            int zuletzt = 0;
            for(int i = 0; i < nutzdataRoom.Count; i++)
            {
                roomData rd = nutzdataRoom[i];
                if ((int)rd.Wochentag < day)
                {
                    // tue nichts
                }
                else if ((int)rd.Wochentag == day)
                {
                    {
                        if (rd.Stunde <= hour)
                        {
                            if (rd.Minute <= minute)
                            {
                                zuletzt = i;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return nutzdataRoom[zuletzt].Temperatur;
        }

        public int nextPoint()
        {
            int day = (int)(DateTime.Now.DayOfWeek);
            byte hour = (byte)(DateTime.Now.Hour);
            byte minute = (byte)(DateTime.Now.Minute);
            int zuletzt = 0;
            for (int i = 0; i < nutzdataRoom.Count; i++)
            {
                roomData rd = nutzdataRoom[i];
                if ((int)rd.Wochentag < day)
                {
                    // tue nichts
                }
                else if ((int)rd.Wochentag == day)
                {
                    if (rd.Stunde <= hour)
                    {
                        if (rd.Minute <= minute)
                        {
                            zuletzt = i;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return zuletzt + 1;
        }

        public void delData(int RaumID, roomData.Day Wochentag, byte Stunde, byte Minute, short Temperatur)
        {
            foreach (roomData test in dataRoom)
            {
                if (test.RaumID == RaumID && test.Wochentag == Wochentag && test.Stunde == Stunde && test.Minute == Minute && test.Temperatur == Temperatur)
                {
                    dataRoom.Remove(test);
                    break;
                }
            }
        }

        #endregion Constructors

        #region PrivateMembers

        private EventList<Heater> heaters;
		private string name;
		private int id;

		private void heaters_Changed(object sender, EventArgs e)
		{
			EventList<Heater>.ChangedEventArgs args = e as EventList<Heater>.ChangedEventArgs;

			switch (args.ChangeType)
			{
				case EventList<Heater>.ChangeType.ADDED:
					EventList<Heater>.AddedEventArgs addedArgs = args as EventList<Heater>.AddedEventArgs;
					addedArgs.Item.PropertyChanged += new PropertyChangedEventHandler(Heater_PropertyChanged);
					break;

				case EventList<Heater>.ChangeType.REMOVED:
				case EventList<Heater>.ChangeType.CLEARED:
					Data.sortHeaterIDs();

					break;

				case EventList<Heater>.ChangeType.RANGE_ADDED:
					EventList<Heater>.RangeAddedEventArgs rangeAddedArgs = args as EventList<Heater>.RangeAddedEventArgs;

					foreach (Heater heater in rangeAddedArgs.Items)
						heater.PropertyChanged += new PropertyChangedEventHandler(Heater_PropertyChanged);

					break;

			}
            
		}

		private void Heater_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Power":
					break;
			}

		}

        public void sort()
        {
            List<roomData> newdataRoom = new List<roomData>();
            List<roomData> newnutzdataroom = new List<roomData>();
            int dataroomlength = dataRoom.Count;
            int nutzroomlength = nutzdataRoom.Count;
            roomData newest = null;
            roomData next = null;

            for (int i = 0; i < dataroomlength; i++)
            {
                for (int j = 0; j < dataRoom.Count; j++)
                {
                    if (j == 0)
                    {
                        newest = dataRoom[j];
                    }
                    else
                    {
                        next = dataRoom[j];
                        if ((int)(newest.Wochentag) > (int)(next.Wochentag)) // Wenn der naechste wert ein Tag vorher ist: also Montag < Dienstag < .... < Sonntag
                        {
                            newest = next;
                        }
                        else if ((int)(newest.Wochentag) < (int)(next.Wochentag))
                        {
                            //newest ist ein tag frueher als der naechste angeschaute wert. also tue nix
                        }
                        else //wenn es der selbe tag ist
                        {
                            //s
                            if ((int)(newest.Stunde) > (int)(next.Stunde))
                            {
                                newest = next;
                            }
                            else if ((int)(newest.Stunde) < (int)(next.Stunde))
                            {
                                //newest ist ein tag frueher als der naechste angeschaute wert. also tue nix
                            }
                            else //wenn es der selbe tag ist
                            {
                                if ((int)(newest.Minute) > (int)(next.Minute))
                                {
                                    newest = next;
                                }
                                else if ((int)(newest.Minute) < (int)(next.Minute))
                                {
                                    //newest ist ein tag frueher als der naechste angeschaute wert. also tue nix
                                }
                                else //wenn es der selbe tag, stunde, minute
                                {
                                    // der fall sollte nicht eintreten
                                }
                            }
                        }
                    }
                }
                newdataRoom.Add(newest);
                dataRoom.Remove(newest);
            }

            for (int i = 0; i < nutzroomlength; i++)
            {
                for (int j = 0; j < nutzdataRoom.Count; j++)
                {
                    if (j == 0)
                    {
                        newest = nutzdataRoom[j];
                    }
                    else
                    {
                        next = nutzdataRoom[j];
                        if ((int)(newest.Wochentag) > (int)(next.Wochentag)) // Wenn der naechste wert ein Tag vorher ist: also Montag < Dienstag < .... < Sonntag
                        {
                            newest = next;
                        }
                        else if ((int)(newest.Wochentag) < (int)(next.Wochentag))
                        {
                            //newest ist ein tag frueher als der naechste angeschaute wert. also tue nix
                        }
                        else //wenn es der selbe tag ist
                        {
                            //s
                            if ((int)(newest.Stunde) > (int)(next.Stunde))
                            {
                                newest = next;
                            }
                            else if ((int)(newest.Stunde) < (int)(next.Stunde))
                            {
                                //newest ist ein tag frueher als der naechste angeschaute wert. also tue nix
                            }
                            else //wenn es der selbe tag ist
                            {
                                if ((int)(newest.Minute) > (int)(next.Minute))
                                {
                                    newest = next;
                                }
                                else if ((int)(newest.Minute) < (int)(next.Minute))
                                {
                                    //newest ist ein tag frueher als der naechste angeschaute wert. also tue nix
                                }
                                else //wenn es der selbe tag, stunde, minute
                                {
                                    // der fall sollte nicht eintreten
                                }
                            }
                        }
                    }
                }
                newnutzdataroom.Add(newest);
                nutzdataRoom.Remove(newest);
            }

            dataRoom = newdataRoom;
            nutzdataRoom = newnutzdataroom;



            //room.dataRoom.Sort((x, y) => ((int)(x.Wochentag)).CompareTo(((int)(y.Wochentag))));
            //room.dataRoom.Sort((x, y) => x.calculateTime().CompareTo(y.calculateTime()));
            //room.nutzdataRoom.Sort((x, y) => ((int)x.Wochentag).CompareTo((int)y.Wochentag));
            //room.nutzdataRoom.Sort((x, y) => x.calculateTime().CompareTo(y.calculateTime()));
        }

        #endregion PrivateMembers

        internal void removeValves()
		{
			foreach (Heater heater in heaters)
				heater.Valve = null;
		}
	}
}
