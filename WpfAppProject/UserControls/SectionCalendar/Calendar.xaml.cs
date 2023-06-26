using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfAppProject.UserControls.SectionCalendar
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
            }
        }

        public Calendar()
        {
            InitializeComponent();
            InitializeProperties();
            SetDate();
        }
        private void InitializeProperties()
        {
            foreach (var child in MainGrid.Children)
            {
                if (child is Month month)
                {
                    Months.Add(month);
                }
            }
        }
        public void SetDate()
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

        public Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> GetData()
        {
            var data = new Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>>();

            foreach (Month month in Months)
            {
                // Создаем элемент словаря с ключом - началом месяца
                DateTime firstDayOfMonth = new DateTime(month.Date.Year, month.Date.Month, 1);
                Dictionary<DateTime, Dictionary<string, bool>> monthData = month.GetData();
                data[firstDayOfMonth] = monthData;
            }

            return data;
        }

        public void SetData(Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> data)
        {
            foreach (var month in Months)
            {
                var key = new DateTime(month.Date.Year, month.Date.Month, 1);

                if (data.ContainsKey(key))
                {
                    month.SetData(data[key]);
                }
            }
        }
    }
}