using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseManager
{
    public class Module
    {
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
        public decimal Progress { get; set; }
        public Module(string name)
        {
            Name = name;
            Topics = new List<Topic>();
            Progress = 0;
        }
        public void AddTopic(Topic topic)
        {
            Topics.Add(topic);
            MessageBox.Show($"Тема '{topic.Name}' добавлена к модулю '{Name}'.");
        }
        public void RemoveTopic(Topic topic)
        {
            if (Topics.Contains(topic))
            {
                Topics.Remove(topic);
                MessageBox.Show("Тема удалена.");
            }
            else
            {
                MessageBox.Show("Тема не найдена.");
            }
        }
        public void UpdateProgress(decimal newProgress)
        {
            if (newProgress >= 0 && newProgress <= 100)
            {
                Progress = newProgress;
                MessageBox.Show($"Прогресс модуля '{Name}' обновлён до {Progress}%.");
            }
            else
            {
                MessageBox.Show("Неверное значение прогресса.");
            }
        }
        public void DisplayModuleInfo()
        {
            var moduleInfoForm = new ModuleInfoForm();
            moduleInfoForm.Module = this;
            moduleInfoForm.ShowDialog();
        }
    }
}
