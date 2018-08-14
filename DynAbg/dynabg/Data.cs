
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System;
using System.ComponentModel;
using FlowCalibrationInterface;
using DynAbg.valve;
using System.Threading;
using System.Collections.Generic;
using DynAbg.Generic;

namespace DynAbg
{
	internal static class Data
	{
		public static ICalibration calibration;
		public static List<ValveInfo> valveInfos;


        private static EventList<Room> rooms;
		private static string ddl;

		static Data()
		{
			rooms = new EventList<Room>();
			rooms.ItemsChanged += new EventHandler(rooms_Changed);
		}

		private static void rooms_Changed(object sender, EventArgs e)
		{
			//calculate();
		}

		public static string Protocol
		{
			get
			{
				return ddl;
			}

			set
			{
				ddl = value;
			}
		}

		public static EventList<Room> Rooms 
		{
			get
			{
				return rooms;
			}

			private set
			{
				if (rooms != null)
					rooms.ItemsChanged -= new EventHandler(rooms_Changed);

				rooms = value;

				if (rooms == null)
					rooms = new EventList<Room>();

				rooms.ItemsChanged += new EventHandler(rooms_Changed);
			}
		}

		public static List<IValve> Valves
		{
			get
			{
				List<IValve> valves = new List<IValve>();

				foreach (Room room in rooms)
					foreach (Heater heater in room.Heaters)
						if (heater.Valve != null)
							valves.Add(heater.Valve);

				return valves;
			}
		}


		internal static bool load(string file)
		{
			EventList<Room> rooms = deserializeFromXML(file);

			if (rooms == null)
				return false;

			foreach (Room room in Rooms)
				foreach (Heater heater in room.Heaters)
					if (heater.Valve != null)
						calibration.deactivateValve(heater.Valve);

			Rooms = rooms;

			return true;
		}

		internal static void save(string file)
		{
			serializeToXML(Rooms, file, ddl);
		}

		private static void serializeToXML(EventList<Room> rooms, string file, string ddl)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(EventList<Room>));
			var settings = new XmlWriterSettings { Indent = true };
			XmlWriter writer = XmlWriter.Create(file, settings);
			serializer.WriteObject(writer, rooms);
			writer.Close();
		}

		private static EventList<Room> deserializeFromXML(string file)
		{
			FileInfo fileInfo = new FileInfo(file);
			EventList<Room> rooms = null;

			if (!fileInfo.Exists)
				return rooms;

			DataContractSerializer deserializer = new DataContractSerializer(typeof(EventList<Room>));
			XmlReader textReader = XmlReader.Create(file);

			try
			{
				rooms = (EventList<Room>)deserializer.ReadObject(textReader);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				textReader.Close();
			}

			return rooms;
		}

		private static Room getRoom(Heater heater)
		{
			foreach (Room room in Rooms)
				if (room.Heaters.Contains(heater))
					return room;

			return null;
		}


		internal static int getNewHeaterID()
		{
			int id = 0;

			foreach (Room room in rooms)
				id += room.Heaters.Count;

			return id++;
		}

		internal static int getNewRoomID()
		{
			return rooms.Count;
		}

		internal static Heater getHeater(int id)
		{
			foreach (Room room in rooms)
			{
				List<Heater> heaters = room.Heaters;

				foreach (Heater heater in heaters)
					if (heater.ID == id)
						return heater;
			}

			return null;
		}

		internal static Room getRoom(int id)
		{
			foreach (Room room in rooms)
			{
				if (room.ID == id)
					return room;
			}

			return null;
		}

		internal static void sortHeaterIDs()
		{
			List<Heater> heaters = new List<Heater>();

			foreach (Room room in rooms)
				heaters.AddRange(room.Heaters);

			int counter = 0;

			foreach (Heater heater in heaters)
			{
				heater.ID = counter;
				counter++;
			}
		}

		internal static void sortRoomIDs(int above)
		{
			foreach (Room room in rooms)
			{
				if (room.ID < above)
					continue;
				else
					room.ID--;
			}
		}

		internal static void reset()
		{
			foreach (Room room in Rooms)
				foreach (Heater heater in room.Heaters)
					if (heater.Valve != null)
						calibration.deactivateValve(heater.Valve);

			Rooms = new EventList<Room>();
		}

		internal static bool settingsValid(ref List<String> fehler)
		{
			if (fehler == null)
				fehler = new List<string>();

			// Empfänger prüfen ...
			if (calibration == null)
				fehler.Add(String.Format("Fehler {0:3} - Die Bibliothek FlowCalibration.dll wurde nicht geladen.", fehler.Count + 1));
			else if (!calibration.Initialized)
				fehler.Add(String.Format("Fehler {0:000} - Der EnOcean-Empfänger wurde nicht initialisiert.", fehler.Count + 1));

			if (Rooms.Count == 0)
				fehler.Add(String.Format("Fehler {0:000} - Es wurde kein Raum angelegt.", fehler.Count + 1));

			List<IValve> valves = new List<IValve>();

			// Einstellungen der Ventile prüfen ...
			foreach (Room room in rooms)
			{
				if (room.Heaters.Count == 0)
					fehler.Add(String.Format("Fehler {0:000} - Im Raum {1:000} wurde kein Heizkörper angelegt.", fehler.Count + 1, room.Name));

				foreach (Heater heater in room.Heaters)
				{
					ValveInfo valveInfo = heater.ValveInfo;

					if (valveInfo == null)
					{
						fehler.Add(String.Format("Fehler {0:000} - Dem Heizkörper {1:000} im Raum {2:000} wurde kein Ventil zugewiesen.", fehler.Count + 1, heater.ID + 1, room.Name));
					}
					else
					{
						IValve valve = heater.Valve;

						if (valve == null)
						{
							fehler.Add(String.Format("Fehler {0:000} - Das Ventil am Heizkörper {1:000} im Raum {2:000} wurde nicht angelernt.", fehler.Count + 1, heater.ID + 1, room.Name));
						}
						else
						{
							valves.Add(valve);

							if (!valve.Battery)
								fehler.Add(String.Format("Fehler {0:000} - Die Ladung der Batterien des Ventils am Heizkörper {1:000} im Raum {2:000} ist unter 20 %.", fehler.Count + 1, heater.ID + 1, room.Name));

							if (!valve.KommunikationOK)
								fehler.Add(String.Format("Fehler {0:000} - Die Kommunikation mit dem Ventil am Heizkörper {1:000} im Raum {2:000} ist unterbrochen.", fehler.Count + 1, heater.ID + 1, room.Name));

							if (valve.Signal > 85)
								fehler.Add(String.Format("Fehler {0:000} - Die Signalstärke des Ventils am Heizkörper {1:000} im Raum {2:000} ist nicht ausreichend.", fehler.Count + 1, heater.ID + 1, room.Name));
						}
					}
				}
			}

			return fehler.Count == 0;
		}
	}
}
