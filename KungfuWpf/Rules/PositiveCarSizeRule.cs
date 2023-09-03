using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KungfuWpf.Rules
{
    public class PositiveCarSizeRule : ValidationRule
    {
        private float _min = 5;
        private float _max = 10f;

        public float Min
        {
            get => _min;
            set { _min = value; }
        }

        public float Max
        {
            get => _max;
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            float val = 0;
            try
            {
                if (((string)value).Length > 0)
                {
                    val = float.Parse((string)value, NumberStyles.Any, cultureInfo);
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Illegal characters.");
            }

            if (val < Min || val > Max)
            {
                return new ValidationResult(false, "Car's size is range " + Min + " to " + Max);
            }
            return new ValidationResult(true, null);
        }
    }
}
