using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KungfuWpfExamples.ViewModel
{
    public class Item : ViewModelBase
    {

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set => SetProperty(ref _isSelected, value);
        }

        public string Name { get; set; }
    }
}
