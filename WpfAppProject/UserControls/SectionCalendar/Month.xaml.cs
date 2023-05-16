using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfAppProject.UserControls.SectionCalendar
{
    /// <summary>
    /// Логика взаимодействия для Month.xaml
    /// </summary>
    public partial class Month : UserControl, INotifyPropertyChanged
    {
        public List<Label> Weeks { get; set; }

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

        // Методы и реализация

        public Month()
        {
            InitializeComponent();
            InitializeProperties();
            Render();
        }

        /// <summary>
        /// Инициализация свойств.
        /// </summary>
        private void InitializeProperties()
        {
            Weeks = new List<Label>()
            {
                Week1,
                Week2,
                Week3,
                Week4,
                Week5,
                Week6
            };

            foreach (var child in MyGrid.Children)
            {
                if (child is Button button)
                {
                    button.Padding = new Thickness(0);
                    ButtonsDate.Add(button);
                }
            }
        }

        /// <summary>
        /// Отрисовывает месяц.
        /// </summary>
        public void Render()
        {
            Clear();
            SetMonthName();
            FillDataMonth();
        }

        // Метод определения имени месяца.
        private void SetMonthName()
        {
            LabelMonth.Content = DateTime.ParseExact(Date.ToShortDateString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM");
        }

        // Метод для обновления информации.
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        // Очищает данные месяца.
        public void Clear()
        {
            foreach (var item in Weeks)
            {
                item.Content = "—";
            }

            for (int i = 0; i < ButtonsDate.Count; i++)
            {
                ButtonsDate[i].Content = "";
                ButtonsDate[i].IsEnabled = true;
                ButtonsDate[i].Background = DayBackground;
            }
        }

        private void SetDayContent(int index, object content)
        {
            Viewbox viewbox = new Viewbox()
            {
                Name = "Container"
            };

            Label label = new Label()
            {
                Name = "Day",
                Content = content.ToString(),
                Padding = new Thickness(0)
            };

            viewbox.Child = label;

            ButtonsDate[index].Content = viewbox;
        }

        /// <summary>
        /// Заполняет данными месяц, числами и типами недель.
        /// </summary>
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
                SetDayContent(j, previosMonth--);
                ButtonsDate[j].IsEnabled = false;
            }

            for (int day = 1; day <= DateTime.DaysInMonth(Date.Year, Date.Month); butInd++, day++)
            {
                SetDayContent(butInd, day);

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
                SetDayContent(butInd, nextMonth++);
                ButtonsDate[butInd].IsEnabled = false;
            }
        }

        /// <summary>
        /// Получает день из кнопки.
        /// </summary>
        /// <param name="button">Кнопка месяца, из которой нужно полуить число.</param>
        /// <returns></returns>
        private int GetDay(Button button)
        {
            int result = 0;

            if (button.Content is Viewbox viewbox && viewbox.Child is Label label)
            {
                Int32.TryParse((string)label.Content, out result);
            }

            return result;
        }

        // Обработчик клика на опцию меню.
        private void MenuItem_Click(object sender, RoutedEventArgs e, Button button)
        {
            if (button.Background != DayOffBackground)
            {
                button.Background = DayOffBackground;
            }
            else
            {
                button.Background =
                    (Date.ToString("M.yyyy") == DateTime.Now.ToString("M.yyyy")
                        &&
                        GetDay(button) == DateTime.Now.Day)
                    ?
                    TodayBackground : DayBackground;
            }
        }

        // Обработчик нажатия на день.
        private void Button_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;

            if (!button.IsEnabled)
            {
                return;
            }

            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();

            menuItem.Header = " Выходной ";

            // Установить обработчик события клика.
            menuItem.Click += (Sender, EventArgs) =>
                {
                    MenuItem_Click(Sender, EventArgs, button);
                };

            contextMenu.Items.Add(menuItem);

            button.ContextMenu = contextMenu;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                button.ContextMenu.IsOpen = true;
            }
        }
    }
}

