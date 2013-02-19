using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class MultiImageToolStripMenuItem : ToolStripMenuItem
    {
        public MultiImageToolStripMenuItem()
        {
            InitializeComponent();
        }

        public Image DisabledImage { get; set; }
        public Image EnabledImage { get; set; }

        public void Enable(bool value)
        {
            Image = value ? EnabledImage : DisabledImage;
            Enabled = value;
        }

    }
}
