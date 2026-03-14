using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class CourseManagementForm : Form
    {
        private List<Course> courses = new List<Course>();
        private ListView listView;
        private Button createCourseButton;
        private Button addModuleButton;
        private Button removeModuleButton;
        private Button updateProgressButton;
        public CourseManagementForm()
        {
            this.Text = "Управление курсами";
            this.Width = 500;
            this.Height = 400;
            CreateControls();
        }
        private void CreateControls()
        {
            listView = new ListView
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(480, 300),
                View = View.Details,
                FullRowSelect = true
            };
            listView.Columns.Add("Название", 150);
            listView.Columns.Add("Описание", 330);
            createCourseButton = new Button
            {
                Location = new System.Drawing.Point(10, 320),
                Text = "Создать курс",
                Size = new System.Drawing.Size(100, 25)
            };
            createCourseButton.Click += (sender, e) =>
            {
                var createCourseForm = new CreateCourseForm();
                createCourseForm.ShowDialog();
                if (createCourseForm.DialogResult == DialogResult.OK)
                {
                    var course = new Course(
                    createCourseForm.Name,
                    createCourseForm.Description,
                    createCourseForm.StartTime,
                    createCourseForm.EndTime);
                    courses.Add(course);
                    UpdateCourseList();
                }
            };
            addModuleButton = new Button
            {
                Location = new System.Drawing.Point(120, 320),
                Text = "Добавить модуль",
                Size = new System.Drawing.Size(100, 25)
            };
            addModuleButton.Click += (sender, e) =>
            {
                if (listView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Сначала выберите курс.");
                    return;
                }
                var course = courses[listView.SelectedItems[0].Index];
                var addModuleForm = new AddModuleForm();
                addModuleForm.ShowDialog();
                if (addModuleForm.DialogResult == DialogResult.OK)
                {
                    course.AddModule(new Module(addModuleForm.Name));
                }
            };
            removeModuleButton = new Button
            {
                Location = new System.Drawing.Point(230, 320),
                Text = "Удалить модуль",
                Size = new System.Drawing.Size(100, 25)
            };
            removeModuleButton.Click += (sender, e) =>
            {
                if (listView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Сначала выберите курс.");
                    return;
                }
                var course = courses[listView.SelectedItems[0].Index];
                var removeModuleForm = new RemoveModuleForm(course.Modules);
                removeModuleForm.ShowDialog();
                if (removeModuleForm.DialogResult == DialogResult.OK)
                {
                    course.RemoveModule(removeModuleForm.Module);
                }
            };
            updateProgressButton = new Button
            {
                Location = new System.Drawing.Point(340, 320),
                Text = "Обновить прогресс",
                Size = new System.Drawing.Size(120, 25)
            };
            updateProgressButton.Click += (sender, e) =>
            {
                if (listView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Сначала выберите курс.");
                    return;
                }
                var course = courses[listView.SelectedItems[0].Index];
                if (course.Modules.Count == 0)
                {
                    MessageBox.Show("У курса нет модулей.");
                    return;
                }
                var updateProgressForm = new UpdateProgressForm();
                updateProgressForm.ShowDialog();
                if (updateProgressForm.DialogResult == DialogResult.OK)
                {
                    course.Modules[0].UpdateProgress(updateProgressForm.Progress);
                }
            };
            this.Controls.Add(listView);
            this.Controls.Add(createCourseButton);
            this.Controls.Add(addModuleButton);
            this.Controls.Add(removeModuleButton);
            this.Controls.Add(updateProgressButton);
        }
        private void UpdateCourseList()
        {
            listView.Items.Clear();
            foreach (var course in courses)
            {
                listView.Items.Add(new ListViewItem(new[] { course.Name, course.Description }));
            }
        }
    }
}
