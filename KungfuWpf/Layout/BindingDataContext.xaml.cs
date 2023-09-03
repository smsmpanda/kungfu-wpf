using KungfuWpf.Model;
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

namespace KungfuWpf.Layout
{
    /// <summary>
    /// BindingDataContext.xaml 的交互逻辑
    /// </summary>
    public partial class BindingDataContext : Window
    {
        public BindingDataContext()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myGrid.DataContext = new Car {
                Id = 1,
                Name = "Build Your Dream",
                Color = "White",
                Width = 5.5f,
                Description = "比亚迪 - 世界的帝豪"
            };
        }
    }
}
