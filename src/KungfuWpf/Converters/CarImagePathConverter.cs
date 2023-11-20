using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace KungfuWpf.Converters
{
    public class CarImagePathConverter : IValueConverter
    {
        private string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(),"Images");
        public string ImageDirectory 
        {
            get => imageDirectory; 
            set => imageDirectory = value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = Path.Combine(imageDirectory, value.ToString());
            return new BitmapImage(new Uri(imagePath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
