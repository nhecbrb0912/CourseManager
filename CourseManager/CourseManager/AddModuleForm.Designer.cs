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
            this.Width = 300;
            this.Height = 150;
            var nameLabel = new Label
            {
                Text = "Имя модуля:",
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            var nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 30),
                Size = new System.Drawing.Size(200, 20)
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
                Name = nameTextBox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            cancelButton.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.AddRange(new Control[] { nameLabel, nameTextBox, okButton,
cancelButton });
        }

        #endregion
    }
}