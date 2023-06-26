using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WpfAppProject.UserControls.SectionCalendar
{
    public class LoadTabItemCalendar
    {
        public static LoadTabItemCalendar Instance { get; set; }

        public string FileName { get; } = "Data.xml";
        private MainWindow MainWindow { get; set; }

        public static void Load(MainWindow mainWindow)
        {
            if (Instance == null)
            {
                Instance = new LoadTabItemCalendar() { MainWindow = mainWindow };
            }
            Instance.MainWindow.MyTabItemCalendar.SetData(Instance.GetData());
        }
        public object MyTabItemCalendar { get; private set; }

        public Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> GetData()
        {
            var result = new Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>>();

            XDocument xml = XDocument.Load(FileName);

            foreach (XElement monthElement in xml.Root.Elements("Month"))
            {
                DateTime monthDate = DateTime.Parse(monthElement.Attribute("value").Value);
                Dictionary<DateTime, Dictionary<string, bool>> monthDays = new Dictionary<DateTime, Dictionary<string, bool>>();

                foreach (XElement dayElement in monthElement.Elements("Day"))
                {
                    DateTime dayDate = DateTime.Parse(dayElement.Attribute("value").Value);
                    Dictionary<string, bool> dayProperties = new Dictionary<string, bool>();

                    foreach (XElement propertyElement in dayElement.Element("Properties").Elements())
                    {
                        string propertyName = propertyElement.Name.LocalName;
                        bool propertyValue = bool.Parse(propertyElement.Attribute("value").Value);

                        dayProperties[propertyName] = propertyValue;
                    }

                    monthDays[dayDate] = dayProperties;
                }

                result[monthDate] = monthDays;
            }

            return result;
        }
    }
}
