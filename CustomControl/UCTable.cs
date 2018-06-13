using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CustomControl
{
    /// <summary>
    /// 展示图表Table，Author:Tony 20180610
    /// </summary>
   public class UCTable :Panel
    {
        const string category = "UCTable";
        const float marginDistance = 2;//Table距离外框的上下左右距离
        //记录子控件，在改变控件大小时，能够改变子控件的位置
        private List<Tuple<Control, int, int,bool>> children = new List<Tuple<Control, int, int,bool>>();
        #region 属性
        private float lineThickness = 0.2f;
        [Description("线条的粗细"),Category(category)]
        public float LineThickness
        {
            get { return lineThickness; }
            set
            {
                if (value >= 0)
                {
                    lineThickness = value;
                }
                else
                {
                    lineThickness = 0.2f;
                }
                Invalidate();
            }
        }

        private System.Drawing.Color color = System.Drawing.Color.Black;
        [Description("线条的颜色"),Category(category)]
        public Color Color
        {
            get { return color; }
            set { color = value; Invalidate(); }
        }

        private int rowCount = 3;
        [Description("列数"),Category(category)]
        public int RowCount
        {
            get { return rowCount; }
            set
            {
                if (value <= 0)
                {
                    rowCount = 3;
                }
                else
                {
                    rowCount = value;
                }
                Invalidate();
            }
        }


        private int columnCount = 3;
        [Description("行数"), Category(category)]
        public int ColumnCount
        {
            get { return columnCount; }
            set
            {
                if (value <= 0)
                {
                    columnCount = 3;
                }
                else
                {
                    columnCount = value;
                }
                Invalidate();
            }
        }

        private float? headerWidth = null;
        [Description("列头的宽度"),Category(category)]
        public float? HeaderWidth
        {
            get { return headerWidth; }
            set
            {
                if (value.HasValue && value.Value > 0)
                    headerWidth = value;
                else
                {
                    headerWidth = null;
                }
            }
        }

        private float? headerHeight = null;
        [Description("行头的高度"),Category(category)]
        public float? HeaderHeight
        {
            get { return headerHeight; }
            set
            {
                if (value.HasValue && value.Value > 0)
                    headerHeight = value;
                else
                {
                    headerHeight = null;
                }
            }
        }

        #endregion

        #region 构造函数
        public UCTable()
        {
            SetStyle(ControlStyles.UserPaint,true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion

        #region 方法
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawTable(e.Graphics);
            base.OnPaint(e);
            
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            foreach (var c in children)
            {
                SetControlLocation(c.Item1, c.Item2, c.Item3, c.Item4);
            }

        }
        private void DrawTable(Graphics graphics)
        {
            var e = graphics;
            e.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(Color,LineThickness);
            #region 画头
            float cellHeight =0, cellWidth=0;
            if (HeaderHeight.HasValue)
            {
                cellHeight = (this.Size.Height - marginDistance * 2-HeaderHeight.Value) / (float)(RowCount-1);
                //画开头的两条Row线
                e.DrawLine(pen,new PointF(marginDistance, marginDistance), 
                    new PointF(this.Size.Width - marginDistance,marginDistance));
                e.DrawLine(pen, new PointF(marginDistance, marginDistance + HeaderHeight.Value),
                    new PointF(this.Size.Width - marginDistance, marginDistance + HeaderHeight.Value));
            }
            else
            {
                cellHeight = (this.Size.Height - marginDistance * 2) / (float)RowCount;
                e.DrawLine(pen, new PointF(marginDistance, marginDistance+cellHeight*0),
                    new PointF(this.Size.Width - marginDistance,marginDistance+cellHeight*0));
                e.DrawLine(pen, new PointF(marginDistance, marginDistance + cellHeight * 1),
                    new PointF(this.Size.Width - marginDistance, marginDistance + cellHeight * 1));
            }

            if (HeaderWidth.HasValue)
            {
                cellWidth = (this.Size.Width - marginDistance * 2 - HeaderWidth.Value) / (float)(ColumnCount - 1);
                e.DrawLine(pen, new PointF(marginDistance, marginDistance), 
                    new PointF(marginDistance, this.Size.Height-marginDistance));
                e.DrawLine(pen, new PointF(marginDistance + HeaderWidth.Value, marginDistance),
                    new PointF(marginDistance + HeaderWidth.Value, this.Size.Height - marginDistance));
            }
            else
            {
                cellWidth = (this.Size.Width - marginDistance * 2) / (float)ColumnCount;
                e.DrawLine(pen,new PointF(marginDistance+cellWidth*0,marginDistance),
                    new PointF(marginDistance+cellWidth*0,this.Size.Height-marginDistance));
                e.DrawLine(pen, new PointF(marginDistance + cellWidth * 1, marginDistance),
                    new PointF(marginDistance + cellWidth * 1, this.Size.Height - marginDistance));
            }
            #endregion
            
            //画横线
            for (int i = 2; i <= RowCount; i++)
            {
                PointF startPoint = new PointF(marginDistance,HeaderHeight.HasValue?(marginDistance+HeaderHeight.Value + (i-1) * cellHeight):(marginDistance+i*cellHeight));
                PointF endPoint = new PointF(this.Size.Width-marginDistance, HeaderHeight.HasValue ? (marginDistance + HeaderHeight.Value + (i - 1) * cellHeight) : (marginDistance + i * cellHeight));
                e.DrawLine(pen,startPoint,endPoint);
            }
            //画竖线
            for (int i = 2; i <= ColumnCount; i++)
            {
                PointF startPoint = new PointF(HeaderWidth.HasValue?(marginDistance+HeaderWidth.Value+(i-1)*cellWidth): (marginDistance+i*cellWidth),marginDistance);
                PointF endPoint = new PointF(HeaderWidth.HasValue ? (marginDistance + HeaderWidth.Value + (i - 1) * cellWidth) : (marginDistance + i * cellWidth), this.Size.Height-marginDistance);
                e.DrawLine(pen,startPoint,endPoint);
            }
            pen.Dispose();           
        }
        /// <summary>
        /// 向单元格中添加控件，行和列是从0开始算的
        /// </summary>
        /// <param name="c">要添加的控件</param>
        /// <param name="row">控件所在的行</param>
        /// <param name="column">控件所在的列</param>
        /// <param name="isCenter">控件是否单元格中间显示</param>
        public void AddControl(Control c,int row,int column,bool isCenter=false)
        {
            SetControlLocation(c,row,column,isCenter);
            if (!this.Controls.Contains(c))
            {
                this.Controls.Add(c);
                children.Add(new Tuple<Control,int,int,bool>(c, row, column,isCenter));
            }           
        }
        private void SetControlLocation(Control c, int row, int column, bool isCenter)
        {
            if (row > RowCount - 1)
            {
                row = 0;
            }
            if (column > ColumnCount - 1)
            {
                column = 0;
            }

            float x;//单元格左上方的x坐标
            float y;//单元格右上方的y坐标
            float cellHeight;//单元格高度
            float cellWidth;//单元格宽度
            if (HeaderHeight.HasValue)
            {
                if (HeaderWidth.HasValue)
                {
                    cellHeight=row==0?HeaderHeight.Value: (this.Size.Height - marginDistance * 2-HeaderHeight.Value) / (float)(RowCount-1);
                    cellWidth = column == 0 ? HeaderWidth.Value : (this.Size.Width - marginDistance * 2 - HeaderWidth.Value) / (float)(ColumnCount - 1);
                    x = column == 0 ? marginDistance : (marginDistance+HeaderWidth.Value + (column - 1) * cellWidth);
                    y = row == 0 ? marginDistance : (marginDistance + HeaderHeight.Value + (row - 1) * cellHeight);
                }
                else
                {
                    cellHeight = row == 0 ? HeaderHeight.Value : (this.Size.Height - marginDistance * 2 - HeaderHeight.Value) / (float)(RowCount - 1);
                    cellWidth = (this.Size.Width - marginDistance * 2) / (float)ColumnCount;
                    x = marginDistance + column * cellWidth;
                    y = row == 0 ? marginDistance : (marginDistance + HeaderHeight.Value + (row - 1) * cellHeight);
                }
            }
            else
            {
                if (HeaderWidth.HasValue)
                {
                    cellHeight = (this.Size.Height - marginDistance * 2) / (float)RowCount;
                    cellWidth = column == 0 ? HeaderWidth.Value : (this.Size.Width - marginDistance * 2 - HeaderWidth.Value) / (float)(ColumnCount - 1);
                    x = column == 0 ? marginDistance : (marginDistance + HeaderWidth.Value + (column - 1) * cellWidth);
                    y = marginDistance + cellHeight * row;
                }
                else
                {
                    cellHeight = (this.Size.Height - marginDistance * 2) / (float)RowCount;
                    cellWidth = (this.Size.Width - marginDistance * 2) / (float)ColumnCount;
                    //单元格row,column的左上方坐标
                    x = marginDistance + cellWidth * column;
                    y = marginDistance + cellHeight * row;
                }
            }

            //float cellHeight = (this.Size.Height - marginDistance * 2) / (float)RowCount;
            //float cellWidth = (this.Size.Width - marginDistance * 2) / (float)ColumnCount;
            ////单元格row,column的左上方坐标
            //float x = marginDistance + cellWidth * column;
            //float y = marginDistance + cellHeight * row;
            
            if (isCenter)
            {
                //cell中心点
                float centerX_cell = x + cellWidth / 2;
                float centerY_cell = y + cellHeight / 2;
                //cell中心点-控件的宽/高除以2得到控件的坐标
                float x_control = centerX_cell - c.Size.Width / 2;
                float y_control = centerY_cell - c.Height / 2;
                c.Location = new Point((int)x_control, (int)y_control);
            }
            else
            {
                c.Location = new Point((int)x, (int)y);
            }
        }
        #endregion
    }
}
