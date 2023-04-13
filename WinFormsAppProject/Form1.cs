using System.Data;

namespace WinFormsAppProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Settings.Init(ref dateTimePicker1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, Control> settigsElements = GetSettingElements();
            foreach (var element in settigsElements)
            {
                long StampStart = Settings.ConvertToTimestamp(dateTimePicker1.Text);
                Settings.Save(element.Key, StampStart);
            }
        }

        private Dictionary<string, Control> GetSettingElements()
        {
            return new Dictionary<string, Control> {
                { "SemestrBeginning", dateTimePicker1 }
            };
        }
    }
}