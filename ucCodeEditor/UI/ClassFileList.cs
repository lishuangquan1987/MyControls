using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace ucCodeEditor
{
    public partial class ClassFileList : UserControl
    {
        public ClassFileList()
        {
            InitializeComponent();
            LoadFile();
        }

        public textEditor fctb { get; set; }

        public void LoadFile()
        {
            if (!Directory.Exists(CommConfig.ClassLibPath))
            {
                textBox1.Text = "无可用文件！";
                return;

            }
            foreach (var i in Directory.GetFiles(CommConfig.ClassLibPath))
            {
                listBox1.Items.Add(i);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = File.ReadAllText(listBox1.Text);
            Regex reg = new Regex(@"(?<!/)/\*([^*/]|\*(?!/)|/(?<!\*))*((?=\*/))(\*/)");
            Match m = reg.Match(s, 0);
            if (m.Success)
            {
                textBox1.Text = m.Value;
            }
            else
            {
                textBox1.Text = "无可用数据";
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (fctb != null)
            {
                fctb.Text = File.ReadAllText(listBox1.Text);
            }
        }
    }
}
