using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KungfuWpf.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KungfuWpf.Model
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Name = "hello";
            ShowCommand = new RelayCommand(Show);
        }

        public RelayCommand ShowCommand { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }
        public void Show()
        {
            Name = "点击了按钮！";
            MessageBox.Show("点击了按钮！");
        }
    }
}
