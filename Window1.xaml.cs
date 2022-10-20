using System.Windows;
using System.Windows.Controls.Primitives;
using System;
using System.Windows.Media.Animation;

namespace Coil_Windie_Boi
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        char[] startData = new char[2] { '1', '\n' };
        char[] pauseData = new char[2] { '2', '\n' };
        bool removal_started = true;
        UIElement homePage = new HomePage();
        UIElement outputPage = new OutputPage();
        UIElement settingsPage = new SettingsPage();

        public Window1()
        {
            InitializeComponent();
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(homePage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation ani = new DoubleAnimation(1, TimeSpan.FromSeconds(2));
            mainGrid.BeginAnimation(OpacityProperty, ani);
        }

        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(homePage);
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(outputPage);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(settingsPage);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (InternalSerialPort.serialPort.IsOpen)
            {
                if (removal_started)
                    InternalSerialPort.SendData(startData);
                else
                    InternalSerialPort.SendData(pauseData);

                removal_started = !removal_started;
            } 
            else
            {
                ListShare.DataList.Insert(0, "Not Connected. Go to the settings menu.");
            }
        }
    }
}
