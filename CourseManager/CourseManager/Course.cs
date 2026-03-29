using System;
using System.Collections.Generic;

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
        }

        public void RemoveModule(Module module)
        {
            if (Modules.Contains(module))
            {
                Modules.Remove(module);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}