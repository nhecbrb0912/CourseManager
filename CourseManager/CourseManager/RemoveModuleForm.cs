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
    public partial class RemoveModuleForm : Form
    {
        public Module Module { get; private set; }
        private List<Module> modules;
        public RemoveModuleForm(List<Module> modules)
        {
            this.modules = modules;
            InitializeComponent();
        }

    }
}
