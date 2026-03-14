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
            this.Height = 300;
            var nameLabel = new Label
            {
                Text = "Название:",
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            var nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 30),
                Size = new System.Drawing.Size(200, 20)
            };
            var descriptionLabel = new Label
            {
                Text = "Описание:",
                Location = new System.Drawing.Point(10, 50),
                AutoSize = true
            };
            var descriptionTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 70),
                Size = new System.Drawing.Size(200, 20),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            var startTimeLabel = new Label
            {
                Text = "Дата начала:",
                Location = new System.Drawing.Point(10, 100),
                AutoSize = true
            };
            var startTimePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(10, 120),
                Size = new System.Drawing.Size(200, 20)
            };
            var endTimeLabel = new Label
            {
                Text = "Дата окончания:",
                Location = new System.Drawing.Point(10, 140),
                AutoSize = true
            };
            var endTimePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(10, 160),
                Size = new System.Drawing.Size(200, 20)
            };
            var okButton = new Button
            {
                Text = "ОК",
                Location = new System.Drawing.Point(10, 190),
                Size = new System.Drawing.Size(75, 23)
            };
            var cancelButton = new Button
            {
                Text = "Отмена",
                Location = new System.Drawing.Point(85, 190),
                Size = new System.Drawing.Size(75, 23)
            };
            okButton.Click += (sender, e) =>
            {
                Name = nameTextBox.Text;
                Description = descriptionTextBox.Text;
                StartTime = startTimePicker.Value;
                EndTime = endTimePicker.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            cancelButton.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.AddRange(new Control[]
            {
nameLabel, nameTextBox,
descriptionLabel, descriptionTextBox,
startTimeLabel, startTimePicker,
endTimeLabel, endTimePicker,
okButton, cancelButton
            });

        #endregion
        }
    }
}