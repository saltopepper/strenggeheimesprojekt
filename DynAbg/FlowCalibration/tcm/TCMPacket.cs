using System;

// A send packet and it information saved in variables

namespace FlowCalibration.tcm
{
	public class TCMPacket
    {
        public const int TYPE_RADIO				    = 0x01;
	    public const int TYPE_RESPONSE			    = 0x02;
	    public const int TYPE_RADIO_SUB_TEL		    = 0x03;
	    public const int TYPE_EVENT				    = 0x04;
	    public const int TYPE_COMMON_COMMAND		= 0x05;
	    public const int TYPE_SMART_ACK_COMMAND     = 0x06;
        public const int TYPE_REMOTE_MAN_COMMAND    = 0x07;

        public const int PROTOCOL_UNKNOWN   = 0x00;
        public const int PROTOCOL_ESP2      = 0x01;
        public const int PROTOCOL_ESP3      = 0x02;

        public int protocol { get; set; }
        public int type { get; set; }
        public byte[] data { get; set; }
        public byte[] optionalData { get; set; }
        public byte[] completeData { get; set; }
        public DateTime timestamp { get; set; }

        public TCMPacket(int protocol, int type, byte[] data, byte[] optionalData, byte[] completeData, DateTime timestamp)
        {
            this.protocol = protocol;
            this.type = type;
            this.data = data;
            this.optionalData = optionalData;
            this.completeData = completeData;
            this.timestamp = timestamp;
        }
    }
}
