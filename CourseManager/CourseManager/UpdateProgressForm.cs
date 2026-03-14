using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseManager
{
    public partial class UpdateProgressForm : Form
    {
        public decimal Progress { get; private set; }
        public UpdateProgressForm()
        {
            InitializeComponent();
        }

    }
}
