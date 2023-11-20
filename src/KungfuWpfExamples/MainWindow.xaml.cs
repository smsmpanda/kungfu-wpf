using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#nullable disable

namespace KungfuWpfExamples;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ICommand SayHelloCommand { get; set; }
    public ICommand SayGoodbyeCommand { get; set; }
    public ObservableCollection<Student> Students { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        Students = new ObservableCollection<Student>
            {
                new Student { Name = "Alice", Age = 20 },
                new Student { Name = "Bob", Age = 22 },
                new Student { Name = "Charlie", Age = 19 }
            };

        SayHelloCommand = new RelayCommand(SayHello);
        SayGoodbyeCommand = new RelayCommand(SayGoodbye);
    }

    private void lstStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        CollectionViewSource.GetDefaultView(Students).Refresh();
    }
    private void SayHello(object parameter)
    {
        MessageBox.Show("Hello, WPF!");
    }
    private void SayGoodbye(object parameter)
    {
        MessageBox.Show("Goodbye, WPF!");
    }

    public class Student : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}