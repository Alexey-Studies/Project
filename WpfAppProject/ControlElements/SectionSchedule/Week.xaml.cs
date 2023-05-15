using System.Windows.Controls;

namespace WpfAppProject.ControlElements.SectionSchedule
{
    /// <summary>
    /// Логика взаимодействия для Week.xaml
    /// </summary>
    public partial class Week : UserControl
    {
        public string Title
        {
            get => (string)WeekTitle.Content;
            set => WeekTitle.Content = value;
        }

        public Week()
        {
            InitializeComponent();
        }
    }
}
