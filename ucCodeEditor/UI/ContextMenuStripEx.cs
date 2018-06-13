using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ucCodeEditor
{
    public partial class ContextMenuStripEx : ContextMenuStrip
    {

        public ContextMenuStripEx()
            : base()
        {
            this.RenderMode = ToolStripRenderMode.System;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), 
                new Rectangle(-1, -1, this.ClientRectangle.Width + 2, this.ClientRectangle.Height + 2));
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.WhiteSmoke), 1.0f), 
                new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1));
        }
    }
}
