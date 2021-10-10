using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Coil_Windie_Boi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] sendData = new char[1];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendData(char dataToSend, string dataSent, char confirmationData, int responseInt)
        {
            if (!InternalSerialPort.serialPort.IsOpen)
            {
                try
                {
                    sendData[0] = dataToSend;
                    InternalSerialPort.serialPort.Open();

                    InternalSerialPort.serialPort.Write(sendData, 0, 1);

                    InternalSerialPort.serialPort.Close();
                    InternalSerialPort.serialPort.Open();

                    InternalSerialPort.serialPort.DiscardInBuffer();

                    if (InternalSerialPort.serialPort.ReadByte() == responseInt)
                        OutputList.Items.Insert(0, "Data Sent : " + dataSent);
                    else
                        OutputList.Items.Insert(0, "Failed to Send : " + dataSent);

                    InternalSerialPort.serialPort.Close();

                    sendData[0] = confirmationData;
                    InternalSerialPort.serialPort.Open();

                    InternalSerialPort.serialPort.Write(sendData, 0, 1);

                    InternalSerialPort.serialPort.Close();
                }
                catch
                {
                    OutputList.Items.Insert(0, "Please reconnect device.");
                }
            }
        }


        private void CalibrateButton_Click(object sender, RoutedEventArgs e)
        {
            OutputListGrid.Width = 600;
            try
            {
                //InternalSerialPort.serialPort = new InternalSerialPort.serialPort(PortNameText.Text, int.Parse(BaudRateText.Text), Parity.None, 8, StopBits.One);

                PingButton.IsEnabled = true;
                CalculateButton.IsEnabled = true;

                OutputList.Items.Insert(0, "Calibration complete");
            }
            catch
            {
                OutputList.Items.Insert(0, "Invalid port or baud rate");
            }
        }

        private void PingButton_Click(object sender, RoutedEventArgs e)
        {
            // SendData((char)10, "Ping Data", (char)110, 50);
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            SendData((char)11, "Calculate Data", (char)111, 51);
            StartButton.IsEnabled = true;
        }

        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SendData((char)12, StartButton.Content.ToString() + " Data", (char)112, 52); 

            if (StartButton.Content.ToString() == "Start" || StartButton.Content.ToString() == "Resume")
                StartButton.Content = "Pause";
            else
                StartButton.Content = "Resume";
        }
    }
}
