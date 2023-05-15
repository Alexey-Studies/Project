using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfAppProject.ControlElements.SectionWorkload
{
    /// <summary>
    /// Логика взаимодействия для DataGridWorkload.xaml
    /// </summary>
    public partial class DataGridWorkload : UserControl
    {
        public DataGridWorkload()
        {
            InitializeComponent();
            DataGridLoad.ItemsSource = GetPreparedData(GetData());
        }
        private static List<Row> GetData()
        {
            var rows = new List<Row>
            {
                new Row {Date = new DateTime(2023, 2, 21).ToString("dd.MM.yyyy"), Group = "КИ-21", Lecture = 8, Labs = 32, Exam = 100, Total = 0 },
                new Row {Date = new DateTime(2023, 3, 27).ToString("dd.MM.yyyy"), Lecture = 20, Labs = 44 },
                new Row {Date = new DateTime(2023, 4, 29).ToString("dd.MM.yyyy"), Lecture = 16, Labs = 40 },
                new Row {Date = new DateTime(2023, 5, 3).ToString("dd.MM.yyyy"), Lecture = 16, Labs = 46 },
                new Row {Date = new DateTime(2023, 6, 6).ToString("dd.MM.yyyy"), Lecture = 4, Labs = 4 },
                new Row {Date = new DateTime(2023, 6, 10).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 3 },
                new Row {Date = new DateTime(2023, 6, 14).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 2 },
                new Row {Date = new DateTime(2023, 10, 23).ToString("dd.MM.yyyy"), Lecture = 1, Labs = 1 },
                new Row {Date = new DateTime(2023, 10, 28).ToString("dd.MM.yyyy"), Lecture = 6, Labs = 4 },
                new Row {Date = new DateTime(2023, 11, 7).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 3 },
                new Row {Date = new DateTime(2023, 11, 15).ToString("dd.MM.yyyy"), Lecture = 4, Labs = 3 },
                new Row {Date = new DateTime(2023, 12, 7).ToString("dd.MM.yyyy"), Lecture = 7, Labs = 5 },
                new Row {Date = new DateTime(2023, 12, 15).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 1 },
                new Row {Date = new DateTime(2023, 12, 16).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 1 },
                new Row {Date = new DateTime(2023, 01, 7).ToString("dd.MM.yyyy"), Lecture = 7, Labs = 5 },
                new Row {Date = new DateTime(2023, 01, 15).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 1 },
                new Row {Date = new DateTime(2023, 01, 16).ToString("dd.MM.yyyy"), Lecture = 3, Labs = 1 },
            };

            return rows;
        }

        private static List<Row> GetPreparedData(List<Row> data)
        {
            List<Row> totalRows = new List<Row>()
            {
                new Row(),
                new Row {Date = "ЗА СЕМЕСТР"},
            };
            int month = DateTime.ParseExact(data[0].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).Month;
            var totalRow = new Row
            {
                Date = "Итого",
                Group = DateTime.ParseExact(data[0].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM")
            };
            PropertyInfo[] properties = typeof(Row).GetProperties();
            for (int i = 0; i < data.Count; i++)
            {
                int curMonth = DateTime.ParseExact(data[i].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).Month;

                if (month == curMonth)
                {
                    int rowTotal = 0;
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(int))
                        {  // проверяем, что тип свойства равен int
                            rowTotal += (int)property.GetValue(data[i]); // получаем текущее значение свойства у объекта row
                        }
                    }
                    properties[properties.Length - 1].SetValue(data[i], rowTotal);
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(int))
                        {  // проверяем, что тип свойства равен int
                            int currentValue = (int)property.GetValue(data[i]);  // получаем текущее значение свойства у объекта row
                            int totalValue = (int)property.GetValue(totalRow);  // получаем текущее значение свойства у объекта totalRow
                            property.SetValue(totalRow, currentValue + totalValue);  // записываем сумму значений в свойство объекта totalRow
                        }
                    }
                    if (i == data.Count - 1)
                    {
                        data.Insert(i + 1, totalRow);
                        Row total = new Row()
                        {
                            Date = "Итого",
                        };
                        totalRows.Add(totalRow);
                        foreach (var row in totalRows)
                        {
                            data.Add(row);

                            foreach (var property in properties)
                            {
                                if (property.PropertyType == typeof(int))
                                {  // проверяем, что тип свойства равен int
                                    int currentValue = (int)property.GetValue(row);  // получаем текущее значение свойства у объекта row
                                    int totalValue = (int)property.GetValue(total);  // получаем текущее значение свойства у объекта totalRow
                                    property.SetValue(total, currentValue + totalValue);  // записываем сумму значений в свойство объекта totalRow
                                }
                            }
                        }
                        data.Add(total);
                        break;
                    }
                }
                else
                {
                    month = curMonth;
                    data.Insert(i, totalRow);
                    totalRows.Add(totalRow);
                    totalRow = new Row
                    {
                        Date = "Итого",
                        Group = DateTime.ParseExact(data[i + 1].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM")
                    };
                }
            }
            return data;
        }

        public class Row
        {
            public string Date { get; set; }
            public string Group { get; set; }
            public int Lecture { get; set; }
            public int Labs { get; set; }
            public int Exam { get; set; }
            public int Total { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DataGridLoad.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = DataGridLoad.Columns[j].Header;
            }
            for (int i = 0; i < DataGridLoad.Columns.Count; i++)
            {
                for (int j = 0; j < DataGridLoad.Items.Count; j++)
                {
                    TextBlock b = DataGridLoad.Columns[i].GetCellContent(DataGridLoad.Items[j]) as TextBlock;
                    Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
}
