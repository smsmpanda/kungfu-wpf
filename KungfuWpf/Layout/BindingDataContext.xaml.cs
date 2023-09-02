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

    public class Car : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e) 
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, e);
            }
        }

        public int Id { get; set; }

        private string _name;
        public string Name 
        { 
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        private string _color;
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Color"));
            }
        }

        private float _width;
        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Width"));
            }
        }

        private float? _height;
        public float? Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Height"));
            }
        }

        private string _descirption;
        public string Description
        {
            get => _descirption;
            set
            {
                _descirption = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }
    }
}
