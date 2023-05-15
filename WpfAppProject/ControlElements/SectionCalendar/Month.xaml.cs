using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfAppProject.ControlElements.SectionCalendar
{
    /// <summary>
    /// Логика взаимодействия для Month.xaml
    /// </summary>
    public partial class Month : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// инициализация свойств
        /// </summary>
        public List<Label> Weeks { get; set; } = new List<Label>();

        public List<Button> ButtonsDate { get; set; } = new List<Button>();

        private SolidColorBrush _dayBackground = Brushes.White;

        public SolidColorBrush DayBackground
        {
            get => _dayBackground;
            set
            {
                if (_dayBackground != value)
                {
                    _dayBackground = value ?? Brushes.White;
                    OnPropertyChanged(nameof(DayBackground));
                    Render();
                }
            }
        }
        private SolidColorBrush _DayOffBackground = Brushes.LightGreen;

        public SolidColorBrush DayOffBackground
        {
            get => _DayOffBackground;
            set
            {
                if (_DayOffBackground != value)
                {
                    _DayOffBackground = value ?? Brushes.LightGreen;
                    OnPropertyChanged(nameof(DayOffBackground));
                    Render();
                }
            }
        }

        private SolidColorBrush _todayBackground = Brushes.LightBlue;

        public SolidColorBrush TodayBackground
        {
            get => _todayBackground;
            set
            {
                if (_todayBackground != value)
                {
                    _todayBackground = value;
                    OnPropertyChanged(nameof(TodayBackground));
                    Render();
                }
            }
        }

        private DateTime _Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        private bool _StartWeekType = true;
        public bool StartWeekType
        {
            get => _StartWeekType;
            set
            {
                if (value != _StartWeekType)
                {
                    _StartWeekType = value;
                    OnPropertyChanged("StartWeekType");
                    Render();
                }
            }
        }
        public bool EndWeekType { get; set; }
        public DateTime Date
        {
            get => _Date;
            set
            {
                if (value != _Date && value != null)
                {
                    _Date = value;
                    OnPropertyChanged("Date");
                    Render();
                }
            }
        }

        private bool _FullBlock = false;
        public bool FullBlock
        {
            get => _FullBlock;
            set
            {
                _FullBlock = value;
                Render();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// методы и реализация
        /// </summary>

        public Month()
        {
            InitializeComponent();
            InitializeProperties(); //инициализация коллекций
            Render();
        }

        public void Render()
        {
            CompletionMonthName(Date);
            Clear();
            FillDataMonth();
        }

        //метод определения имени месяца
        private void CompletionMonthName(DateTime date)
        {
            LabelMonth.Content = DateTime.ParseExact(date.ToShortDateString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM");
        }

        //метод для обновления информации
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void Clear()
        {
            foreach (var item in Weeks)
            {
                item.Content = "—";
            }
            for (int i = 0; i < myGrid.Children.Count; i++)
            {
                ButtonsDate[i].Content = "";
                ButtonsDate[i].IsEnabled = true;
                ButtonsDate[i].Background = DayBackground;
            }
        }

        private void InitializeProperties()
        {
            Weeks.Add(Week1);
            Weeks.Add(Week2);
            Weeks.Add(Week3);
            Weeks.Add(Week4);
            Weeks.Add(Week5);
            Weeks.Add(Week6);
            for (int i = 0; i < myGrid.Children.Count; i++)
            {
                ButtonsDate.Add((Button)myGrid.Children[i]);
            }
        }

        private void FillDataMonth()
        {
            int previosMonth = DateTime.DaysInMonth(Date.Year - 1, 12);
            int nextMonth = 1;
            int weekNum = -1;
            bool weekType = StartWeekType;
            int butInd = (int)(new DateTime(Date.Year, Date.Month, 1).DayOfWeek + 6) % 7;

            if ((Date.Month - 1) > 1)
            {
                previosMonth = DateTime.DaysInMonth(Date.Year, Date.Month - 1);
            }

            //обозначение кнокпок вне месяца неактивными
            for (int j = butInd - 1; j >= 0; j--)
            {
                ButtonsDate[j].Content = previosMonth--;
                ButtonsDate[j].IsEnabled = false;
            }

            for (int day = 1; day <= DateTime.DaysInMonth(Date.Year, Date.Month); butInd++, day++)
            {
                ButtonsDate[butInd].Content = day;

                // Подсветить текущий день
                if ((Date.ToString("M.yyyy") == DateTime.Now.ToString("M.yyyy")) && day == DateTime.Now.Day)
                {
                    ButtonsDate[butInd].Background = TodayBackground;
                }
                // Сделать дни до начала семестра
                if (FullBlock || day < Date.Day)
                {
                    ButtonsDate[butInd].IsEnabled = false;
                }
                // Обозначение типа недели
                if (!FullBlock && day >= Date.Day && weekNum != butInd / 7)
                {
                    weekNum = butInd / 7;
                    Weeks[weekNum].Content = weekType ? "В" : "Н";
                    weekType = !weekType;
                }
                // Если последний день месяца это воскресенье то тип последней недели поменять на противоположный
                if (day == DateTime.DaysInMonth(Date.Year, Date.Month))
                {
                    EndWeekType = (new DateTime(Date.Year, Date.Month, 1)).AddMonths(1).AddDays(-1).DayOfWeek != 0 ? !weekType : weekType;
                }

            }

            //обозначение оставшихся кнокпок вне месяца неактивными
            for (; butInd < 42; butInd++)
            {
                ButtonsDate[butInd].Content = nextMonth++;
                ButtonsDate[butInd].IsEnabled = false;
            }
        }

        // Обработчик клика на опцию меню
        private void MenuItem_Click(object sender, RoutedEventArgs e, Button button)
        {
            if (button.Background != DayOffBackground)
            {
                button.Background = DayOffBackground;
            }
            else
            {
                button.Background = (Date.ToString("M.yyyy") == DateTime.Now.ToString("M.yyyy") && (int)button.Content == DateTime.Now.Day) ? TodayBackground : DayBackground;
            }
        }

        //обработчик меню
        private void Button_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button button = sender as Button;

            if (!button.IsEnabled)
            {
                return;
            }

            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = " Выходной ";

            menuItem.Click += (Sender, EventArgs) => { MenuItem_Click(Sender, EventArgs, button); };// событие клика по опции с передачей базовой кнопки

            contextMenu.Items.Add(menuItem);

            button.ContextMenu = contextMenu;
        }
    }
}

