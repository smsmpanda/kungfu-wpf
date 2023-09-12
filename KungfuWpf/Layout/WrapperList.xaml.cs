using KungfuWpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// WrapperList.xaml 的交互逻辑
    /// </summary>
    public partial class WrapperList : Window
    {
        public WrapperList()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Name = "Build Your Dream - HAN",
                    Color = "White",
                    Width = 5.2f,
                    Description = "Build Your Dream - 比亚迪 - 启迪世界！",
                    ImagePath = "BYD.png",
                    Height = 1.45f,
                    SaleType = "Online"
                },
                new Car
                {
                    Id = 2,
                    Name = "Geely Automobile - XINGYAO",
                    Color = "Black",
                    Width = 5.3f,
                    Description = "吉利汽车 - 世界帝豪！",
                    ImagePath = "JL.png",
                    Height = 1.48f,
                    SaleType = "Offline"
                },
                new Car
                {
                    Id = 3,
                    Name = "XIAO PENG",
                    Color = "Fluorescent",
                    Width = 5.4f,
                    Description = "超长续航-1300KM，值得你拥有！",
                    ImagePath = "XP.png",
                    Height = 1.58f,
                    SaleType = "Offline"
                },
                new Car
                {
                    Id = 4,
                    Name = "LI XIANG ONE",
                    Color = "Silver",
                    Width = 5.5f,
                    Description = "土豪的世界你不懂，吊打一切！",
                    ImagePath = "LX.png",
                    Height = 1.51f,
                    SaleType = "Online"
                },
                new Car
                {
                    Id = 5,
                    Name = "AITO",
                    Color = "White",
                    Width = 5.45f,
                    Description = "世界之光 -  HUAWEI",
                    ImagePath = "AITO.png",
                    Height = 1.31f,
                    SaleType = "Online"
                },
                new Car
                {
                    Id = 6,
                    Name = "Fei Fan",
                    Color = "White",
                    Width = 5.7f,
                    Description = "非凡体验 -  非凡",
                    ImagePath = "FF.png",
                    Height = 1.56f,
                    SaleType = "Offline"
                },
                new Car
                {
                    Id = 7,
                    Name = "Chang An",
                    Color = "White",
                    Width = 5.45f,
                    Description = "安全驾驶 -  Zooker",
                    ImagePath = "CN.png",
                    Height = 1.57f,
                    SaleType = "Online"
                }
            };

            myCarList.ItemsSource = new ObservableCollection<Car>(cars);
        }
    }
}
