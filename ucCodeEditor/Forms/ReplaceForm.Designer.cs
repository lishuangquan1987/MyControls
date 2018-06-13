namespace ucCodeEditor
{
    partial class ReplaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbFind = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(161, 43);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(100, 21);
            this.tbFind.TabIndex = 0;
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 97);
            this.Controls.Add(this.tbFind);
            this.Name = "ReplaceForm";
            this.Text = "ReplaceForm";
            this.Load += new System.EventHandler(this.ReplaceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbFind;
    }
}