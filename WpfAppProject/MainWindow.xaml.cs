using System.Windows;
using WpfAppProject.Clasess;
using WpfAppProject.UserControls.SectionCalendar;

namespace WpfAppProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SaveTabItemCalendar.InstanceOf(this);
            LoadTabItemCalendar.Load(this);
        }
    }
}
