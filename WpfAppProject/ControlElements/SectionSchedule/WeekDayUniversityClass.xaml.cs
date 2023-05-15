using System.Windows.Controls;

namespace WpfAppProject.ControlElements.SectionSchedule
{
    /// <summary>
    /// Логика взаимодействия для WeekDayUniversityClass.xaml
    /// </summary>
    public partial class WeekDayUniversityClass : UserControl
    {
        public string ClassTitle
        {
            get => (string)Title.Content;
            set => Title.Content = value;
        }

        public WeekDayUniversityClass()
        {
            InitializeComponent();
        }
    }
}
