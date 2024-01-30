using System;
using System.Collections.Generic;
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

namespace KungfuWpfExamples
{
    /// <summary>
    /// CustomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CustomeWindow : Window
    {
        public CustomeWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/app.ico"));
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonMaximize_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.btnMaximize.Content = "\xe601";
            }
            else {
                this.WindowState = WindowState.Maximized;
                this.btnMaximize.Content = "\xe600";
            }
        }

        private void Window_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
