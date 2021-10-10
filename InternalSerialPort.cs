using System.IO.Ports;

namespace Coil_Windie_Boi
{
    class InternalSerialPort
    {
        public static SerialPort serialPort { get; set; } = new SerialPort();

        internal static void NewDataRecieved()
        {
            string data = serialPort.ReadLine();
            if (data != null)
                    ListShare.DataList.Insert(0, data);
        }

        public static void SendData(char[] data)
        {
            if (serialPort.IsOpen)
                serialPort.Write(data, 0, data.Length);
            else
                ListShare.DataList.Insert(0, "Please reconnect.");
        }
    }
}
