using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace ucCodeEditor
{
    public partial class ucLuaEditor : UserControl
    {
        public ucLuaEditor()
        {
            InitializeComponent();
            CommConfig.AppLocationPath = Application.StartupPath;
            UpStyle();

            InitStylesPriority();
        }

        private void UpStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer 
                | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        private void CreatNew()
        {
            #region 初始化数据
            txtEditor.Text = @"";
            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("ucCodeEditor.SampleScript.A2750S.lua", this.GetType().Assembly);
            //txtEditor.Text= rm.GetString("A2750S.lua");
            txtEditor.Text = Resource.LuaSource;
            #endregion
        }

        private void InitStylesPriority()
        {

            txtEditor.ClearUndo();
            txtEditor.IsChanged = false;

            CreatNew();

            KeyWordsAuto.CreatMenu(txtEditor, imageList1);
            //KeysWordCMDAuto.CreatMenu(consoleTextBox1, imageList1);
        }

        #region 动画
        public bool isShow = true;
        int loadSpeed = 10;
        public void StartMeun()
        {
            if (isShow)
            {
                timerBack.Enabled = true;
            }
            else
            {
                timerCell.Enabled = true;
            }
        }
        private void CellOutputHide()
        {
            panel1.Height -= loadSpeed;
            loadSpeed += loadSpeed;
            if (panel1.Height <= 30)
            {
                timerCell.Enabled = false;
                panel1.Height = 30;
                loadSpeed = 10;

                //back
                timerBack.Enabled = true;
            }
        }
        private void CellOutputShow()
        {
            panel1.Height += loadSpeed;
            loadSpeed += loadSpeed;
            if (panel1.Height >= 310)
            {
                timerCell.Enabled = false;
                panel1.Height = 310;
                loadSpeed = 10;

                //OK
                // picDic.Image = Properties.Resources.rdir1;
                isShow = false;//显示过程完成
            }
        }

        private void backOutputHide()
        {
            panel1.Left += loadSpeed;
            loadSpeed += loadSpeed;
            if (panel1.Left >= this.Width)
            {
                timerBack.Enabled = false;
                panel1.Left = this.Width;
                loadSpeed = 10;

                //OK
                //picDic.Image = Properties.Resources.dir1;
                isShow = true;//显示过程结束
            }
        }
        private void backOutputShow()
        {
            panel1.Left -= loadSpeed;
            loadSpeed += loadSpeed;
            if (panel1.Left <= this.Width - panel1.Width - 18)
            {
                timerBack.Enabled = false;
                panel1.Left = this.Width - panel1.Width - 18;
                loadSpeed = 10;

                //celll
                timerCell.Enabled = true;
            }
        }

        private void timerCell_Tick(object sender, EventArgs e)
        {
            if (isShow)
            {
                CellOutputShow();
            }
            else
            {
                CellOutputHide();
            }
        }

        private void timerBack_Tick(object sender, EventArgs e)
        {
            if (isShow)
            {
                backOutputShow();
            }
            else
            {
                backOutputHide();
            }
        }

        #endregion

        #region 添加UI

        public void AddCsc()
        {
            panControlView.Controls.Clear();
            ComplieUI cp = new ComplieUI();
            cp.BackColor = Color.Transparent;
            cp.tb = this.txtEditor;
            cp.Dock = DockStyle.Fill;
            panControlView.Controls.Add(cp);
        }

        public void AddClassFileList()
        {
            panControlView.Controls.Clear();
            ClassFileList cp = new ClassFileList();
            cp.BackColor = Color.Transparent;
            cp.fctb = this.txtEditor;
            cp.Dock = DockStyle.Fill;
            panControlView.Controls.Add(cp);
        }

        public void AddText(string txt)
        {
            panControlView.Controls.Clear();
            TextBox tx = new TextBox();
            tx.Text = txt;
            tx.Dock = DockStyle.Fill;
            tx.BorderStyle = BorderStyle.None;
            tx.BackColor = Color.FromArgb(30, 17, 18);
            tx.ForeColor = Color.Green;
            tx.Multiline = true;
            tx.ScrollBars = ScrollBars.Both;
            tx.Font = new System.Drawing.Font("微软雅黑", 9);
            panControlView.Controls.Add(tx);
        }

        #endregion

        private void ucLuaEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.Up)
            { txtEditor.Focus(); }
            else if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.Down)
            { 
                //consoleTextBox1.Focus(); 
            }
        }

        private void txtEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (CompliReslut.RowErrorNumber != -1)
            {
                txtEditor[CompliReslut.RowErrorNumber - 1].BackgroundBrush = null;
                txtEditor.Invalidate();
                CompliReslut.RowErrorNumber = -1;
            }
        }

        private void contextMenuStripEx1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txtEditor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void toolItemCopy_Click(object sender, EventArgs e)
        {
            if (txtEditor.Selection.End.iChar > 0)
            {
                txtEditor.Copy();
            }
        }

        private void toolItemCut_Click(object sender, EventArgs e)
        {
            if (txtEditor.Selection.End.iChar > 0)
            {
                txtEditor.Cut();
            }
        }

        private void toolItemPaste_Click(object sender, EventArgs e)
        {
            txtEditor.Paste();
        }

        private void toolItemSelectAll_Click(object sender, EventArgs e)
        {
            txtEditor.SelectAll();
        }

        private void toolItemClear_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
        }

        private void txtEditor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ucLuaEditor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
