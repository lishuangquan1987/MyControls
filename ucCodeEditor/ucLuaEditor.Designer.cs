namespace ucCodeEditor
{
    partial class ucLuaEditor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLuaEditor));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timerCell = new System.Windows.Forms.Timer(this.components);
            this.timerBack = new System.Windows.Forms.Timer(this.components);
            this.panControlView = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtEditor = new ucCodeEditor.textEditor();
            this.contextMenuStripEx1 = new ucCodeEditor.ContextMenuStripEx();
            this.toolItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Black;
            this.imageList1.Images.SetKeyName(0, "s.png");
            this.imageList1.Images.SetKeyName(1, "d.png");
            this.imageList1.Images.SetKeyName(2, "m.png");
            this.imageList1.Images.SetKeyName(3, "k.png");
            this.imageList1.Images.SetKeyName(4, "c.png");
            // 
            // timerCell
            // 
            this.timerCell.Interval = 10;
            this.timerCell.Tick += new System.EventHandler(this.timerCell_Tick);
            // 
            // timerBack
            // 
            this.timerBack.Interval = 10;
            this.timerBack.Tick += new System.EventHandler(this.timerBack_Tick);
            // 
            // panControlView
            // 
            this.panControlView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panControlView.Location = new System.Drawing.Point(0, 1);
            this.panControlView.Name = "panControlView";
            this.panControlView.Size = new System.Drawing.Size(265, 26);
            this.panControlView.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Controls.Add(this.panControlView);
            this.panel1.Location = new System.Drawing.Point(487, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 30);
            this.panel1.TabIndex = 2;
            // 
            // txtEditor
            // 
            this.txtEditor.AllowDrop = true;
            this.txtEditor.AutoScrollMinSize = new System.Drawing.Size(31, 21);
            this.txtEditor.BackBrush = null;
            this.txtEditor.CommentPrefix = "//";
            this.txtEditor.ContextMenuStrip = this.contextMenuStripEx1;
            this.txtEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEditor.DelayedEventsInterval = 1000;
            this.txtEditor.DelayedTextChangedInterval = 1000;
            this.txtEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditor.Font = new System.Drawing.Font("宋体", 14F);
            this.txtEditor.IsReplaceMode = false;
            this.txtEditor.Language = ucCodeEditor.Language.CSharp;
            this.txtEditor.Location = new System.Drawing.Point(0, 0);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.txtEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtEditor.Size = new System.Drawing.Size(465, 361);
            this.txtEditor.TabIndex = 0;
            this.txtEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEditor_KeyPress);
            this.txtEditor.TextChanged += new System.EventHandler<ucCodeEditor.TextChangedEventArgs>(this.txtEditor_TextChanged);
            this.txtEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEditor_KeyDown);
            // 
            // contextMenuStripEx1
            // 
            this.contextMenuStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemCopy,
            this.toolItemCut,
            this.toolStripMenuItem4,
            this.toolItemPaste,
            this.toolStripMenuItem5,
            this.toolItemSelectAll,
            this.toolStripMenuItem6,
            this.toolItemClear});
            this.contextMenuStripEx1.Name = "contextMenuStripEx1";
            this.contextMenuStripEx1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStripEx1.Size = new System.Drawing.Size(95, 132);
            this.contextMenuStripEx1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripEx1_Opening);
            // 
            // toolItemCopy
            // 
            this.toolItemCopy.BackColor = System.Drawing.SystemColors.Control;
            this.toolItemCopy.ForeColor = System.Drawing.Color.Black;
            this.toolItemCopy.Name = "toolItemCopy";
            this.toolItemCopy.Size = new System.Drawing.Size(94, 22);
            this.toolItemCopy.Text = "复制";
            this.toolItemCopy.Click += new System.EventHandler(this.toolItemCopy_Click);
            // 
            // toolItemCut
            // 
            this.toolItemCut.ForeColor = System.Drawing.Color.Black;
            this.toolItemCut.Name = "toolItemCut";
            this.toolItemCut.Size = new System.Drawing.Size(94, 22);
            this.toolItemCut.Text = "剪切";
            this.toolItemCut.Click += new System.EventHandler(this.toolItemCut_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(91, 6);
            // 
            // toolItemPaste
            // 
            this.toolItemPaste.ForeColor = System.Drawing.Color.Black;
            this.toolItemPaste.Name = "toolItemPaste";
            this.toolItemPaste.Size = new System.Drawing.Size(94, 22);
            this.toolItemPaste.Text = "粘贴";
            this.toolItemPaste.Click += new System.EventHandler(this.toolItemPaste_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(91, 6);
            // 
            // toolItemSelectAll
            // 
            this.toolItemSelectAll.ForeColor = System.Drawing.Color.Black;
            this.toolItemSelectAll.Name = "toolItemSelectAll";
            this.toolItemSelectAll.Size = new System.Drawing.Size(94, 22);
            this.toolItemSelectAll.Text = "全选";
            this.toolItemSelectAll.Click += new System.EventHandler(this.toolItemSelectAll_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(91, 6);
            // 
            // toolItemClear
            // 
            this.toolItemClear.ForeColor = System.Drawing.Color.Black;
            this.toolItemClear.Name = "toolItemClear";
            this.toolItemClear.Size = new System.Drawing.Size(94, 22);
            this.toolItemClear.Text = "清除";
            this.toolItemClear.Click += new System.EventHandler(this.toolItemClear_Click);
            // 
            // ucLuaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtEditor);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "ucLuaEditor";
            this.Size = new System.Drawing.Size(465, 361);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLuaEditor_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ucLuaEditor_KeyDown);
            this.panel1.ResumeLayout(false);
            this.contextMenuStripEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timerCell;
        private System.Windows.Forms.Timer timerBack;
        private System.Windows.Forms.Panel panControlView;
        private System.Windows.Forms.Panel panel1;
        private ucCodeEditor.ContextMenuStripEx contextMenuStripEx1;
        private System.Windows.Forms.ToolStripMenuItem toolItemCopy;
        private System.Windows.Forms.ToolStripMenuItem toolItemCut;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolItemSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolItemClear;
        public textEditor txtEditor;
    }
}
