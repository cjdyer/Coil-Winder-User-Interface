using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace Coil_Windie_Boi
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        char[] pingData = new char[2] { '1', '\n' };

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void CalibrateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InternalSerialPort.serialPort.Close();

                InternalSerialPort.serialPort.BaudRate = int.Parse(BaudRateText.Text);
                InternalSerialPort.serialPort.Parity = Parity.None;
                InternalSerialPort.serialPort.PortName = PortNameText.Text;
                InternalSerialPort.serialPort.DataBits = 8;
                InternalSerialPort.serialPort.StopBits = StopBits.One;
                InternalSerialPort.serialPort.Open();

                PingButton.IsEnabled = true;
                CalculateButton.IsEnabled = true;

                ListShare.DataList.Insert(0, "Connected");
                ListShare.Calibrated = true;

                InternalSerialPort.serialPort.DataReceived += (_, EventArgs) =>
                {
                    InternalSerialPort.NewDataRecieved();
                };
            }
            catch(FormatException)
            {
                ListShare.DataList.Insert(0, "Invalid port or baud rate.");
            }
            catch (System.IO.IOException)
            {
                ListShare.DataList.Insert(0, "Please connect device.");
            }
        }

        private void PingButton_Click(object sender, RoutedEventArgs e)
        {
            InternalSerialPort.SendData(pingData);
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            char[] charArr = SpeedText.Text.ToCharArray();
            char[] calculateData = new char[charArr.Length + 3];
            calculateData[0] = 'S';
            for (int i = 0; i < charArr.Length; i++)
            {
                calculateData[i + 2] = charArr[i];
                calculateData[i + 3] = '\n';
            }
            Console.WriteLine(calculateData);
            InternalSerialPort.SendData(calculateData);

        }
    }
}
