using System.Windows.Forms;

namespace CourseManager
{
    partial class UpdateProgressForm
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
            this.Text = "Обновить прогресс";
            this.Width = 300;
            this.Height = 150;
            var progressLabel = new Label
            {
                Text = "Прогресс (%):",
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            var progressNumericUpDown = new NumericUpDown
            {
                Location = new System.Drawing.Point(10, 30),
                Size = new System.Drawing.Size(100, 20),
                Minimum = 0,
                Maximum = 100
            };
            var okButton = new Button
            {
                Text = "ОК",
                Location = new System.Drawing.Point(10, 60),
                Size = new System.Drawing.Size(75, 23)
            };
            var cancelButton = new Button
            {
                Text = "Отмена",
                Location = new System.Drawing.Point(85, 60),
                Size = new System.Drawing.Size(75, 23)
            };
            okButton.Click += (sender, e) =>
            {
                Progress = progressNumericUpDown.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            cancelButton.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.AddRange(new Control[] { progressLabel, progressNumericUpDown,
okButton, cancelButton });
        }

        #endregion
    }
}