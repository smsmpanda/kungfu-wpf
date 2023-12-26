using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KungfuWpfExamples
{
    /// <summary>
    /// WPF2D.xaml 的交互逻辑
    /// </summary>
    public partial class WPF2D : Window, INotifyPropertyChanged
    {
        private const double AnglePerHour = 30.0;
        private const double AnglePerMinute = 6.0;
        private const double AnglePerSecond = 6.0;


        public event PropertyChangedEventHandler? PropertyChanged;

        public double HourAngle { get; private set; }
        public double MinuteAngle { get; private set; }
        public double SecondAngle { get; private set; }
        public WPF2D()
        {
            InitializeComponent();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            DataContext = this;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;


            HourAngle = currentTime.Hour * AnglePerHour + currentTime.Minute * AnglePerMinute / 12;
            MinuteAngle = currentTime.Minute * AnglePerMinute + currentTime.Second * AnglePerSecond / 60;
            SecondAngle = currentTime.Second * AnglePerSecond;


            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HourAngle)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinuteAngle)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondAngle)));
        }
    }
}
