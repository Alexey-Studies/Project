namespace WinFormsAppProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Size = new Size(1280, 720);
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
        }

        private Dictionary<string, Control> GetSettingElements()
        {
            return new Dictionary<string, Control> {
                { "SemestrBeginning", dateTimePicker1 }
            };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dictionary<string, Control> settigsElements = GetSettingElements();

            foreach (var element in settigsElements)
            {
                long StampStart = Settings.ConvertToTimestamp(dateTimePicker1.Text);
                Settings.Save(element.Key, StampStart);
            }
        }

        /// <summary>
        /// Обрабатывает вставку данных из Excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Modifiers == Keys.Control && e.KeyCode == Keys.V))
                return;

            char[] rowSplitter = { '\r', '\n' };
            char[] colSplitter = { '\t' };

            int selectedRow = dataGridViewPlan.SelectedCells[0].RowIndex;
            int selectedCol = dataGridViewPlan.SelectedCells[0].ColumnIndex;

            string text = (Clipboard.GetDataObject()?.GetData(DataFormats.Text))?.ToString() ?? "";

            // Разделить текст на строки.
            string[] rows = text.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

            // Добавить недостающие строки.
            if (dataGridViewPlan.Rows.Count < (selectedRow + rows.Length))
                dataGridViewPlan.Rows.Add(selectedRow + rows.Length - dataGridViewPlan.Rows.Count);

            for (int i = 0; i < rows.Length; i++)
            {
                string[] rowCells = rows[i].Split(colSplitter);

                for (int j = 0; j < rowCells.Length && selectedCol + j < dataGridViewPlan.ColumnCount; j++)
                    dataGridViewPlan.Rows[selectedRow + i].Cells[selectedCol + j].Value = rowCells[j];
            }
        }

        private void dataGridViewPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex > 2 && dataGridViewPlan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string data = (string)dataGridViewPlan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                dataGridViewPlan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = new string(data.Where(char.IsDigit).ToArray());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Settings.Init(ref dateTimePicker1);
        }
    }
}