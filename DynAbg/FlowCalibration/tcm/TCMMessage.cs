using System;
using System.Text;
using System.IO;

//basically the communication with the valve
//constructor checks for valid communication

namespace FlowCalibration.tcm
{
	public class TCMMessage
    {
        public const int UNKNOWN = -1;
        /** Receive-Radio-Telegram */ 
	    public const byte HEADERID_RRT = 0x00;
	    public const byte HEADERID_UNKNOWN1 = 0x01;
	    public const byte HEADERID_UNKNOWN2 = 0x02; 
	    /** Transmit-Radio-Telegram */
	    public const byte HEADERID_TRT = 0x03;
	    /** Receive-Message-Telegram */
	    public const byte HEADERID_RMT = 0x04;
	    /** Transmit-Command-Telegram */
	    public const byte HEADERID_TCT = 0x05;
	    /** Message from TCM */
	    public const byte HEADERID_TCM = 0x08;

	    /** Telegram from PTM switch module */
	    public const byte ORG_RPS = 0x05; 
	    /** 1 byte data telegram from STM sensor module */
	    public const byte ORG_1BS = 0x06;
	    /** 4 byte data telegram from STM sensor module */
	    public const byte ORG_4BS = 0x07; 
	    /** Telegram from CTM module */
	    public const byte ORG_HRC = 0x08; 
	    /** 6 byte modem telegram */
	    public const byte ORG_6DT = 0x0A; 
	    /** Modem-Acknowledge-Telegram */
	    public const byte ORG_MDA = 0x0B;
        /** Anlern module RORG und ORG byte */
        public const byte ORG_D4 = 0xD4;
        /** Data Telegram byte fuer modul */
        public const byte ORG_D2 = 0xD2;


        /** Erron in H_SEQ field */
        public const byte ERR_SYNTAX_H_SEQ = 0x08; 
	    /** Error in length */
	    public const byte ERR_SYNTAX_LENGTH = 0x09; 
	    /** Error in Checksum */
	    public const byte ERR_SYNTAX_CHKSUM = 0x0A; 
	    /** Error in ORG field */
	    public const byte ERR_SYNTAX_ORG = 0x0B; 
	    /** Same modem ID */
	    public const byte ERR_MODEM_DUP_ID = 0x0C; 
	    /** ERROR-Message */
	    public const byte ERR = 0x19; 
	    /** ID-Number outside ID-Range */
	    public const byte ERR_TX_IDRANGE = 0x22; 
	    /** A modem acknowledge telegram is received after a timeout of 100 ms */
	    public const byte ERR_MODEM_NOTWANTEDACK = 0x28; 
	    /** No modem acknowledge telegram is received after a timeout of 100 ms */
	    public const byte ERR_MODEM_NOTACK = 0x29; 
	    /** OK-Message */
	    public const byte OK = 0x58; 
	    /** Current TCM radio sensitivity */
	    public const byte INF_RX_SENSITIVITY = (byte)0x88; 
	    /** Init-Message */
	    public const byte INF_INIT = (byte)0x89; 
	    /** The TCM software version */
	    public const byte INF_SW_VER = (byte)0x8C; 
	    /** ID range base number */
	    public const byte INF_IDBASE = (byte)0x98; 
	    /** TCM modem status */
        public const byte INF_MODEM_STATUS = (byte)0xA8;

        public long bapID { get; private set; }
        public byte[] message { get; private set; }
        public int byte0 { get; private set; }
        public int byte1 { get; private set; }
        public int byte2 { get; private set; }
        public int byte3 { get; private set; }
        public int byte4 { get; private set; }
        public int byte5 { get; private set; }
        public int byte6 { get; private set; }
        public int byte7 { get; private set; }
        public int dataBytes { get; private set; }
        public long objectID { get; private set; }
        public DateTime datum_uhrzeit { get; private set; }
        public bool highspeedMessage { get; private set; }
        public int protocol { get; private set; }
        public int orgByte { get; private set; }
        public int headerID { get; private set; }
        public int status { get; private set; }
        public int signalStrength { get; private set; }
        public int subtelegrams { get; private set; }
        public long destinationID { get; private set; }
        public int securityLevel { get; private set; }
        public bool learnMessage { get; private set; }
        public bool regularData { get; private set; }

        //Learn related stuff
        public int function { get; private set; }
        public int type { get; private set; }
        public int manufacturer { get; private set; }

        public TCMMessage(TCMPacket packet, long bapID)
        {
            this.bapID = bapID;
            this.protocol = packet.protocol;
            this.message = packet.completeData;
            this.datum_uhrzeit = packet.timestamp;
            this.highspeedMessage = true;

            switch (protocol)
            {
                case TCMPacket.PROTOCOL_ESP2:
                    if (!readESP2(packet))
                        headerID = HEADERID_UNKNOWN1;
                    break;
                // Meistens Protocoll ESP3
                case TCMPacket.PROTOCOL_ESP3:
                    if (!readESP3(packet))
                        headerID = HEADERID_UNKNOWN1;
                    break;
            }

            this.dataBytes = ((int)(byte3 & 0xFF) << 24 | (int)(byte2 & 0xFF) << 16 | (int)(byte1 & 0xFF) << 8 | (int)(byte0 & 0xFF));
            this.learnMessage = orgByte == ORG_D2 ? false : orgByte == ORG_1BS ? (byte3 & 0x08) == 0 : (byte0 & 0x08) == 0;
            this.regularData = learnMessage ? orgByte == ORG_1BS ? true : (byte0 & 0x80) == 0 : true;

            if (learnMessage && !regularData)
            {

                if (this.orgByte == ORG_D4)
                {
                    //int test = byte4;
                    manufacturer = byte4;
                    function = byte1;
                    type = byte2;
                }
                else if (this.orgByte == ORG_D2)
                {

                }
                else
                {
                    manufacturer = readBits(dataBytes, 13, 11);
                    function = readBits(dataBytes, 0, 6);
                    type = readBits(dataBytes, 6, 7);
                }
            }
            else
            {
                function = UNKNOWN;
                type = UNKNOWN;
                manufacturer = UNKNOWN;
            }
        }

        private bool readESP2(TCMPacket packet)
        {
            this.objectID = byteArrToLong(message, 8);
            this.byte3 = byteArrToInt(message, 4);
            this.byte2 = byteArrToInt(message, 5);
            this.byte1 = byteArrToInt(message, 6);
            this.byte0 = byteArrToInt(message, 7);
            this.orgByte = message[3];
            this.headerID = readBits(message[2], 0, 3);
            this.status = byteArrToInt(message, 12);
            this.signalStrength = 255;
            this.securityLevel = 0;
            this.destinationID = 0xFFFFFFFF;
            return true;
        }

        private bool readESP3(TCMPacket packet)
        {
            //Erst auf optionale Daten prüfen
            // PacketType muss 1 sein
            // SecurityLevvel muss 0 sein
            // Read Data -> Falls Korrekt, dann bytes setzen
            if (packet.type != TCMPacket.TYPE_RADIO)
            {
                //Console.WriteLine("Packet type " + packet.type + " is not supported");
                return false;
            }

            if (packet.optionalData != null)
            {
                byte[] optionalData = packet.optionalData;

                subtelegrams = (int)(optionalData[0] & 0xFF);

                destinationID = 0;
                destinationID |= (long)(optionalData[1] & 0xFF) << 24;
                destinationID |= (long)(optionalData[2] & 0xFF) << 16;
                destinationID |= (long)(optionalData[3] & 0xFF) << 8;
                destinationID |= (long)(optionalData[4] & 0xFF);

                signalStrength = (int)(optionalData[5] & 0xFF);
                securityLevel = (int)(optionalData[6] & 0xFF);
            }

            if (securityLevel != 0)
            {
                Console.WriteLine("Security type " + securityLevel + " is not supported");
                return false;
            }

            MemoryStream memoryStream = new MemoryStream(packet.data);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            int choice = binaryReader.ReadByte();

            switch (choice)
            {
                case 0xA5: //4BS
                    orgByte = ORG_4BS;
                    break;

                case 0xD5: //1BS
                    orgByte = ORG_1BS;
                    break;

                case 0xF6: //RPS
                    orgByte = ORG_RPS;
                    break;

                case 0xD4:
                    orgByte = ORG_D4;
                    break;

                case 0xD2:
                    orgByte = ORG_D2;
                    break;

                default:
                    Console.WriteLine("Unsupported choice " + choice);
                    return false;
            }

            switch (orgByte)
            {
                case ORG_4BS:
                    this.byte3 = binaryReader.ReadByte();
                    this.byte2 = binaryReader.ReadByte();
                    this.byte1 = binaryReader.ReadByte();
                    this.byte0 = binaryReader.ReadByte();
                    break;

                case ORG_1BS:
                    this.byte3 = binaryReader.ReadByte();
                    this.byte2 = 0;
                    this.byte1 = 0;
                    this.byte0 = 0;
                    break;

                case ORG_RPS:
                    this.byte3 = binaryReader.ReadByte();
                    this.byte2 = 0;
                    this.byte1 = 0;
                    this.byte0 = 0;
                    break;

                case ORG_D4:
                    this.byte6 = binaryReader.ReadByte();
                    this.byte5 = binaryReader.ReadByte();
                    this.byte4 = binaryReader.ReadByte();
                    this.byte3 = binaryReader.ReadByte();
                    this.byte2 = binaryReader.ReadByte();
                    this.byte1 = binaryReader.ReadByte();
                    this.byte0 = binaryReader.ReadByte();
                    break;

                case ORG_D2:
                    if (packet.completeData[2] == 0x0E)
                    {
                        this.byte7 = binaryReader.ReadByte();
                        this.byte6 = binaryReader.ReadByte();
                        this.byte5 = binaryReader.ReadByte();
                        this.byte4 = binaryReader.ReadByte();
                        this.byte3 = binaryReader.ReadByte();
                        this.byte2 = binaryReader.ReadByte();
                        this.byte1 = binaryReader.ReadByte();
                        this.byte0 = binaryReader.ReadByte();
                    }
                    else if (packet.completeData[2] == 0x0C)
                    {
                        this.byte5 = binaryReader.ReadByte();
                        this.byte4 = binaryReader.ReadByte();
                        this.byte3 = binaryReader.ReadByte();
                        this.byte2 = binaryReader.ReadByte();
                        this.byte1 = binaryReader.ReadByte();
                        this.byte0 = binaryReader.ReadByte();
                    }
                    else if (packet.completeData[2] == 0x08)
                    {
                        this.byte1 = binaryReader.ReadByte();
                        this.byte0 = binaryReader.ReadByte();
                    }
                    break;
            }

            objectID = 0;
            objectID |= (long)(binaryReader.ReadByte() & 0xFF) << 24;
            objectID |= (long)(binaryReader.ReadByte() & 0xFF) << 16;
            objectID |= (long)(binaryReader.ReadByte() & 0xFF) << 8;
            objectID |= (long)(binaryReader.ReadByte() & 0xFF);

            status = binaryReader.ReadByte();
            headerID = HEADERID_RRT;
            return true;
        }

        public static int byteArrToInt(byte[] ar, int start)
        {
            int low = ar[start] & 0xFF;
            return (int)(low);
        }

        public static long byteArrToLong(byte[] ar, int start)
        {
            long ret = 0L;
            ret = ((long)(ar[start] & 0xFF) << 24 | (ar[start + 1] & 0xFF) << 16 | (ar[start + 2] & 0xFF) << 8 | (ar[start + 3] & 0xFF));
            return ret;
        }

        public static String reformMessage(byte[] message, long bapid)
        {
            StringBuilder newMessage = new StringBuilder(100);
            newMessage.Append(Convert.ToString(bapid));
            newMessage.Append('#');
            newMessage.Append(getHexString(message));
            newMessage.Append('#');
            newMessage.Append(Convert.ToString(System.DateTime.Now.Ticks));
            return newMessage.ToString();
        }

        public static String getHexString(byte[] bs)
        {
            StringBuilder ret = new StringBuilder(bs.Length);

            for (int i = 0; i < bs.Length; i++)
            {
                String hex = (0x0100 + (bs[i] & 0x00FF)).ToString("X8").Substring(1);
                ret.Append((hex.Length < 2 ? "0" : "") + hex);
            }

            return ret.ToString();
        }

        public static int readBits(int data, int offset, int length)
        {
            if ((offset + length) > 32 || offset < 0 || offset > 31)
            {
                throw new IndexOutOfRangeException();
            }

            int bitposition = 31 - offset;

            int result = 0;

            for (int i = 0; i < length; i++)
            {
                result = (result << 1) | ((data >> bitposition) & 0x01);
                bitposition--;
            }

            return result;
        }

        public static int readBits(byte data, int offset, int length)
        {
            if ((offset + length) > 8 || offset < 0 || offset > 7)
                throw new IndexOutOfRangeException();

            int bitposition = 7 - offset;

            int result = 0;

            for (int i = 0; i < length; i++)
            {
                result = (result << 1) | ((data >> bitposition) & 0x01);
                bitposition--;
            }

            return result;
        }

        public int getSignalStrength()
        {
            return signalStrength;
        }

        public int getProtocol()
        {
            return protocol;
        }

        public String toSting()
        {
            return getHexString(message);
        }
    }
}
