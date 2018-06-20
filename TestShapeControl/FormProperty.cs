using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestShapeControl
{
    public partial class FormProperty : Form
    {

        private ShapeControl.CustomControl1  _caller=null;

        public ShapeControl.CustomControl1  Caller
        {
            get { return _caller; }
            set
            {
                _caller = value;
                string s = _caller.Tag2;
                var info = s.Split(':');
                textBoxIP.Text  = info[0];
                textBoxNotes.Text = info[1];
                this.Text = _caller.Text + " properties";

            }
        }
        public FormProperty()
        {
            InitializeComponent();
        }

        private void FormProperty_FormClosing(object sender, FormClosingEventArgs e)
        {
            //should validate first
            _caller.Tag2 =textBoxIP.Text  +":" +textBoxNotes.Text ;
        }

        private void FormProperty_Activated(object sender, EventArgs e)
        {
            this.Location = _caller.Location;
        }

        private void textBoxIP_Enter(object sender, EventArgs e)
        {
            textBoxIP.SelectionStart = textBoxIP.Text.Length;
        }
    }
}
