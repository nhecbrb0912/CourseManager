using System;
using System.Drawing;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class StatisticsForm : Form
    {
        private Course course;
        private ProgressBar overallProgressBar;
        private Label overallProgressLabel;
        private ListView modulesStatsListView;
        private Panel chartPanel;

        public StatisticsForm(Course course)
        {
            this.course = course;
            InitializeComponent();
            LoadStatistics();
        }


        private void LoadStatistics()
        {
            if (course == null)
                return;

            // 1. Общий прогресс
            decimal overallProgress = course.CalculateOverallProgress();
            overallProgressBar.Value = (int)overallProgress;
            overallProgressLabel.Text = $"{overallProgress:F1}%";

            // Цвет прогресс-бара в зависимости от прогресса
            if (overallProgress >= 100)
                overallProgressBar.ForeColor = Color.Green;
            else if (overallProgress >= 50)
                overallProgressBar.ForeColor = Color.Orange;
            else
                overallProgressBar.ForeColor = Color.Blue;

            // 2. Сводная статистика
            var completedModulesLabel = (Label)this.Controls.Find("completedModulesLabel", true)[0];
            var totalTopicsLabel = (Label)this.Controls.Find("totalTopicsLabel", true)[0];
            var completionDateLabel = (Label)this.Controls.Find("completionDateLabel", true)[0];

            completedModulesLabel.Text = $"Завершено модулей: {course.GetCompletedModulesCount()} из {course.Modules?.Count ?? 0}";
            totalTopicsLabel.Text = $"Всего тем: {course.GetTotalTopicsCount()}";

            if (course.IsCourseCompleted())
            {
                completionDateLabel.Text = $"Курс завершён!";
                completionDateLabel.ForeColor = Color.Green;
                completionDateLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                completionDateLabel.Text = $"Курс в процессе";
                completionDateLabel.ForeColor = Color.Gray;
            }

            // 3. Статистика по модулям
            PopulateModulesStatistics();

            // 4. График прогресса
            DrawProgressChart();

            // 5. Уведомления о завершении
            CheckCompletedModules();
        }

        private void PopulateModulesStatistics()
        {
            modulesStatsListView.Items.Clear();

            if (course.Modules != null)
            {
                foreach (var module in course.Modules)
                {
                    var item = new ListViewItem(module.Name);
                    item.SubItems.Add($"{module.Progress:F1}%");
                    item.SubItems.Add(module.Topics?.Count.ToString() ?? "0");
                    item.SubItems.Add(module.IsCompleted() ? "Завершён" : "В процессе");

                    // Цветовая индикация
                    if (module.IsCompleted())
                    {
                        item.BackColor = Color.LightGreen;
                        item.ForeColor = Color.DarkGreen;
                    }
                    else if (module.Progress > 0)
                    {
                        item.BackColor = Color.LightYellow;
                    }

                    modulesStatsListView.Items.Add(item);
                }
            }
        }

        private void DrawProgressChart()
        {
            chartPanel.Paint += (s, e) =>
            {
                if (course.Modules == null || course.Modules.Count == 0)
                {
                    e.Graphics.DrawString("Нет модулей для отображения",
                        new Font("Segoe UI", 9), Brushes.Gray, 10, 30);
                    return;
                }

                var graphics = e.Graphics;
                int barWidth = (chartPanel.Width - 20) / course.Modules.Count - 3;
                int maxHeight = chartPanel.Height - 25;
                int startY = chartPanel.Height - 20;

                // Рисуем ось
                graphics.DrawLine(Pens.Gray, 10, startY, chartPanel.Width - 10, startY);

                for (int i = 0; i < course.Modules.Count; i++)
                {
                    var module = course.Modules[i];
                    int barHeight = (int)(maxHeight * module.Progress / 100);
                    int x = 10 + i * (barWidth + 3);
                    int y = startY - barHeight;

                    // Цвет бара
                    Color barColor = module.IsCompleted() ? Color.Green :
                                    module.Progress >= 50 ? Color.Orange : Color.Blue;

                    // Рисуем бар
                    using (var brush = new SolidBrush(barColor))
                    {
                        graphics.FillRectangle(brush, x, y, barWidth, barHeight);
                    }

                    // Рисуем границу
                    graphics.DrawRectangle(Pens.Black, x, y, barWidth, barHeight);

                    // Рисуем название модуля (сокращённое)
                    string displayName = module.Name;
                    //if (displayName.Length > 12)
                    //    displayName = displayName.Substring(0, 10) + "..";

                    graphics.DrawString(displayName,
                        new Font("Segoe UI", 7),
                        Brushes.Black,
                        x, startY + 3);

                    // Рисуем процент над баром
                    graphics.DrawString($"{module.Progress:F0}%",
                        new Font("Segoe UI", 7, FontStyle.Bold),
                        Brushes.DarkRed,
                        x + barWidth / 2 - 10, y - 15);
                }
            };
            chartPanel.Invalidate();
        }

        private void CheckCompletedModules()
        {
            if (course.Modules == null)
                return;

            foreach (var module in course.Modules)
            {
                if (module.IsCompleted())
                {
                    // Уведомление о завершении модуля
                    MessageBox.Show(
                        $"Поздравляем!\n\nМодуль «{module.Name}» завершён!\nПрогресс: {module.Progress}%",
                        "Модуль завершён",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            // Уведомление о завершении всего курса
            if (course.IsCourseCompleted() && course.Modules.Count > 0)
            {
                MessageBox.Show(
                    $"ПОЗДРАВЛЯЕМ!\n\nКурс «{course.Name}» полностью завершён!\nОбщий прогресс: 100%",
                    "Курс завершён",
                    MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
        }
    }
}