using System;
using System.Collections.Generic;

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
        }

        public void RemoveTopic(Topic topic)
        {
            if (Topics.Contains(topic))
            {
                Topics.Remove(topic);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}