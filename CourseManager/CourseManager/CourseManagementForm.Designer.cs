using System.Windows.Forms;

namespace CourseManager
{
    partial class CourseManagementForm
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
            this.Text = "Управление курсами";
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;

            listView = new ListView
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(760, 450),
                View = View.Details,
                FullRowSelect = true
            };
            listView.Columns.Add("Название", 200);
            listView.Columns.Add("Описание", 550);

            var createCourseBtn = new Button { Text = "Создать курс", Location = new System.Drawing.Point(10, 470), Size = new System.Drawing.Size(120, 30) };
            var addModuleBtn = new Button { Text = "Добавить модуль", Location = new System.Drawing.Point(140, 470), Size = new System.Drawing.Size(120, 30) };
            var removeModuleBtn = new Button { Text = "Удалить модуль", Location = new System.Drawing.Point(270, 470), Size = new System.Drawing.Size(120, 30) };
            var viewInfoBtn = new Button { Text = "Просмотр информации", Location = new System.Drawing.Point(400, 470), Size = new System.Drawing.Size(180, 30) };

            createCourseBtn.Click += CreateCourseBtn_Click;
            addModuleBtn.Click += AddModuleBtn_Click;
            removeModuleBtn.Click += RemoveModuleBtn_Click;
            viewInfoBtn.Click += ViewInfoBtn_Click;

            this.Controls.AddRange(new Control[] { listView, createCourseBtn, addModuleBtn, removeModuleBtn, viewInfoBtn });
        }

        #endregion
    }
}