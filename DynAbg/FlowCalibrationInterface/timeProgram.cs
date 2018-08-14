using FlowCalibrationInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCalibrationInterface
{

    public class timeProgram
    {
        /*
         * sint16 becomes System.Int16, or short
         * uint16 becomes System.UInt16, or ushort
         * uint32 becomes System.UInt32, or uint
         */

        public byte eMinute;    // 0-59
        public byte eStunde;    // 0-23
        public byte sMinute;    // 0-59
        public byte sStunde;    // 0-23

        public byte period;
        /* 15   - Friday - Monday
         * 14   - Friday - Sunday
         * 13   - Thursday - Friday
         * 12   - Wednesday - Friday
         * 11   - Tuesday - Thursday
         * 10   - Monday - Wednesday
         * 9    - Sunday
         * 8    - Saturday
         * 7    - Friday
         * 6    - Thursday
         * 5    - Wednesday
         * 4    - Tuesday
         * 3    - Monday 
         * 2    - Saturday - Sunday
         * 1    - Monday - Friday
         * 0    - Monday - Sunday
         */

        public byte mode;
        /*
         * 3    - Building protection
         * 2    - Pre-comfort
         * 1    - Economy
         * 0    - Comfort (Standard)
         * */

        public byte deletion;
        /*
         * 1    - Deletion
         * 0    - No deletion
         * */

        public timeProgram(byte emin, byte estun, byte smin, byte sstun, byte per, byte mod, byte del)
        {
            this.eMinute = emin;
            this.eStunde = estun;
            this.sMinute = smin;
            this.sStunde = sstun;
            this.period = per;
            this.mode = mod;
            this.deletion = del;
        }
        
    }
}
