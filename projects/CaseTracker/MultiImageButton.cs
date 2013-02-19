using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class MultiImageButton : Button
    {
        public MultiImageButton()
        {
            InitializeComponent();
        }

        public Image DisabledBackgroundImage { get; set; }
        public Image EnabledBackgroundImage {  get; set; }

        public void Enable (bool value) {
            BackgroundImage = value ? EnabledBackgroundImage : DisabledBackgroundImage;
            Enabled = value;
        }

    }
}
