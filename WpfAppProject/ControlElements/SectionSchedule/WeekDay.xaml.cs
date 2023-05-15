using System.Windows.Controls;

namespace WpfAppProject.ControlElements.SectionSchedule
{
    /// <summary>
    /// Логика взаимодействия для WeekDay.xaml
    /// </summary>
    public partial class WeekDay : UserControl
    {
        public string DayTitle
        {
            get { return (string)Title.Content; }
            set { Title.Content = value; }
        }

        public WeekDay()
        {
            InitializeComponent();
        }
    }
}
