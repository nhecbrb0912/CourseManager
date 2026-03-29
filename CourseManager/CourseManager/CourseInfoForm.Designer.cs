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
            this.Width = 500;
            this.Height = 450;
            this.StartPosition = FormStartPosition.CenterScreen;

            var nameLabel = new Label { Text = "Название: ", Location = new System.Drawing.Point(10, 10), AutoSize = true, Name = "nameLabel" };
            var descriptionLabel = new Label { Text = "Описание: ", Location = new System.Drawing.Point(10, 30), AutoSize = true, MaximumSize = new System.Drawing.Size(460, 0), Name = "descriptionLabel" };
            var startTimeLabel = new Label { Text = "Дата начала: ", Location = new System.Drawing.Point(10, 50), AutoSize = true, Name = "startTimeLabel" };
            var endTimeLabel = new Label { Text = "Дата окончания: ", Location = new System.Drawing.Point(10, 70), AutoSize = true, Name = "endTimeLabel" };
            var modulesCountLabel = new Label { Text = "Модулей: 0", Location = new System.Drawing.Point(10, 90), AutoSize = true, Name = "modulesCountLabel" };

            modulesListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 110),
                Size = new System.Drawing.Size(460, 150),
                Name = "modulesListBox"
            };

            var viewModuleInfoBtn = new Button { Text = "Просмотр информации о модуле", Location = new System.Drawing.Point(10, 270), Size = new System.Drawing.Size(230, 30), Name = "viewModuleInfoBtn" };
            viewModuleInfoBtn.Click += ViewModuleInfoBtn_Click;

            var closeButton = new Button { Text = "Закрыть", Location = new System.Drawing.Point(250, 270), Size = new System.Drawing.Size(100, 30) };
            closeButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                nameLabel, descriptionLabel, startTimeLabel, endTimeLabel,
                modulesCountLabel, modulesListBox, viewModuleInfoBtn, closeButton
            });
        }

        #endregion
    }
}

