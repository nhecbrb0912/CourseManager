using System;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class CreateCourseForm : Form
    {
        public string CourseName { get; private set; }
        public string Description { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public CreateCourseForm()
        {
            InitializeComponent();
        }


    }
}