using System;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;

namespace Coil_Windie_Boi
{
    /// <summary>
    /// Interaction logic for OutputPage.xaml
    /// </summary>
    public partial class OutputPage : UserControl
    {
        public OutputPage()
        {
            InitializeComponent();

            OutputList.ItemsSource = new List<string>();
            OutputList.ItemsSource = ListShare.DataList;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            OutputList.ItemsSource = new List<string>();
            OutputList.ItemsSource = ListShare.DataList;
        }
    }
}
