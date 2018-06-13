using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Drawing.Drawing2D;

namespace ucCodeEditor
{
    public partial class ComplieUI : UserControl
    {
        public ComplieUI()
        {
            InitializeComponent();
        }

        private void ComplieUI_Load(object sender, EventArgs e)
        {

        }

        public textEditor tb { get; set; }

        private void btnCsc_Click(object sender, EventArgs e)
        {
            UIConfig(false);
            CreatFile();

            txtOutput.Text = runStringCSC(MakeCommond());//编译

            if (File.Exists(CommConfig.ExeFilePath) || File.Exists(CommConfig.DllFilePath))
            {
                txtOutput.Text = "====================编译成功==================\r\n";
                if (radExe.Checked || radWinexe.Checked)//测试*.exe文件
                {
                    if (chkCmd.Checked || radWinexe.Checked)//判断是否使用CMD
                    {
                        System.Diagnostics.Process.Start(CommConfig.ExeFilePath);
                    }
                    else
                    {
                        txtOutput.Text += "\r\n" + runStringTemp(txtArgs.Text);
                    }
                }
            }
            else
                txtOutput.Text += "==================编译失败!!=======================";

            UIConfig(true);
        }
        public void UIConfig(bool isLock)
        {
            tb.ReadOnly = !isLock;
            txtArgs.Enabled = isLock;
            radExe.Enabled = isLock;
            radWinexe.Enabled = isLock;
            radDll.Enabled = isLock;
            chkCmd.Enabled = isLock;
        }
        private string MakeCommond()
        {
            StringBuilder sb = new StringBuilder();

            if (radExe.Checked)
                sb.Append("/target:exe  /out:" + CommConfig.ExeFilePath + " " + CommConfig.SrcFilePath);
            else if (radWinexe.Checked)
                sb.Append("/target:winexe  /out:" + CommConfig.ExeFilePath + " " + CommConfig.SrcFilePath);
            else
                sb.Append("/target:library /out:" + CommConfig.DllFilePath + " " + CommConfig.SrcFilePath);
            return sb.ToString();
        }
        //测试生成文件
        private string runStringTemp(string commond)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = CommConfig.ExeFilePath;
            p.StartInfo.Arguments = commond;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(commond);
            return p.StandardOutput.ReadToEnd();

        }
        //编译函数
        private string runStringCSC(string commond)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = CommConfig.CscPath + "\\csc.exe";
            p.StartInfo.Arguments = commond;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(commond);
            // p.StandardInput.WriteLine("exit");
            return p.StandardOutput.ReadToEnd();

        }
        private void CreatFile()
        {
            if (Directory.Exists(CommConfig.BinPath))
                Directory.Delete(CommConfig.BinPath, true);
            Directory.CreateDirectory(CommConfig.BinPath);
            Thread.Sleep(500);
            StreamWriter strCode = new StreamWriter(File.OpenWrite(CommConfig.SrcFilePath), System.Text.Encoding.Default);
            strCode.WriteLine(tb.Text);
            strCode.Close();
        }

        private void texstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Copy();
        }

        private void 清楚ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

    }
}
