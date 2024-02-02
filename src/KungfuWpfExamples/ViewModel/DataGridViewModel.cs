using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KungfuWpfExamples.ViewModel
{
    public class DataGridViewModel : ViewModelBase
    {
        private bool _isAllSelected;
        private ObservableCollection<Item> _myItems;
        public DataGridViewModel()
        {
            MyItems = new ObservableCollection<Item>() 
            {
                new Item { Name = "Bob" },
                new Item { Name = "Slice" },
                new Item { Name = "John" },
                new Item { Name = "Lisa" },
            };

            AllSelectedCommand = new AnotherCommandImplementation(v => {
                foreach (var item in MyItems) 
                {
                    item.IsSelected = (bool)v;
                }
            });
        }

        public bool IsAllSelected
        {
            get { return _isAllSelected; }
            set
            {
                foreach (var item in MyItems)
                {
                    item.IsSelected = value;
                }
                SetProperty(ref _isAllSelected, value);
            }
        }

        public ObservableCollection<Item> MyItems
        {
            get { return _myItems; }
            set
            {
                _myItems = value;
                SetProperty(ref _myItems, value);
            }
        }

        public AnotherCommandImplementation AllSelectedCommand { get; }
    }
}
