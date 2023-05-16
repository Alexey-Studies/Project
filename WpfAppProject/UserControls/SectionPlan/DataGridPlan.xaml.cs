using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfAppProject.UserControls.SectionPlan
{
    /// <summary>
    /// Логика взаимодействия для DataGridPlan.xaml
    /// </summary>
    public partial class DataGridPlan : UserControl
    {
        public IEnumerable ItemsSource
        {
            get => Plan.ItemsSource;
            set => Plan.ItemsSource = value;
        }

        public DataGridPlan()
        {
            InitializeComponent();

            ObservableCollection<PlanDataGridRow> PlanRows = new ObservableCollection<PlanDataGridRow>();
            Plan.ItemsSource = PlanRows;
        }

        /// <summary>
        /// Устанавливает значение ячейки в главном DataGrid.
        /// </summary>
        /// <param name="x">Столбец</param>
        /// <param name="y">Строка</param>
        /// <param name="value">Значение ячейки</param>
        private void SetCell(int x, int y, object value)
        {
            if (x >= typeof(PlanDataGridRow).GetProperties().Length)
                return;

            // Если строка уже существует.
            if (y < Plan.Items.Count - 1)
            {
                // Получить выбранный столбец.
                DataGridColumn selectedColumn = Plan.Columns[x];

                // Получить информацию о пути привязки данных для свойства.
                string bindingPath = ((selectedColumn as DataGridBoundColumn).Binding as Binding).Path.Path;

                // Получить PropertyInfo для свойства, соответствующего столбца.
                PropertyInfo propertyInfo = typeof(PlanDataGridRow).GetProperty(bindingPath);

                // Преобразовать под необходимый тип данных.
                if (propertyInfo.PropertyType == typeof(int))
                {
                    value = int.TryParse((string)value, out int intValue)
                        ?
                        (object)intValue
                        :
                        null;
                }

                // Установить новое значение свойства.
                propertyInfo.SetValue(Plan.Items[y], value);
            }
            else
            {
                var newRow = new PlanDataGridRow();
                var properties = typeof(PlanDataGridRow).GetProperties();
                var items = Plan.ItemsSource as ObservableCollection<PlanDataGridRow>;

                if (properties[x].PropertyType == typeof(int))
                {
                    if (int.TryParse((string)value, out int intValue))
                    {
                        properties[x].SetValue(newRow, intValue);
                    }
                }
                else
                {
                    properties[x].SetValue(newRow, value);
                }

                items.Add(newRow);
            }

            // Обновить представление DataGrid.
            Plan.Items.Refresh();
        }

        /// <summary>
        /// Срабатывает при нажатии CTRL + V в Grid, содержащем главный DataGrid.
        /// Берёт из буфера обмена данные и вставляет в главный DataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control || e.Key != Key.V)
            {
                return;
            }
            else if (((IEditableCollectionView)Plan.Items).IsAddingNew)
            {
                ((IEditableCollectionView)Plan.Items).CommitNew();
                Plan.Items.Refresh();
            }
            else if (((IEditableCollectionView)Plan.Items).IsEditingItem)
            {
                ((IEditableCollectionView)Plan.Items).CommitEdit();
                Plan.Items.Refresh();
            }

            // Получить содержимое буфера обмена.
            string clipboardText = Clipboard.GetText();

            // Разбить текст на строки.
            string[] rows = clipboardText.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            int selColIndex = 0;
            int selRowIndex = 0;

            // Если выбрана ячейка, то взять её координаты.
            if (Plan.CurrentCell != null)
            {
                var cellInfo = Plan.CurrentCell;

                selColIndex = cellInfo.Column?.DisplayIndex ?? 0;
                selRowIndex = Plan.Items.IndexOf(cellInfo.Item) >= 0 ? Plan.Items.IndexOf(cellInfo.Item) : 0;
            }

            for (int y = 0; y < rows.Length; y++)
            {
                string[] cells = rows[y].Split('\t');

                for (int x = 0; x < cells.Length; x++)
                {
                    SetCell(x + selColIndex, y + selRowIndex, cells[x]);
                }
            }

            e.Handled = true;
        }
    }

    /// <summary>
    /// Класс, используемый для главного DataGrid раздела "План".
    /// </summary>
    public class PlanDataGridRow
    {
        public string Subject { get; set; }

        public string Abbriviation { get; set; }

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
    }
}
