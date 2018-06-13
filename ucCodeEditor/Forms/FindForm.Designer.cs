namespace ucCodeEditor
{
    partial class FindForm
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
            this.cbWholeWord = new System.Windows.Forms.CheckBox();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(104, 12);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(100, 21);
            this.tbFind.TabIndex = 0;
            // 
            // cbWholeWord
            // 
            this.cbWholeWord.AutoSize = true;
            this.cbWholeWord.Location = new System.Drawing.Point(340, 86);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new System.Drawing.Size(78, 16);
            this.cbWholeWord.TabIndex = 1;
            this.cbWholeWord.Text = "checkBox1";
            this.cbWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(340, 14);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(78, 16);
            this.cbMatchCase.TabIndex = 1;
            this.cbMatchCase.Text = "checkBox1";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            // 
            // cbRegex
            // 
            this.cbRegex.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cbRegex.AutoSize = true;
            this.cbRegex.Location = new System.Drawing.Point(340, 46);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new System.Drawing.Size(78, 16);
            this.cbRegex.TabIndex = 1;
            this.cbRegex.Text = "checkBox1";
            this.cbRegex.UseVisualStyleBackColor = true;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 134);
            this.Controls.Add(this.cbRegex);
            this.Controls.Add(this.cbMatchCase);
            this.Controls.Add(this.cbWholeWord);
            this.Controls.Add(this.tbFind);
            this.Name = "FindForm";
            this.Text = "FindForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.CheckBox cbWholeWord;
        private System.Windows.Forms.CheckBox cbMatchCase;
        private System.Windows.Forms.CheckBox cbRegex;
    }
}