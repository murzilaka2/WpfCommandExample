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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new DataSource();
        }
    }
    class DataSource : INotifyPropertyChanged
    {
        private readonly Command blueCommand;
        private readonly Command greenCommand;
        private readonly Command redCommand;
        private string selectedColor = "Black";
        public DataSource()
        {
            blueCommand = new DelegateCommand(
            () => SelectedColor = "Blue",
            () => SelectedColor != "Blue"
            );
            greenCommand = new DelegateCommand(() => SelectedColor = "Green", () => SelectedColor != "Green");
            redCommand = new DelegateCommand(
            () => SelectedColor = "Red",
            () => SelectedColor != "Red"
            );
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals(nameof(SelectedColor)))
                {
                    blueCommand.RaiseCanExecuteChanged();
                    greenCommand.RaiseCanExecuteChanged();
                    redCommand.RaiseCanExecuteChanged();
                }
            };
        }
        public ICommand BlueCommand => blueCommand;
        public ICommand GreenCommand => greenCommand;
        public ICommand RedCommand => redCommand;
        public string SelectedColor
        {
            get => selectedColor;
            set
            {
                if (!selectedColor.Equals(value))
                {
                    selectedColor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedColor)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}