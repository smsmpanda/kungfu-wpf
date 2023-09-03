using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KungfuWpf.Model
{
    public class Car : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
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
                if(value > 1000) {
                    throw new ArgumentException("The car's width max width not gt 1000.");
                }
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
