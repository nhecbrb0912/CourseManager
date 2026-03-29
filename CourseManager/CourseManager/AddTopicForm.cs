using System;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class AddTopicForm : Form
    {
        public string TopicName { get; private set; }

        public AddTopicForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Добавить тему";
            this.Width = 350;
            this.Height = 180;
            this.StartPosition = FormStartPosition.CenterScreen;

            var nameLabel = new Label { Text = "Название темы:", Location = new System.Drawing.Point(10, 20), AutoSize = true };
            var nameTextBox = new TextBox { Location = new System.Drawing.Point(10, 45), Width = 300 };
            var okButton = new Button { Text = "ОК", Location = new System.Drawing.Point(10, 90), Size = new System.Drawing.Size(100, 30) };
            var cancelButton = new Button { Text = "Отмена", Location = new System.Drawing.Point(120, 90), Size = new System.Drawing.Size(100, 30) };

            okButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                {
                    MessageBox.Show("Введите название темы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TopicName = nameTextBox.Text;
                this.DialogResult = DialogResult.OK;
                MessageBox.Show(
        $"Добавлена тема '{TopicName}'.",
        "Успех",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    );

                this.Close();
            };

            cancelButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { nameLabel, nameTextBox, okButton, cancelButton });
        }
    }
}