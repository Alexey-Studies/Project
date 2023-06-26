using System;
using System.Collections.Generic;
using System.Xml;
using WpfAppProject.Interfaces;

namespace WpfAppProject.Clasess
{
    internal class SaveTabItemCalendar : ISave
    {
        public static SaveTabItemCalendar Instance { get; set; }

        public string FileName { get; } = "Data.xml";
        private MainWindow MainWindow { get; set; }

        public static SaveTabItemCalendar InstanceOf(MainWindow mainWindow)
        {
            if (Instance == null)
            {
                Instance = new SaveTabItemCalendar() { MainWindow = mainWindow };
            }

            return Instance;
        }

        public Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> GetData()
        {
            return Instance.MainWindow.MyTabItemCalendar.GetData();
        }

        public XmlDocument PrepareData()
        {
            var data = GetData();


            // Создаем новый экземпляр XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // Создаем корневой элемент
            XmlElement rootElement = xmlDocument.CreateElement("Data");

            // Проходим по словарю с данными и создаем элементы для каждой даты
            foreach (var monthData in data)
            {
                // Создаем элемент для даты
                XmlElement dateElement = xmlDocument.CreateElement("Month");
                dateElement.SetAttribute("value", monthData.Key.ToString("yyyy-MM"));

                // Проходим по словарю с данными для каждой даты и создаем элементы для каждой даты
                foreach (var dayData in monthData.Value)
                {
                    // Создаем элемент для месяца
                    XmlElement monthElement = xmlDocument.CreateElement("Day");
                    monthElement.SetAttribute("value", dayData.Key.ToString("yyyy-MM-dd"));

                    // Создаем элемент для дня
                    XmlElement properties = xmlDocument.CreateElement("Properties");

                    // Проходим по словарю с данными для каждого месяца и создаем элементы для каждой даты
                    foreach (var propertyData in dayData.Value)
                    {

                        // Создаем элемент для значения
                        XmlElement property = xmlDocument.CreateElement(propertyData.Key);
                        property.SetAttribute("value", propertyData.Value.ToString());

                        // Добавляем элементы для дня к элементу для месяца
                        properties.AppendChild(property);
                    }

                    monthElement.AppendChild(properties);

                    // Добавляем элементы для месяца к элементу для даты
                    dateElement.AppendChild(monthElement);
                }

                // Добавляем элемент для даты к корневому элементу
                rootElement.AppendChild(dateElement);
            }

            // Добавляем корневой элемент к документу
            xmlDocument.AppendChild(rootElement);

            // Возвращаем экземпляр XmlDocument
            return xmlDocument;
        }

        public void Save()
        {
            if (Instance != null)
            {
                var data = Instance.PrepareData();
                data.Save(FileName);
            }
        }
    }
}
