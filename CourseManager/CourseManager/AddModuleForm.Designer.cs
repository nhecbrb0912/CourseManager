using System.Windows.Forms;

namespace CourseManager
{
    partial class AddModuleForm
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
            this.Text = "Добавить модуль";
            this.Width = 350;
            this.Height = 180;
            this.StartPosition = FormStartPosition.CenterScreen;

            var nameLabel = new Label
            {
                Text = "Название модуля:",
                Location = new System.Drawing.Point(10, 20),
                AutoSize = true
            };

            var nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 45),
                Width = 300
            };

            var okButton = new Button
            {
                Text = "ОК",
                Location = new System.Drawing.Point(10, 90),
                Size = new System.Drawing.Size(100, 30)
            };

            var cancelButton = new Button
            {
                Text = "Отмена",
                Location = new System.Drawing.Point(120, 90),
                Size = new System.Drawing.Size(100, 30)
            };

            okButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Введите название модуля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ModuleName = nameTextBox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            cancelButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { nameLabel, nameTextBox, okButton, cancelButton });
        }

        #endregion
    }
}