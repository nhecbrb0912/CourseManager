using System.Windows.Forms;

namespace CourseManager
{
    partial class CourseInfoForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Информация о курсе";
            this.Width = 400;
            this.Height = 300;
            var nameLabel = new Label
            {
                Text = $"Название: {course.Name}",
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            var descriptionLabel = new Label
            {
                Text = $"Описание: {course.Description}",
                Location = new System.Drawing.Point(10, 30),
                AutoSize = true,
                MaximumSize = new System.Drawing.Size(380, 0)
            };
            var startTimeLabel = new Label
            {
                Text = $"Дата начала: {course.StartTime.ToShortDateString()}",
                Location = new System.Drawing.Point(10, 50),
                AutoSize = true
            };
            var endTimeLabel = new Label
            {
                Text = $"Дата окончания: {course.EndTime.ToShortDateString()}",
                Location = new System.Drawing.Point(10, 70),
                AutoSize = true
            };
            this.Controls.AddRange(new Control[] { nameLabel, descriptionLabel, startTimeLabel,
endTimeLabel });
        }

        #endregion
    }
}

