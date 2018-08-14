using FlowCalibrationInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynAbg
{

    public class roomData
    {
        /*
         * sint16 becomes System.Int16, or short
         * uint16 becomes System.UInt16, or ushort
         * uint32 becomes System.UInt32, or uint
         */

        public int RaumID;
        public Day Wochentag;
        public byte Stunde; //uINT8
        public byte Minute; //uINT8
        public short Temperatur; //sINT16

        public enum Day { Heute, Montag, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag };


        public roomData(int RaumID, Day Wochentag, byte Stunde, byte Minute, short Temperatur)
        {
            this.RaumID = RaumID;
            this.Wochentag = Wochentag;
            this.Stunde = Stunde;
            this.Minute = Minute;
            this.Temperatur = Temperatur;
        }

        public int calculateTime()
        {
            return Convert.ToInt32(Stunde) * 60 * 60 + Convert.ToInt32(Minute) * 60;
        }

        public Day getDay(string Name)
        {
            return (Day) Enum.Parse(typeof(Day), Name);
        }

        public string inString()
        {
            string ret = "";
            ret = ret + RaumID.ToString() + "|" + Wochentag.ToString() + "|" + Stunde.ToString() + "|" + Minute.ToString() + "|" + Temperatur.ToString();
            return ret;
        }
    }
}
