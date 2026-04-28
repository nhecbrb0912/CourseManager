namespace CourseManager
{
    public class Topic
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public Topic(string name)
        {
            Name = name;
            IsCompleted = false;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        public override string ToString()
        {
            return $"{Name}{(IsCompleted ? " ✓" : "")}";
        }
    }
}