using KungfuWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

#nullable disable

namespace KungfuWpf.DataTemplateSelectors
{
    internal class SaleTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultSaleTemplate { get; set; }
        public DataTemplate OnlineSaleCarTemplate { get; set; }
        public string PropertyToEvaluate { get; set; }
        public string PropertyValue { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Car car = (Car)item;

            PropertyInfo property = typeof(Car).GetProperty(PropertyToEvaluate);

            if (property.GetValue(car, null).ToString() == PropertyValue)
            {
                return OnlineSaleCarTemplate;
            }
            else
            {
                return DefaultSaleTemplate;
            }
        }
    }
}
#nullable restore
