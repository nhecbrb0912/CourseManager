using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseManager
{
    
    public class Course
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Module> Modules { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Course(string name, string description, DateTime startTime, DateTime endTime)
        {
            Name = name; Description = description;
            Modules = new List<Module>();
            StartTime = startTime; EndTime = endTime;
        }

        public void AddModule(Module module)
        {
            Modules.Add(module);
            MessageBox.Show($"Модуль '{module.Name}' добавлен к курсу '{Name}'.");
        }

        public void RemoveModule(Module module)
        {
            if (Modules.Contains(module)) { Modules.Remove(module); MessageBox.Show("Модуль удалён."); }
            else { MessageBox.Show("Модуль не найден."); }
        }

        public void DisplayCourseInfo()
        {
            var form = new CourseInfoForm(); form.Course = this; form.ShowDialog();
        }
    }

}
