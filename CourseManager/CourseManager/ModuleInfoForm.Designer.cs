using System.Windows.Forms;

namespace CourseManager
{
    partial class ModuleInfoForm
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
            this.Text = "Информация о модуле";
            this.Width = 400;
            this.Height = 300;
            var nameLabel = new Label
            {
                Text = $"Название: {module.Name}",
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            var topicsLabel = new Label
            {
                Text = $"Количество тем: {module.Topics.Count}",
                Location = new System.Drawing.Point(10, 30),
                AutoSize = true
            };
            var progressLabel = new Label
            {
                Text = $"Прогресс: {module.Progress}%",
                Location = new System.Drawing.Point(10, 50),
                AutoSize = true
            };
            this.Controls.AddRange(new Control[] { nameLabel, topicsLabel, progressLabel });
        }

        #endregion
    }
}