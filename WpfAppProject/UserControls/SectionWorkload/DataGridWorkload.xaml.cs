using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using WpfAppProject.Interfaces;

namespace WpfAppProject.UserControls.SectionWorkload
{
    /// <summary>
    /// Логика взаимодействия для DataGridWorkload.xaml
    /// </summary>
    public partial class DataGridWorkload : UserControl
    {
        public DataGridWorkload()
        {
            InitializeComponent();
            MainDataGrid.ItemsSource = GetPreparedData(
                GetData()
                );
        }
        private static List<DataGridWorkloadRow> GetData()
        {
            var rows = new List<DataGridWorkloadRow>
            {
                new DataGridWorkloadRow {Date = new DateTime(2023, 2, 21).ToString("dd.MM.yyyy"), Group = "КИ-21", Lectures = 8, Total = 0},
                new DataGridWorkloadRow {Date = new DateTime(2023, 3, 27).ToString("dd.MM.yyyy"), Lectures = 20},
                new DataGridWorkloadRow {Date = new DateTime(2023, 4, 29).ToString("dd.MM.yyyy"), Lectures = 16},
                new DataGridWorkloadRow {Date = new DateTime(2023, 5, 3).ToString("dd.MM.yyyy"), Lectures = 16},
                new DataGridWorkloadRow {Date = new DateTime(2023, 6, 6).ToString("dd.MM.yyyy"), Lectures = 4},
                new DataGridWorkloadRow {Date = new DateTime(2023, 6, 10).ToString("dd.MM.yyyy"), Lectures = 3},
                new DataGridWorkloadRow {Date = new DateTime(2023, 6, 14).ToString("dd.MM.yyyy"), Lectures = 3},
                new DataGridWorkloadRow {Date = new DateTime(2023, 10, 23).ToString("dd.MM.yyyy"), Lectures = 1},
                new DataGridWorkloadRow {Date = new DateTime(2023, 10, 28).ToString("dd.MM.yyyy"), Lectures = 6},
                new DataGridWorkloadRow {Date = new DateTime(2023, 11, 7).ToString("dd.MM.yyyy"), Lectures = 3},
                new DataGridWorkloadRow {Date = new DateTime(2023, 11, 15).ToString("dd.MM.yyyy"), Lectures = 4},
                new DataGridWorkloadRow {Date = new DateTime(2023, 12, 7).ToString("dd.MM.yyyy"), Lectures = 7},
                new DataGridWorkloadRow {Date = new DateTime(2023, 12, 15).ToString("dd.MM.yyyy"), Lectures = 3},
                new DataGridWorkloadRow {Date = new DateTime(2023, 12, 16).ToString("dd.MM.yyyy"), Lectures = 3},
                new DataGridWorkloadRow {Date = new DateTime(2023, 01, 7).ToString("dd.MM.yyyy"), Lectures = 7},
                new DataGridWorkloadRow {Date = new DateTime(2023, 01, 15).ToString("dd.MM.yyyy"), Lectures = 3},
                new DataGridWorkloadRow {Date = new DateTime(2023, 01, 16).ToString("dd.MM.yyyy"), Lectures = 3},
            };

            return rows;
        }

        private static List<DataGridWorkloadRow> GetPreparedData(List<DataGridWorkloadRow> data)
        {
            List<DataGridWorkloadRow> totalRows = new List<DataGridWorkloadRow>()
            {
                new DataGridWorkloadRow(),
                new DataGridWorkloadRow {Date = "ЗА СЕМЕСТР"},
            };
            int month = DateTime.ParseExact(data[0].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).Month;
            var totalRow = new DataGridWorkloadRow
            {
                Date = "Итого",
                Group = DateTime.ParseExact(data[0].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM")
            };
            PropertyInfo[] properties = typeof(DataGridWorkloadRow).GetProperties();
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
                        DataGridWorkloadRow total = new DataGridWorkloadRow()
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
                    totalRow = new DataGridWorkloadRow
                    {
                        Date = "Итого",
                        Group = DateTime.ParseExact(data[i + 1].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MMMM")
                    };
                }
            }
            return data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < MainDataGrid.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = MainDataGrid.Columns[j].Header;
            }

            for (int i = 0; i < MainDataGrid.Columns.Count; i++)
            {
                for (int j = 0; j < MainDataGrid.Items.Count; j++)
                {
                    TextBlock b = MainDataGrid.Columns[i].GetCellContent(MainDataGrid.Items[j]) as TextBlock;
                    Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b?.Text;
                }
            }
        }
    }

    public class DataGridWorkloadRow : IDataGridPlanWorkloadRow
    {
        public string Date { get; set; }

        public string Group { get; set; }

        public int Lectures { get; set; }

        public int PracticalLessons { get; set; }

        public int LaboratoryStudies { get; set; }

        public int Nirs { get; set; }

        public int PartTimeStudents { get; set; }

        public int CourseConsultations { get; set; }

        public int ExaminationConsultations { get; set; }

        public int ControlAuditWork { get; set; }

        public int IndependentWork { get; set; }

        public int AbstractsTranslations { get; set; }

        public int CalculatedGraphWorks { get; set; }

        public int CourseWorks { get; set; }

        public int SemesterExams { get; set; }

        public int PracticeGuide { get; set; }

        public int StateExams { get; set; }

        public int Vkr { get; set; }

        public int Guidance { get; set; }

        public int OtherTypes { get; set; }

        public int Total { get; set; }
    }
}
