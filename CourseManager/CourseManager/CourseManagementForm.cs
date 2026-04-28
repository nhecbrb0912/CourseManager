using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace CourseManager
{
    public partial class CourseManagementForm : Form
    {
        private List<Course> courses = new List<Course>();
        private ListView listView;
        private const string SaveFilePath = "courses.json";

        public CourseManagementForm()
        {
            InitializeComponent();
            LoadCourses();
        }



        private void CreateCourseBtn_Click(object sender, EventArgs e)
        {
            var form = new CreateCourseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var course = new Course(form.CourseName, form.Description, form.StartTime, form.EndTime);
                courses.Add(course);
                UpdateCourseList();
                SaveCourses();
            }
        }

        private void AddModuleBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Сначала выберите курс.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = listView.SelectedItems[0].Index;
            var form = new AddModuleForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (listView.SelectedItems.Count == 0 || listView.SelectedItems[0].Index != selectedIndex)
                {
                    MessageBox.Show("Курс не выбран. Выберите курс повторно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var course = courses[listView.SelectedItems[0].Index];
                course.AddModule(new Module(form.ModuleName));
                UpdateCourseList();
                SaveCourses();
            }
        }

        private void RemoveModuleBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Сначала выберите курс.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var course = courses[listView.SelectedItems[0].Index];
            if (course.Modules.Count == 0)
            {
                MessageBox.Show("У курса нет модулей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new RemoveModuleForm(course.Modules);
            if (form.ShowDialog() == DialogResult.OK && form.Module != null)
            {
                course.RemoveModule(form.Module);
                UpdateCourseList();
                SaveCourses();
            }
        }

        // ✅ ОБРАБОТЧИК ДЛЯ КНОПКИ ОБНОВЛЕНИЯ ПРОГРЕССА
        private void UpdateProgressBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Сначала выберите курс.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var course = courses[listView.SelectedItems[0].Index];
            if (course.Modules.Count == 0)
            {
                MessageBox.Show("У курса нет модулей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "💡 Совет: Для обновления прогресса откройте информацию о курсе → выберите модуль → нажмите \"Просмотр информации о модуле\" → обновите прогресс там.",
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Открываем информацию о курсе для удобства
            var infoForm = new CourseInfoForm();
            infoForm.SetCourse(course);
            infoForm.ShowDialog();
        }

        private void ViewInfoBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Сначала выберите курс.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var course = courses[listView.SelectedItems[0].Index];
            var infoForm = new CourseInfoForm();
            infoForm.SetCourse(course);
            infoForm.ShowDialog();
        }

        private void StatisticsBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Сначала выберите курс для просмотра статистики.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var course = courses[listView.SelectedItems[0].Index];
            var statsForm = new StatisticsForm(course);
            statsForm.ShowDialog();
        }

        private void UpdateCourseList()
        {
            listView.Items.Clear();
            foreach (var course in courses)
            {
                listView.Items.Add(new ListViewItem(new[] { course.Name, course.Description }));
            }
        }

        private void SaveCourses()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(courses, options);
                File.WriteAllText(SaveFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCourses()
        {
            try
            {
                if (File.Exists(SaveFilePath))
                {
                    var json = File.ReadAllText(SaveFilePath);
                    courses = JsonSerializer.Deserialize<List<Course>>(json) ?? new List<Course>();
                    UpdateCourseList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                courses = new List<Course>();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveCourses();
            base.OnFormClosing(e);
        }
    }
}