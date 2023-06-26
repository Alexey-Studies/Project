using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfAppProject.Clasess;

namespace WpfAppProject.UserControls.SectionCalendar
{
    /// <summary>
    /// Логика взаимодействия для TabItemCalendar.xaml
    /// </summary>
    public partial class TabItemCalendar : UserControl
    {
        public TabItemCalendar()
        {
            InitializeComponent();
        }

        public Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> GetData()
        {
            return MainCalendar.GetData();
        }

        public void SetData(Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> data)
        {
            MainCalendar.SetData(data);
        }

        private void ButtonSave_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SaveTabItemCalendar.Instance.Save();
        }
    }
}
