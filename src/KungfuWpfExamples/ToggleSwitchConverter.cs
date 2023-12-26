using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KungfuWpfExamples
{
    public class ToggleSwitchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height)
            {
                var property = parameter as string;
                switch (property)
                {
                    case "ButtonSize":
                        return (height - 8);
                    case "OutsideCorner":
                        return new CornerRadius(height / 2);
                    case "InteriorCorner":
                        return new CornerRadius((height - 8) / 2);
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
