using System;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class ModuleInfoForm : Form
    {
        private Module module;
        private ListBox topicsListBox;
        private Label nameLabel;
        private Label topicsLabel;
        private Label progressLabel;
        private TextBox progressTextBox;

        public Module Module
        {
            get => module;
            set
            {
                module = value;
                UpdateModuleInfo();
            }
        }

        public ModuleInfoForm()
        {
            InitializeComponent();
        }



        private void UpdateModuleInfo()
        {
            if (module == null)
                return;

            nameLabel.Text = $"Название: {module.Name}";
            topicsLabel.Text = $"Количество тем: {module.Topics?.Count ?? 0}";
            progressLabel.Text = $"Прогресс: {module.Progress}%";
            progressTextBox.Text = module.Progress.ToString();

            topicsListBox.Items.Clear();
            if (module.Topics != null)
            {
                foreach (var topic in module.Topics)
                {
                    topicsListBox.Items.Add(topic);
                }
            }
        }

        private void UpdateProgressBtn_Click(object sender, EventArgs e)
        {
            if (module == null)
            {
                MessageBox.Show("Модуль не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(progressTextBox.Text))
            {
                MessageBox.Show(
                    "Введите значение прогресса!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }


            if (!decimal.TryParse(progressTextBox.Text, out decimal newProgress))
            {
                MessageBox.Show(
                    $"Неверный формат числа!\n\nВы ввели: {progressTextBox.Text}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }


            if (newProgress < 0 || newProgress > 100)
            {
                MessageBox.Show(
                    $"Вы вышли за пределы!\n\nДопустимый диапазон: от 0 до 100.\n",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                progressTextBox.Text = module.Progress.ToString();
                return;
            }

            module.Progress = newProgress;

            MessageBox.Show(
                $"Прогресс модуля '{module.Name}' обновлён до {module.Progress}%.",
                "Успех",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            UpdateModuleInfo();
        }


        private void AddTopicBtn_Click(object sender, EventArgs e)
        {
            if (module == null)
            {
                MessageBox.Show("Модуль не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var addTopicForm = new AddTopicForm();
            if (addTopicForm.ShowDialog() == DialogResult.OK)
            {
                module.AddTopic(new Topic(addTopicForm.TopicName));
                UpdateModuleInfo();
            }
        }

        private void RemoveTopicBtn_Click(object sender, EventArgs e)
        {
            if (module == null)
            {
                MessageBox.Show("Модуль не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (topicsListBox.SelectedItem != null)
            {
                var topic = (Topic)topicsListBox.SelectedItem;
                module.RemoveTopic(topic);
                MessageBox.Show(
                    $"Удалена тема '{topic}'.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

                UpdateModuleInfo();
            }
            else
            {
                MessageBox.Show("Выберите тему для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}