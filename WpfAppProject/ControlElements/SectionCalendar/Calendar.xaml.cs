using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfAppProject.ControlElements.SectionCalendar
{
    /// <summary>
    /// Логика взаимодействия для Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public List<Month> Months { get; set; } = new List<Month>();

        private DateTime _Semestr = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateTime StartDate
        {
            get
            {
                return _Semestr;
            }
            set
            {
                _Semestr = value;
                Render();
            }
        }

        public Calendar()
        {
            InitializeComponent();
            InitializeProperties();
            Render();
        }
        private void InitializeProperties()
        {
            if (Months.Count == 0)
            {
                Months.Add(Month1);
                Months.Add(Month2);
                Months.Add(Month3);
                Months.Add(Month4);
                Months.Add(Month5);
                Months.Add(Month6);
            }
        }
        public void Render()
        {
            short secondHalf = Convert.ToInt16(StartDate.Month > 6);

            for (int i = 0; i < Months.Count; i++)
            {
                Months[i].FullBlock = StartDate.Month > (i + 1 + secondHalf * 6);
                Months[i].Date = new DateTime(StartDate.Year, i + 1 + secondHalf * 6, StartDate.Month == (i + 1 + secondHalf * 6) ? StartDate.Day : 1);
                if (!Months[i].FullBlock && i - 1 >= 0 && !Months[i - 1].FullBlock)
                    Months[i].StartWeekType = Months[i - 1].EndWeekType;
            }
        }
    }
}
