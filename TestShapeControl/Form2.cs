using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestShapeControl
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private void checkBoxA_CheckedChanged(object sender, EventArgs e)
        {
            string tag = ((Control)sender).Tag.ToString();
            Control[] ctrls = this.panel1.Controls.Find("customControl1" + tag, false);
            if (ctrls.Length > 0)
            {
                ShapeControl.CustomControl1 cam = (ShapeControl.CustomControl1)ctrls[0];
                cam.AnimateBorder = ((CheckBox)sender).Checked;
            }


        }


        private void checkBoxB_CheckedChanged(object sender, EventArgs e)
        {
          string tag= ((Control)sender).Tag.ToString();
          Control[] ctrls=this.panel1.Controls.Find("customControl1" + tag, false);
          if (ctrls.Length > 0)
          {
              ShapeControl.CustomControl1 cam = (ShapeControl.CustomControl1)ctrls[0];
              cam.Blink = ((CheckBox)sender).Checked;
          }


        }

        private void checkBoxV_CheckedChanged(object sender, EventArgs e)
        {
            string tag = ((Control)sender).Tag.ToString();
            Control[] ctrls = this.panel1.Controls.Find("customControl1" + tag, false);
            if (ctrls.Length > 0)
            {
                ShapeControl.CustomControl1 cam = (ShapeControl.CustomControl1)ctrls[0];
                cam.Vibrate = ((CheckBox)sender).Checked;
            }
        }
    }
}
