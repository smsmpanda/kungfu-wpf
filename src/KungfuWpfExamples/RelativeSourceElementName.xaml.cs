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
    /// RelativeSourceElementName.xaml 的交互逻辑
    /// </summary>
    public partial class RelativeSourceElementName : Window
    {
        public RelativeSourceElementName()
        {
            InitializeComponent();
        }

        private void Window_Onload(object sender, RoutedEventArgs e)
        {
            var txt = new TextBox 
            {
                Name="txt"
            };
            this.RegisterName("txt",txt);
            panel.Children.Add(txt);
        }
    }
}
