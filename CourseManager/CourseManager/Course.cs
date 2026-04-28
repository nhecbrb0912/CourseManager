using System;
using System.Collections.Generic;
using System.Linq;
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
            Name = name;
            Description = description;
            Modules = new List<Module>();
            StartTime = startTime;
            EndTime = endTime;
        }

        public void AddModule(Module module)
        {
            Modules.Add(module);
            MessageBox.Show(
                $"Модуль «{module.Name}» успешно добавлен в учебный курс «{Name}».",
                "Модуль добавлен",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void RemoveModule(Module module)
        {
            if (Modules.Contains(module))
            {
                Modules.Remove(module);
                MessageBox.Show(
                    $"Модуль «{module.Name}» удалён из курса «{Name}».",
                    "Модуль удалён",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show("Модуль не найден в курсе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public decimal CalculateOverallProgress()
        {
            if (Modules == null || Modules.Count == 0)
                return 0;

            decimal totalProgress = 0;
            foreach (var module in Modules)
            {
                totalProgress += module.Progress;
            }
            return totalProgress / Modules.Count;
        }

        public int GetCompletedModulesCount()
        {
            if (Modules == null)
                return 0;
            return Modules.Count(m => m.Progress >= 100);
        }

        public int GetTotalTopicsCount()
        {
            if (Modules == null)
                return 0;
            int total = 0;
            foreach (var module in Modules)
            {
                if (module.Topics != null)
                    total += module.Topics.Count;
            }
            return total;
        }

        public bool IsCourseCompleted()
        {
            return CalculateOverallProgress() >= 100;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}