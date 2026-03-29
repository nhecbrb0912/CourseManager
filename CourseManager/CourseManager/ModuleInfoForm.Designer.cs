using System.Windows.Forms;
using System.Text.Json;
using Microsoft.VisualBasic;


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
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// 
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Информация о модуле";
            this.Width = 500;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            nameLabel = new Label
            {
                Text = "Название: ",
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };

            topicsLabel = new Label
            {
                Text = "Количество тем: 0",
                Location = new System.Drawing.Point(10, 30),
                AutoSize = true
            };

            progressLabel = new Label
            {
                Text = "Прогресс: %",
                Location = new System.Drawing.Point(10, 50),
                AutoSize = true
            };



            progressTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 75),
                Width = 100,
                Text = "0"
            };

            topicsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 110),
                Size = new System.Drawing.Size(460, 200)
            };

            var updateProgressBtn = new Button
            {
                Text = "Обновить прогресс",
                Location = new System.Drawing.Point(120, 70),
                Size = new System.Drawing.Size(130, 30)
            };
            updateProgressBtn.Click += UpdateProgressBtn_Click;

            var addTopicBtn = new Button
            {
                Text = "Добавить тему",
                Location = new System.Drawing.Point(10, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            addTopicBtn.Click += AddTopicBtn_Click;

            var removeTopicBtn = new Button
            {
                Text = "Удалить тему",
                Location = new System.Drawing.Point(140, 320),
                Size = new System.Drawing.Size(120, 30)
            };
            removeTopicBtn.Click += RemoveTopicBtn_Click;

            var closeButton = new Button
            {
                Text = "Закрыть",
                Location = new System.Drawing.Point(270, 320),
                Size = new System.Drawing.Size(100, 30)
            };
            closeButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                nameLabel, topicsLabel, progressLabel, progressTextBox,
                topicsListBox, updateProgressBtn,
                addTopicBtn, removeTopicBtn, closeButton
            });
        }

        #endregion
    }
}