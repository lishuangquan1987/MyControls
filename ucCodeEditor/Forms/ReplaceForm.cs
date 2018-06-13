using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ucCodeEditor
{
    public partial class ReplaceForm : Form
    {

        textEditor tb;
        bool firstSearch = true;
        Place startPlace;

        public ReplaceForm()
        {
            InitializeComponent();
        }

        public ReplaceForm(textEditor editor)
        {
            InitializeComponent();
            this.tb = editor;
        }

        private void ReplaceForm_Load(object sender, EventArgs e)
        {

        }
    }
}
