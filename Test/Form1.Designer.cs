namespace Test
{
    partial class Form1
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
            this.ucLuaEditor1 = new ucCodeEditor.ucLuaEditor();
            this.SuspendLayout();
            // 
            // ucLuaEditor1
            // 
            this.ucLuaEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLuaEditor1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ucLuaEditor1.Location = new System.Drawing.Point(0, 0);
            this.ucLuaEditor1.Name = "ucLuaEditor1";
            this.ucLuaEditor1.Size = new System.Drawing.Size(1092, 722);
            this.ucLuaEditor1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 722);
            this.Controls.Add(this.ucLuaEditor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ucCodeEditor.ucLuaEditor ucLuaEditor1;
    }
}