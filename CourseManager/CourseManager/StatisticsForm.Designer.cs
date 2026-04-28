using System.Drawing;
using System.Windows.Forms;

namespace CourseManager
{
    partial class StatisticsForm
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
            this.Text = $"Статистика: {course?.Name}";
            this.Width = 600;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // === ОБЩИЙ ПРОГРЕСС ===
            var overallLabel = new Label
            {
                Text = "Общий прогресс по курсу:",
                Location = new Point(15, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            overallProgressBar = new ProgressBar
            {
                Location = new Point(15, 45),
                Width = 400,
                Height = 30,
                Minimum = 0,
                Maximum = 100
            };

            overallProgressLabel = new Label
            {
                Location = new Point(425, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            // === СВОДНАЯ СТАТИСТИКА ===
            var statsLabel = new Label
            {
                Text = "Статистика:",
                Location = new Point(15, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var completedModulesLabel = new Label
            {
                Name = "completedModulesLabel",
                Location = new Point(25, 110),
                AutoSize = true
            };

            var totalTopicsLabel = new Label
            {
                Name = "totalTopicsLabel",
                Location = new Point(25, 130),
                AutoSize = true
            };

            var completionDateLabel = new Label
            {
                Name = "completionDateLabel",
                Location = new Point(25, 150),
                AutoSize = true
            };

            // === СТАТИСТИКА ПО МОДУЛЯМ ===
            var modulesStatsLabel = new Label
            {
                Text = "Статистика по модулям:",
                Location = new Point(15, 185),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            modulesStatsListView = new ListView
            {
                Location = new Point(15, 210),
                Size = new Size(555, 150),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            modulesStatsListView.Columns.Add("Модуль", 180);
            modulesStatsListView.Columns.Add("Прогресс", 100);
            modulesStatsListView.Columns.Add("Тем", 70);
            modulesStatsListView.Columns.Add("Статус", 150);

            // === ГРАФИК ПРОГРЕССА ===
            var chartLabel = new Label
            {
                Text = "Визуализация прогресса:",
                Location = new Point(15, 370),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            chartPanel = new Panel
            {
                Location = new Point(15, 395),
                Size = new Size(555, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            // === КНОПКИ ===
            //var closeButton = new Button
            //{
            //    Text = "Закрыть",
            //    Location = new Point(240, 600),
            //    Size = new Size(100, 35),
            //    Font = new Font("Segoe UI", 9, FontStyle.Bold)
            //};
            //closeButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                overallLabel, overallProgressBar, overallProgressLabel,
                statsLabel, completedModulesLabel, totalTopicsLabel, completionDateLabel,
                modulesStatsLabel, modulesStatsListView,
                chartLabel, chartPanel,
                //closeButton
            });
        }


        #endregion
    }
}