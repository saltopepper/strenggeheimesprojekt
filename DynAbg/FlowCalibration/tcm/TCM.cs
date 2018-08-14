using System;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using FlowCalibration.tools;
using FlowCalibration.ftdi;
using System.Diagnostics;

namespace FlowCalibration.tcm
{
    delegate void MessageEventHandler(TCMMessage message);
    delegate void ErrorEventHandler(string error);

    internal class TCM
    {
        public static byte[] COMMAND_GETID_ESP3 = new byte[] { 0x55, 0x00, 0x01, 0x00, 0x05, 0x70, 0x08, 0x38 };
        
        public event MessageEventHandler messageReceived;
        public event ErrorEventHandler errorOccurred;

        private string lastErrorMessage = "";
        private bool connected = false;
        private SerialPort serialPort = null;
        private long id = 0L;

        // connected mit dem Sender
        public bool connect()
        {
            if (connected)
                return true;

            // findet den richtigen Port
            string port = getFTDIPort();
            //string port = "COM3";
			if (port == null)
				return false;

			Debug.WriteLine(port);

            // Wird angefragt
            // Muss eine ID ausgeben, sonst ist es falsches Modul
            try
            {
				open(port, 57600);
				write(COMMAND_GETID_ESP3);
				id = readID(2000);

                if (id == -1)
                    throw new Exception("No id from TCM received.");

                connected = true;

                Thread thread = new Thread(new ThreadStart(run));
				thread.Name = String.Format("TCM Port = {0}, ID = {1}", port, id);
				thread.Start();
            }
            catch (Exception e)
            {
                close();
                lastErrorMessage = e.Message;
                return false;
            }

            return connected;
        }

        //finds the ftdi port
		private string getFTDIPort()
		{
			FTDI ftdi = new FTDI();
			FTDI.FT_STATUS state;

			uint deviceCount = 0;

			state = ftdi.GetNumberOfDevices(ref deviceCount);

			if (state != FTDI.FT_STATUS.FT_OK)
				return null;

			FTDI.FT_DEVICE_INFO_NODE[] deviceNodes = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
			state = ftdi.GetDeviceList(deviceNodes);

			if (state != FTDI.FT_STATUS.FT_OK)
				return null;

			string port = null;

			foreach (FTDI.FT_DEVICE_INFO_NODE node in deviceNodes)
			{
				if (node.Type == FTDI.FT_DEVICE.FT_DEVICE_2232)
				{
					//Dieses Gerät hat 2 serielle Anschlüsse. Wir brauchen den Typ B.
					if (node.Description.EndsWith("B"))
					{
						state = ftdi.OpenBySerialNumber(node.SerialNumber);

						if (state != FTDI.FT_STATUS.FT_OK)
							continue;

						String tmpPort;
						state = ftdi.GetCOMPort(out tmpPort);

						if (state != FTDI.FT_STATUS.FT_OK)
						{
							Trace.WriteLine("Failed to get COM-Port (error " + state.ToString() + ")");
							ftdi.Close();
							continue;
						}

						if (tmpPort == null || tmpPort.Length == 0)
						{
							Trace.WriteLine(string.Format("Failed to get COM-Port for device {0}.", node.SerialNumber));
							ftdi.Close();
							continue;
						}

						FTDI.FT2232_EEPROM_STRUCTURE eepromData = new FTDI.FT2232_EEPROM_STRUCTURE();

						try
						{
							state = ftdi.ReadFT2232EEPROM(eepromData);
						}
						catch (Exception)
						{
							Trace.WriteLine("Exception thrown when calling ReadFT2232EEPROM");
						}

						if (state != FTDI.FT_STATUS.FT_OK)
						{
							Trace.WriteLine("Failed to read device EEPROM (error " + state.ToString() + ")");
							ftdi.Close();
							continue;
						}

						Debug.WriteLine(port);
						ftdi.Close();

                        if (!eepromData.Manufacturer.Equals("EnOcean GmbH") && !eepromData.Manufacturer.Equals("Viessmann") && !eepromData.Manufacturer.Equals("Eltako GmbH"))
                        {
							Trace.WriteLine(String.Format("Wrong Manufacturer {0}", eepromData.Manufacturer));
							continue;
						}

						if (tmpPort != null)
						{
							port = tmpPort;
							break;
						}
					}
				}
				else if (node.Type == FTDI.FT_DEVICE.FT_DEVICE_232R)
				{
					state = ftdi.OpenBySerialNumber(node.SerialNumber);

					if (state != FTDI.FT_STATUS.FT_OK)
						continue;

					String tmpPort;
					state = ftdi.GetCOMPort(out tmpPort);

					if (state != FTDI.FT_STATUS.FT_OK)
					{
						Trace.WriteLine("Failed to get COM-Port (error " + state.ToString() + ")");
						ftdi.Close();
						continue;
					}

					if (tmpPort == null || tmpPort.Length == 0)
					{
						Trace.WriteLine(string.Format("Failed to get COM-Port for device {0}.", node.SerialNumber));
						ftdi.Close();
						continue;
					}

					FTDI.FT232R_EEPROM_STRUCTURE eepromData = new FTDI.FT232R_EEPROM_STRUCTURE();

					try
					{
						state = ftdi.ReadFT232REEPROM(eepromData);
					}
					catch (Exception)
					{
						Trace.WriteLine("Exception thrown when calling ReadFT232REEPROM");
					}

					if (state != FTDI.FT_STATUS.FT_OK)
					{
						Trace.WriteLine("Failed to read device EEPROM (error " + state.ToString() + ")");
						ftdi.Close();
						continue;
					}

					ftdi.Close();

					if (!eepromData.Manufacturer.Equals("EnOcean GmbH") && !eepromData.Manufacturer.Equals("Viessmann") && !eepromData.Manufacturer.Equals("Eltako GmbH"))
					{
						Trace.WriteLine(String.Format("Wrong Manufacturer {0}", eepromData.Manufacturer));
						continue;
					}

					if (tmpPort != null)
					{
						port = tmpPort;
						break;
					}
				}
			}

			return port;
		}

        // open up the port
       	private void open(string portName, int baud)
	    {
            serialPort = new SerialPort(portName, baud, Parity.None, 8, StopBits.One);
            serialPort.Open();
	    }

        // close the port
        private void close()
        {
            try { serialPort.Close(); } catch (Exception) { };
        }

        // reads id within a timelimit
        private long readID(long timeout)
        {
            long id = -1;

            try
            {
                TCMPacket packet = readTCMPacket(timeout);

                if (packet == null)
                    return id;

                byte[] data = packet.data;

				if (packet.type != TCMPacket.TYPE_RESPONSE)
					return readID(timeout);

				int returnCode = data[0];

				if (returnCode != 0)
					return id;

				id = 0;
				id |= (long)(data[1] & 0xFF) << 24;
				id |= (long)(data[2] & 0xFF) << 16;
				id |= (long)(data[3] & 0xFF) << 8;
				id |= (long)(data[4] & 0xFF);
            }
            catch (Exception e)
            {
				Trace.WriteLine(e.StackTrace);
            }

            return id;
        }

        public bool writeCommand(byte[] command)
        {
            return writeESP3(command);
        }

        // schreibt commands an tcm zum versenden??
        private bool writeESP3(byte[] command)
        {
            //Ok, altes ESP2 Format in das ESP3 Format umwandlen
            MemoryStream inputMemoryStream = new MemoryStream(command);
            BinaryReader binaryReader = new BinaryReader(inputMemoryStream);

            binaryReader.BaseStream.Seek(3, SeekOrigin.Begin); //sync, hseq etc. wird nicht benötigt

            byte org = binaryReader.ReadByte();
            byte byte3 = 0, byte2 = 0, byte1 = 0, byte0 = 0, byte4 = 0, byte5 = 0, byte6 = 0;
            byte id3 = 0, id2 = 0, id1 = 0, id0 = 0;
            byte status, dataLength;

            //ORG umwandeln
            switch (org)
            {
                case 0x05:
                    org = 0xF6;
                    byte3 = binaryReader.ReadByte();
                    binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
                    dataLength = 7;
                    break;

                case 0x06:
                    org = 0xD5;
                    byte3 = binaryReader.ReadByte();
                    binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
                    dataLength = 7;
                    break;

                case 0x07:
                    org = 0xA5;
                    byte3 = binaryReader.ReadByte();
                    byte2 = binaryReader.ReadByte();
                    byte1 = binaryReader.ReadByte();
                    byte0 = binaryReader.ReadByte();
                    dataLength = 10;
                    break;

                case 0xD4:
                    org = 0xD4;
                    byte6 = binaryReader.ReadByte();
                    byte5 = binaryReader.ReadByte();
                    byte4 = binaryReader.ReadByte();
                    byte3 = binaryReader.ReadByte();
                    byte2 = binaryReader.ReadByte();
                    byte1 = binaryReader.ReadByte();
                    byte0 = binaryReader.ReadByte();
                    dataLength = 13;
                    break;

                default:
                    Debug.WriteLine("Unknown org byte.");
                    return false;
            }

            id3 = binaryReader.ReadByte();
            id2 = binaryReader.ReadByte();
            id1 = binaryReader.ReadByte();
            id0 = binaryReader.ReadByte();

            status = binaryReader.ReadByte();

            byte[] header = new byte[6];
            header[0] = 0x55;                               //sync byte
            header[1] = (byte)(dataLength >> 8);            //Datenlänge MSB
            header[2] = (byte)dataLength;                   //Datenlänge LSB
            header[3] = 0x06;                               //Datenlänge optionale Daten
            header[4] = 0x01;                               //Typ Radiotelegramm
            header[5] = Tools.calculateCRC8(header, 1, 4);  //CRC8 des Headers

            MemoryStream memoryStream = new MemoryStream();

            memoryStream.WriteByte(org);                            //Choice							

            switch (org)                                    //Payload
            {
                case 0xD5:
                case 0xF6:
                    memoryStream.WriteByte(byte3);
                    break;

                case 0xA5:
                    memoryStream.WriteByte(byte3);
                    memoryStream.WriteByte(byte2);
                    memoryStream.WriteByte(byte1);
                    memoryStream.WriteByte(byte0);
                    break;

                case 0xD4:
                    memoryStream.WriteByte(byte6);
                    memoryStream.WriteByte(byte5);
                    memoryStream.WriteByte(byte4);
                    memoryStream.WriteByte(byte3);
                    memoryStream.WriteByte(byte2);
                    memoryStream.WriteByte(byte1);
                    memoryStream.WriteByte(byte0);
                    break;
            }

            memoryStream.WriteByte(id3);							//Sender-ID MSB
            memoryStream.WriteByte(id2);
            memoryStream.WriteByte(id1);
            memoryStream.WriteByte(id0);							//Sender-ID LSB

            memoryStream.WriteByte(status);						    //Status

            memoryStream.WriteByte(0x03);							//Subtelegramme

            memoryStream.WriteByte(0xFF);                           //Empfänger MSB
            memoryStream.WriteByte(0xFF);                           //(FFFFFFFF = Broadcast)
            memoryStream.WriteByte(0xFF);
            memoryStream.WriteByte(0xFF);                           //Empfänger MSB

            memoryStream.WriteByte(0xFF);							//RSSI (beim Senden FF)

            memoryStream.Close();
            byte[] data = memoryStream.ToArray();
 
	        byte dataCRC = Tools.calculateCRC8(data);
		
	        memoryStream = new MemoryStream();

            try
	        {
                memoryStream.Write(header, 0 , header.Length);
                memoryStream.Write(data, 0, data.Length);
	        }
	        catch (IOException e)
	        {
				Trace.WriteLine(e.StackTrace);
		        return false;
	        }

            memoryStream.WriteByte(dataCRC);
            memoryStream.Close();
            data = memoryStream.ToArray();

            try
	        {
                serialPort.Write(data, 0, data.Length);
            }
	        catch (Exception e)
	        {
		        lastErrorMessage = e.Message;
		        return false;
	        }
		
	        return true;
        }

        //reads information to the tcm and creates a tcmpacket
        private void run()
        {
            while (connected)
            {
                try
                {
                    TCMPacket packet;

                    do
                    {
                        packet = readTCMPacket(2000);
                    }
                    while (connected && packet == null);

                    if (packet == null)
                        continue;

                    TCMMessage tcmMessage = new TCMMessage(packet, id);
                    fireMessageReceived(tcmMessage);
                }
                catch (Exception e1)
                {
					Trace.WriteLine(e1.Message);
                    fireErrorOccurred(e1.Message);
                    disconnect();
                }
            }
        }

        private void fireMessageReceived(TCMMessage message)
        {
            if (messageReceived != null)
                messageReceived(message);
        }

        private void fireErrorOccurred(string error)
        {
            if (errorOccurred != null)
                errorOccurred(error);
        }

        public string getLastErrorMessage()
        {
            return lastErrorMessage;
        }


        public bool isConnected()
        {
            return connected;
        }

        // gets id of port if connected
        public long getID()
        {
            if (!connected)
            {
                lastErrorMessage = "Not connected";
                return -1L;
            }
            else
            {
                return id;
            }
        }

        // sends message to the serialport
        public bool sendMessage(byte[] message)
	    {
		    if (message == null)
		    {
			    lastErrorMessage = "Given message is NULL";
			    return false;
		    }
		
		    if (!connected)
		    {
			    lastErrorMessage = "Not connected";
			    return false;
		    }
		
		    try
		    {
                serialPort.Write(message, 0, message.Length);
		    }
		    catch (Exception e)
		    {
			    lastErrorMessage = e.Message;
			    return false;
		    }

		    return true;
	    }

        // Disconnects from the port
        public void disconnect()
	    {
		    if (!connected)
			    return;
		
		    connected = false;

            try { serialPort.Close(); } catch (Exception) { }
	    }

        //toHexString
        public static string getHexString(byte[] bs)
	    {
		    StringBuilder ret = new StringBuilder(bs.Length);

            foreach (byte b in bs)
                ret.Append(b.ToString("X2"));
		
		    return ret.ToString();
	    }
	
        //readTCMPacket with timeout limit IMPORTANT
	    private TCMPacket readTCMPacket(long timeout)
	    {	
		    TCMPacket tcmPacket = null;
            DateTime startTime = System.DateTime.Now.AddMilliseconds(timeout);

            while (startTime.Ticks > System.DateTime.Now.Ticks)
		    {
                while (serialPort.BytesToRead == 0 && startTime.Ticks > System.DateTime.Now.Ticks)
                    Thread.Sleep(10);

				if (serialPort.BytesToRead == 0)
					return null;

                int tmpByte = serialPort.ReadByte();
				switch (tmpByte)
			    {
				    case 0x55:
                        tmpByte = serialPort.ReadByte();
					    byte[] header = new byte[6];
					    header[0] = (byte)0x55;
					    header[1] = (byte)tmpByte;

						DateTime start = System.DateTime.Now.AddMilliseconds(2000);

                        while (start.Ticks > System.DateTime.Now.Ticks && serialPort.BytesToRead < 4)
							Thread.Sleep(10);

                        if (serialPort.BytesToRead < 4)
                            continue;

                        serialPort.Read(header, 2, 4);

					    if (Tools.calculateCRC8(header, 1, 4) != header[5])
					    {
							Trace.WriteLine("ESP3 Prüfsummenfehler im Header");
						    continue;
					    }
					
					    int dataLength = 0;
					    dataLength |= (header[1] & 0xFF) << 8;
					    dataLength |= header[2] & 0xFF;
					

					    byte[] data = new byte[dataLength];

                        start = System.DateTime.Now.AddMilliseconds(2000);

                        while (start.Ticks > System.DateTime.Now.Ticks && serialPort.BytesToRead < dataLength)
							Thread.Sleep(10);

                        if (serialPort.BytesToRead < dataLength)
                            continue;

                        serialPort.Read(data, 0, data.Length);
					
					    int optionalDataLength = header[3];
					    byte[] optionalData = new byte[optionalDataLength];

                        start = System.DateTime.Now.AddMilliseconds(2000);

                        while (start.Ticks > System.DateTime.Now.Ticks && serialPort.BytesToRead < optionalDataLength)
							Thread.Sleep(10);

                        if (serialPort.BytesToRead < optionalDataLength)
                            continue;

                        serialPort.Read(optionalData, 0, optionalDataLength);
					
					    int packetType = header[4];
                        int checksum = serialPort.ReadByte();

    					byte[] checkdata = new byte[dataLength + optionalDataLength];
                        System.Array.Copy(data, 0, checkdata, 0, dataLength);
                        System.Array.Copy(optionalData, 0, checkdata, dataLength, optionalDataLength);
    					
    					if (Tools.calculateCRC8(checkdata) != (byte)checksum)
    					{
							Debug.WriteLine("ESP3 Prüfsummenfehler im Datenbereich");
                            continue;
    					}

                        byte[] completeData = new byte[header.Length + data.Length + optionalData.Length + 1];
                        System.Array.Copy(header, 0, completeData, 0, header.Length);
                        System.Array.Copy(data, 0, completeData, header.Length, data.Length);
                        System.Array.Copy(optionalData, 0, completeData, header.Length + data.Length, optionalData.Length);
                        completeData[completeData.Length - 1] = (byte)checksum;
                        
                        return tcmPacket = new TCMPacket(TCMPacket.PROTOCOL_ESP3, packetType, data, optionalData, completeData, System.DateTime.Now);
					
				    default:
					    continue;
			    }
		    }
		
		    return null;
	    }

        internal bool writeCommandModule(byte[] command, long moduleID)
        {
            //Ok, altes ESP2 Format in das ESP3 Format umwandlen
            MemoryStream inputMemoryStream = new MemoryStream(command);
            BinaryReader binaryReader = new BinaryReader(inputMemoryStream);

            binaryReader.BaseStream.Seek(3, SeekOrigin.Begin); //sync, hseq etc. wird nicht benötigt

            byte org = binaryReader.ReadByte();
            byte byte3 = 0, byte2 = 0, byte1 = 0, byte0 = 0, byte4 = 0, byte5 = 0, byte6 = 0, byte7 = 0;
            byte id3 = 0, id2 = 0, id1 = 0, id0 = 0;
            byte status, dataLength = 0;

            //ORG umwandeln
            switch (org)
            {
                case 0x05:
                    org = 0xF6;
                    byte3 = binaryReader.ReadByte();
                    binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
                    dataLength = 7;
                    break;

                case 0x06:
                    org = 0xD5;
                    byte3 = binaryReader.ReadByte();
                    binaryReader.BaseStream.Seek(3, SeekOrigin.Current);
                    dataLength = 7;
                    break;

                case 0x07:
                    org = 0xA5;
                    byte3 = binaryReader.ReadByte();
                    byte2 = binaryReader.ReadByte();
                    byte1 = binaryReader.ReadByte();
                    byte0 = binaryReader.ReadByte();
                    dataLength = 10;
                    break;

                case 0xD4:
                    org = 0xD4;
                    byte6 = binaryReader.ReadByte();
                    byte5 = binaryReader.ReadByte();
                    byte4 = binaryReader.ReadByte();
                    byte3 = binaryReader.ReadByte();
                    byte2 = binaryReader.ReadByte();
                    byte1 = binaryReader.ReadByte();
                    byte0 = binaryReader.ReadByte();
                    dataLength = 13;
                    break;

                case 0xD2:
                    org = 0xD2;
                    if (command.Length == 18)
                    {
                        byte7 = binaryReader.ReadByte();
                        byte6 = binaryReader.ReadByte();
                        byte5 = binaryReader.ReadByte();
                        byte4 = binaryReader.ReadByte();
                        byte3 = binaryReader.ReadByte();
                        byte2 = binaryReader.ReadByte();
                        byte1 = binaryReader.ReadByte();
                        byte0 = binaryReader.ReadByte();
                        dataLength = 14;
                    }
                    else if (command.Length == 16)
                    {
                        byte5 = binaryReader.ReadByte();
                        byte4 = binaryReader.ReadByte();
                        byte3 = binaryReader.ReadByte();
                        byte2 = binaryReader.ReadByte();
                        byte1 = binaryReader.ReadByte();
                        byte0 = binaryReader.ReadByte();
                        dataLength = 12;
                    } else if (command.Length == 12)
                    {
                        byte1 = binaryReader.ReadByte();
                        byte0 = binaryReader.ReadByte();
                        dataLength = 8;
                    }
                 
                    break;

                default:
                    Debug.WriteLine("Unknown org byte.");
                    return false;
            }

            id3 = binaryReader.ReadByte();
            id2 = binaryReader.ReadByte();
            id1 = binaryReader.ReadByte();
            id0 = binaryReader.ReadByte();

            status = binaryReader.ReadByte();

            byte[] header = new byte[6];
            header[0] = 0x55;                               //sync byte
            header[1] = (byte)(dataLength >> 8);            //Datenlänge MSB
            header[2] = (byte)dataLength;                   //Datenlänge LSB
            header[3] = 0x06;                               //Datenlänge optionale Daten
            header[4] = 0x01;                               //Typ Radiotelegramm
            header[5] = Tools.calculateCRC8(header, 1, 4);  //CRC8 des Headers

            MemoryStream memoryStream = new MemoryStream();

            memoryStream.WriteByte(org);                            //Choice							

            switch (org)                                    //Payload
            {
                case 0xD5:
                case 0xF6:
                    memoryStream.WriteByte(byte3);
                    break;

                case 0xA5:
                    memoryStream.WriteByte(byte3);
                    memoryStream.WriteByte(byte2);
                    memoryStream.WriteByte(byte1);
                    memoryStream.WriteByte(byte0);
                    break;

                case 0xD4:
                    memoryStream.WriteByte(byte6);
                    memoryStream.WriteByte(byte5);
                    memoryStream.WriteByte(byte4);
                    memoryStream.WriteByte(byte3);
                    memoryStream.WriteByte(byte2);
                    memoryStream.WriteByte(byte1);
                    memoryStream.WriteByte(byte0);
                    break;

                case 0xD2:
                    if (command.Length == 18)
                    {
                        memoryStream.WriteByte(byte7);
                        memoryStream.WriteByte(byte6);
                        memoryStream.WriteByte(byte5);
                        memoryStream.WriteByte(byte4);
                        memoryStream.WriteByte(byte3);
                        memoryStream.WriteByte(byte2);
                        memoryStream.WriteByte(byte1);
                        memoryStream.WriteByte(byte0);
                    }
                    else if (command.Length == 16)
                    {
                        memoryStream.WriteByte(byte5);
                        memoryStream.WriteByte(byte4);
                        memoryStream.WriteByte(byte3);
                        memoryStream.WriteByte(byte2);
                        memoryStream.WriteByte(byte1);
                        memoryStream.WriteByte(byte0);
                    }
                    else if (command.Length == 12)
                    {
                        memoryStream.WriteByte(byte1);
                        memoryStream.WriteByte(byte0);
                    }

                    break;
            }

            memoryStream.WriteByte(id3);							//Sender-ID MSB
            memoryStream.WriteByte(id2);
            memoryStream.WriteByte(id1);
            memoryStream.WriteByte(id0);							//Sender-ID LSB

            memoryStream.WriteByte(status);						    //Status

            memoryStream.WriteByte(0x03);							//Subtelegramme

            memoryStream.WriteByte((byte)(moduleID >> 24));                           //Empfänger MSB
            memoryStream.WriteByte((byte)(moduleID >> 16));                           //(FFFFFFFF = Broadcast)
            memoryStream.WriteByte((byte)(moduleID >> 8));
            memoryStream.WriteByte((byte)moduleID);                           //Empfänger MSB

            memoryStream.WriteByte(0xFF);							//RSSI (beim Senden FF)

            memoryStream.Close();
            byte[] data = memoryStream.ToArray();

            byte dataCRC = Tools.calculateCRC8(data);

            memoryStream = new MemoryStream();

            try
            {
                memoryStream.Write(header, 0, header.Length);
                memoryStream.Write(data, 0, data.Length);
            }
            catch (IOException e)
            {
                Trace.WriteLine(e.StackTrace);
                return false;
            }

            memoryStream.WriteByte(dataCRC);
            memoryStream.Close();
            data = memoryStream.ToArray();

            try
            {
                serialPort.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                lastErrorMessage = e.Message;
                return false;
            }

            return true;
        }

        //write into the port
        private bool write(byte[] command)
	    {
		    try
		    {
			    serialPort.Write(command, 0, command.Length);
		    }
		    catch (Exception e)
		    {
			    return false;
		    }
		
		    return true;
	    }
    }
}
