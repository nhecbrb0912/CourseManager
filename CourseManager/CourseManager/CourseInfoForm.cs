using System;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class CourseInfoForm : Form
    {
        public Course Course { get; set; }
        private ListBox modulesListBox;

        public CourseInfoForm()
        {
            InitializeComponent();
        }



        public void SetCourse(Course course)
        {
            if (course == null)
                return;

            Course = course;

            this.Text = $"Информация о курсе: {course.Name}";

            foreach (Control control in this.Controls)
            {
                if (control is Label label)
                {
                    if (control.Name == "nameLabel")
                        label.Text = $"Название: {course.Name}";
                    else if (control.Name == "descriptionLabel")
                        label.Text = $"Описание: {course.Description}";
                    else if (control.Name == "startTimeLabel")
                        label.Text = $"Дата начала: {course.StartTime.ToShortDateString()}";
                    else if (control.Name == "endTimeLabel")
                        label.Text = $"Дата окончания: {course.EndTime.ToShortDateString()}";
                    else if (control.Name == "modulesCountLabel")
                        label.Text = $"Модулей: {course.Modules?.Count ?? 0}";
                }
            }

            modulesListBox.Items.Clear();
            if (course.Modules != null)
            {
                foreach (var module in course.Modules)
                {
                    modulesListBox.Items.Add(module);
                }
            }
        }

        private void ViewModuleInfoBtn_Click(object sender, EventArgs e)
        {
            if (modulesListBox.SelectedItem != null)
            {
                var module = (Module)modulesListBox.SelectedItem;
                var moduleInfoForm = new ModuleInfoForm();
                moduleInfoForm.Module = module;
                moduleInfoForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите модуль для просмотра.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}