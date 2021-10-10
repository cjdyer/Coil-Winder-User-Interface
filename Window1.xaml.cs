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
        char[] startData = new char[3] { 'B', 'S', '\n' };
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
                InternalSerialPort.SendData(startData);
            } 
            else
            {
                ListShare.DataList.Insert(0, "Not Calibrated. Go to the settings menu.");
            }
        }
    }
}
