namespace WinFormsAppProject
{
    partial class UniversityClass
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxSubject = new ComboBox();
            comboBoxStudyGroup = new ComboBox();
            comboBoxType = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // comboBoxSubject
            // 
            comboBoxSubject.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxSubject.FormattingEnabled = true;
            comboBoxSubject.Location = new Point(3, 24);
            comboBoxSubject.Margin = new Padding(0);
            comboBoxSubject.Name = "comboBoxSubject";
            comboBoxSubject.Size = new Size(121, 23);
            comboBoxSubject.TabIndex = 0;
            // 
            // comboBoxStudyGroup
            // 
            comboBoxStudyGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxStudyGroup.FormattingEnabled = true;
            comboBoxStudyGroup.Location = new Point(3, 47);
            comboBoxStudyGroup.Margin = new Padding(0);
            comboBoxStudyGroup.Name = "comboBoxStudyGroup";
            comboBoxStudyGroup.Size = new Size(121, 23);
            comboBoxStudyGroup.TabIndex = 1;
            // 
            // comboBoxType
            // 
            comboBoxType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(3, 70);
            comboBoxType.Margin = new Padding(0);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(121, 23);
            comboBoxType.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 6);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 3;
            label1.Text = "Пара";
            // 
            // UniversityClass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(comboBoxType);
            Controls.Add(comboBoxStudyGroup);
            Controls.Add(comboBoxSubject);
            Name = "UniversityClass";
            Size = new Size(127, 95);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxSubject;
        private ComboBox comboBoxStudyGroup;
        private ComboBox comboBoxType;
        private Label label1;
    }
}
