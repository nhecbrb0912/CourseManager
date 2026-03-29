using System.Windows.Forms;

namespace CourseManager
{
    partial class CreateCourseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Создать курс";
            this.Width = 400;
            this.Height = 350;
            this.StartPosition = FormStartPosition.CenterScreen;

            var nameLabel = new Label { Text = "Название:", Location = new System.Drawing.Point(10, 10), AutoSize = true };
            var nameTextBox = new TextBox { Location = new System.Drawing.Point(10, 30), Width = 350 };

            var descLabel = new Label { Text = "Описание:", Location = new System.Drawing.Point(10, 60), AutoSize = true };
            var descTextBox = new TextBox { Location = new System.Drawing.Point(10, 80), Width = 350, Height = 60, Multiline = true };

            var startLabel = new Label { Text = "Дата начала:", Location = new System.Drawing.Point(10, 150), AutoSize = true };
            var startPicker = new DateTimePicker { Location = new System.Drawing.Point(10, 170), Width = 170 };

            var endLabel = new Label { Text = "Дата окончания:", Location = new System.Drawing.Point(190, 150), AutoSize = true };
            var endPicker = new DateTimePicker { Location = new System.Drawing.Point(190, 170), Width = 170 };

            var okButton = new Button { Text = "ОК", Location = new System.Drawing.Point(10, 210), Size = new System.Drawing.Size(100, 30) };
            var cancelButton = new Button { Text = "Отмена", Location = new System.Drawing.Point(120, 210), Size = new System.Drawing.Size(100, 30) };

            okButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Введите название курса!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (startPicker.Value > endPicker.Value)
                {
                    MessageBox.Show("Введены некорректные даты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CourseName = nameTextBox.Text;
                Description = descTextBox.Text;
                StartTime = startPicker.Value;
                EndTime = endPicker.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            cancelButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { nameLabel, nameTextBox, descLabel, descTextBox, startLabel, startPicker, endLabel, endPicker, okButton, cancelButton });
        }

        #endregion
    }
}
