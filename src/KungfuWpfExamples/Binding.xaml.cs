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
    /// Binding.xaml 的交互逻辑
    /// </summary>
    public partial class Binding : Window
    {
        public Binding()
        {
            InitializeComponent();

            //DataContext = new BindingViewModel();
        }
    }


    public class BindingViewModel 
    {
        public string Message { get; set; }
        public override string ToString()
        {
            return "Empty";
        }
    }

    public class MyResources
    {
        public string Message => "Hello Resource";
        public static string StaticString = "Static String";
    }
}
