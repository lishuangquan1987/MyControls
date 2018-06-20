using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.IO;

namespace TestShapeControl
{
    public partial class Form3 : Form
    {
        List<ShapeControl.CustomControl1> ctrllist = new List<ShapeControl.CustomControl1>(); //cam
        List<ShapeControl.CustomControl1> ctrllist1 = new List<ShapeControl.CustomControl1>(); //line
        List<ShapeControl.CustomControl1> linelist; //temp line list
        private int sx, sy; //mouse down start pos

        private int static_i = 0; 

        private bool ctrlKey = false;
        private bool altKey = false;
        private bool plusKey = false;
        private bool shiftKey = false;
        private bool minusKey = false;
        public Form3()
        {
            InitializeComponent();
        }

        private void btnAddCam_Click(object sender, EventArgs e)
        {
            AddCam("");
        }

        private int getNextCamIndex()
        {
            if (ctrllist.Count == 0) return 1;

            var tempvar = ctrllist.OrderBy(x => x.Name);
            tempvar=tempvar.OrderBy(x => x.Name.Length);
            List<ShapeControl.CustomControl1> templist =tempvar.ToList();
            int count = templist.Count;
            int retval = count+1;
            
            //find missing index
            for (int i = 0; i < count; i++)
            {
                string ctrlname = templist[i].Name;
                string ctrlindex = ctrlname.Substring(3);
                if ((i + 1) != int.Parse(ctrlindex))
                {
                    retval = (i + 1);
                    break;
                }
            }


            return retval;
        }

        //get list of line connected to the cam
        private List<ShapeControl.CustomControl1> getLines(int cam)
        {
            List<ShapeControl.CustomControl1> list=new List<ShapeControl.CustomControl1>();
            for (int i = 0; i < ctrllist1.Count; i++)
            {
                var v = (ctrllist1[i].Name).Split('_');
                if (int.Parse(v[1]) == cam || int.Parse(v[2]) == cam)
                    list.Add(ctrllist1[i]);
            }

            return list ;
        }

        //set position and size for the line shape control
        private void setLine(ref ShapeControl.CustomControl1 ctrl1)
        {
            //note that we want the arrow in the direction of the dest_cam
            int src_cam, dest_cam;
            var v = ctrl1.Name.Split('_');
            src_cam = int.Parse(v[1]);
            dest_cam = int.Parse(v[2]);

            Control[] ctrlsFromcam = this.panel1.Controls.Find("cam" + src_cam, false);
            Control[] ctrlsTocam = this.panel1.Controls.Find("cam" + dest_cam, false);

            //src_cam location
            int x0 = ctrlsFromcam[0].Location.X + ctrlsFromcam[0].Width / 2;
            int y0 = ctrlsFromcam[0].Location.Y + ctrlsFromcam[0].Height / 2;

            //dest_cam location
            int x1 = ctrlsTocam[0].Location.X + ctrlsTocam[0].Width / 2;
            int y1 = ctrlsTocam[0].Location.Y + ctrlsTocam[0].Height / 2;

            
            Control camleft, camright;

            //set direction base on location of des_cm in relation to src_cam
            if (x0 <= x1)  // dest_cam on the right
            {
                camleft = ctrlsFromcam[0];
                camright = ctrlsTocam[0];

                if(y0<y1) // dest_cam at the bottom
                    ctrl1.Direction=ShapeControl.LineDirection.RightDown;
                else      
                    ctrl1.Direction = ShapeControl.LineDirection.RightUp;
            }
            else //dest_cam on the left
            {
                camright= ctrlsFromcam[0];
                camleft = ctrlsTocam[0];

                if (y0 < y1) //dest_cam at the bottom
                    ctrl1.Direction = ShapeControl.LineDirection.LeftDown;
                else
                    ctrl1.Direction = ShapeControl.LineDirection.LeftUp;

            }

           
           //camleft location
           int xx0 = camleft.Location.X + camleft.Width / 2;
           int yy0 = camleft.Location.Y + camleft.Height / 2;

           //camright location
           int xx1 = camright.Location.X + camright.Width / 2;
           int yy1 = camright.Location.Y + camright.Height / 2;


            float gradient=0f;
  

            //correction when the line adjointing is becoming too steep or level
            //as these lines are not rendered very well
            //condition is that x or y delta are within 2 times the border width
            if (Math.Abs(xx0 - xx1) < (2*ctrl1.BorderWidth) || Math.Abs(yy0 - yy1) < (2*ctrl1.BorderWidth))
            {
              
                ctrl1.Shape = (Math.Abs(xx0 - xx1) < (2*ctrl1.BorderWidth)) ? 
                    ShapeControl.ShapeType.LineVertical : 
                    ShapeControl.ShapeType.LineHorizontal;
                
                ////set the corrections
                if (ctrl1.Shape == ShapeControl.ShapeType.LineVertical)
                    xx0 = xx1;
                
                if (ctrl1.Shape == ShapeControl.ShapeType.LineHorizontal)
                    yy0 = yy1;
                

            }
            else
            {
                gradient = (float)(yy0 - yy1) / (float)(xx0 - xx1);
                ctrl1.Shape = (gradient > 0f) ? ShapeControl.ShapeType.LineDown : ShapeControl.ShapeType.LineUp;
            }

            //all shape control are specified by
            //the top-left corner, width and height

            //lx0,ly0 : location for top-left of shape control
            //lx1,ly1 : location for bottom-right of shape control
            //lw: width of shape control
            //lh: height of shape control

            int lx0 = 0, ly0 = 0, lx1 = 0, ly1 = 0, lw = 0, lh = 0;

            switch (ctrl1.Shape)
            {
                case ShapeControl.ShapeType.LineVertical:
                    lx0 = xx0-10;
                    lx1 = xx1+10;
        
                    ly0 = yy0;
                    ly1 = yy1;


                    //correction when ly0 below ly1
                    if (ly0 > ly1)
                    {
                        ly0 = yy1;
                        ly1 = yy0;
                        lx0 = xx1 -10;
                        lx1 = xx0 +10;
                    }
 
                    lw = 20; //default width
                    lh = Math.Abs(yy0 - yy1);
                    //min height
                    if (lh < 10) lh = 10;

                    break;

                case ShapeControl.ShapeType.LineHorizontal:

                    ly0 = yy0 -10;
                    ly1 = yy1 +10;
                    lx0 = xx0;
                    lx1 = xx1;

                    //default height
                    lh = 20;
                    lw = Math.Abs(xx0 - xx1);

                    //min width
                    if (lw < 10) lw = 10;
                    break;
                case ShapeControl.ShapeType.LineUp:
                    lx0 = xx0;
                    ly0 = yy1;
                    lx1 = xx1;
                    ly1 = yy0;
                    lw = Math.Abs(xx0 - xx1);
                    lh = Math.Abs(yy0 - yy1);

                    //default size: area > 100
                    if ((lw * lw + lh * lh) < 100)
                        lh = 10;
                    break;
                case ShapeControl.ShapeType.LineDown:
                    lx0 = xx0;
                    ly0 = yy0;
                    lx1 = xx1;
                    ly1 = yy1;
                    lw = Math.Abs(xx0 - xx1);
                    lh = Math.Abs(yy0 - yy1);

                    //default size: area > 100
                    if ((lw * lw + lh * lh) < 100)
                        lh = 10;

                    break;

            }

            //we got the corrected location and size, so we set them here
            ctrl1.Size = new System.Drawing.Size(lw, lh);
            ctrl1.Location = new Point(lx0, ly0);

        }


        //add a line joining fromcam to tocam
        private void AddLine(int fromcam, int tocam, int argb)
        {
            ShapeControl.CustomControl1 ctrl2 = new ShapeControl.CustomControl1();
            ctrl2.Name = "line_"+ fromcam+"_" + tocam;//ctrllist.Count;


            ctrl2.MouseDoubleClick += new MouseEventHandler(ctrl2_MouseDoubleClick);
            ctrl2.MouseEnter += new EventHandler(ctrl2_MouseEnter);
            ctrl2.MouseLeave += new EventHandler(ctrl2_MouseLeave);
            setLine(ref ctrl2);
           
           // ctrl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), Color.Red);
            ctrl2.Blink = false;
            ctrl2.Vibrate = false;
            ctrl2.AnimateBorder = false;

            ctrl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            ctrl2.BorderColor = System.Drawing.Color.FromArgb(argb);
            ctrl2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            ctrl2.BorderWidth = 8;
            ctrl2.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Bold);



            ctrl2.UseGradient = false;  
            ctrl2.Visible = true;

             ctrllist1.Add(ctrl2);
            this.panel1.Controls.Add(ctrl2);
           // ctrl2.SendToBack();
            ctrl2.BringToFront();
            

        }

        void ctrl2_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void ctrl2_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }


        void ctrl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (ctrlKey && !altKey && !shiftKey )
                {
                    DialogResult dr = MessageBox.Show(this, "Delete line?", "Delete", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        ctrllist1.Remove((ShapeControl.CustomControl1)sender);
                        panel1.Controls.Remove((ShapeControl.CustomControl1)sender);
                    }
                    ctrlKey = false;
                    return;

                }



                if (altKey && !ctrlKey && !shiftKey )
                {
                    ((ShapeControl.CustomControl1)sender).AnimateBorder = !((ShapeControl.CustomControl1)sender).AnimateBorder;
                    altKey = false;
                    return;
                }


                if (!altKey && !ctrlKey && shiftKey)
                {
                    ShapeControl.CustomControl1 ctrl1 = ((ShapeControl.CustomControl1)sender);
                    var v = ctrl1.Name.Split('_');
                    string newname = v[0] + "_" + v[2] + "_" + v[1];
                    ctrl1.Name = newname;
                    setLine(ref ctrl1);
                    
                    shiftKey = false;
                    return;
                }

                ((ShapeControl.CustomControl1)sender).BorderColor = getNextColor();

            }
        

        }

        //adding a new cam base on caminfo
        private void AddCam(string caminfo)
        {

            
            bool bNew =(caminfo =="");
            string name="";
            string tag = "",tag2 = "";
            int x = 0, y = 0, w = 0, h = 0, c = 0;

            //caminfo format
            //name=cam1|x=400|y=146|w=40|h=40|c=2130640896|tag=16,-97,359956|tag2=127.0.0.1:New cam
            if (caminfo != "")
            {
                var info = caminfo.Split('|');
                for (int i = 0; i < info.Length; i++)
                {
                    var details = info[i].Split('=');
                    switch (details[0])
                    {
                        case "name": name = details[1]; break;
                        case "x": x = int.Parse (details[1]); break;
                        case "y": y = int.Parse (details[1]); break;
                        case "w": w = int.Parse(details[1]); break;
                        case "h": h = int.Parse(details[1]); break;
                        case "c": c = int.Parse(details[1]); break;
                        case "tag": tag = details[1]; break;
                        case "tag2": tag2 = details[1]; break; 
                    }

                }
            }
            ShapeControl.CustomControl1 ctrl1 = new ShapeControl.CustomControl1();
           
            ctrl1.BackColor = bNew?System.Drawing.Color.FromArgb(((int)(((byte)(126)))),Color.Red):Color.FromArgb(c);
            ctrl1.Blink = false;
            ctrl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            ctrl1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            ctrl1.BorderWidth = 3;

            ctrl1.Font = new System.Drawing.Font("Arial", 8f, System.Drawing.FontStyle.Bold);
                
            ctrl1.Name =bNew? "cam" + getNextCamIndex():name ;//ctrllist.Count;
            ctrl1.Shape = ShapeControl.ShapeType.Ellipse;
            ctrl1.ShapeImage = ((System.Drawing.Image)global::TestShapeControl.Properties.Resources.camshape);
            ctrl1.Size = bNew? new System.Drawing.Size(40, 40):new System.Drawing.Size(w,h);

            ctrl1.TabIndex = 0;
            ctrl1.UseGradient = false;
            ctrl1.Vibrate = false;
            ctrl1.Visible = true;
            

            ctrl1.MouseDown += new MouseEventHandler(ctrl1_MouseDown);
            ctrl1.MouseMove += new MouseEventHandler(ctrl1_MouseMove);
            ctrl1.MouseUp += new MouseEventHandler(ctrl1_MouseUp);
            ctrl1.MouseDoubleClick += new MouseEventHandler(ctrl1_MouseDoubleClick);
            ctrl1.MouseHover += new EventHandler(ctrl1_MouseHover);

            //for drag and drop
            ctrl1.DragEnter += new DragEventHandler(ctrl1_DragEnter);
            ctrl1.DragDrop += new DragEventHandler(ctrl1_DragDrop);
            ctrl1.AllowDrop = true;


            ctrllist.Add(ctrl1);
            int ypos = (50 * ctrllist.Count) % panel1.Height ;
            int xpos = ((50 * ctrllist.Count) / panel1.Height) * 50;
            
            ctrl1.Location =bNew? new System.Drawing.Point(50 + xpos, ypos-20):new System.Drawing.Point(50,50);
            this.panel1.Controls.Add(ctrl1);
            ctrl1.Text = "cam";
            ctrl1.Text = bNew ?(string)ctrl1.Name.ToString().Clone(): name ;
            ctrl1.BringToFront();
            ctrl1.Tag2=bNew? "127.0.0.1:New cam":tag2;

            //set the color
            if(bNew)
                ctrl1.BackColor = getNextColor();


            float dy = (float)(ctrl1.Top + ctrl1.Height /2) - (float)panel1.Height/2;
            float dx = (float)(ctrl1.Left +ctrl1.Width /2) -(float) panel1.Width/2;

            ctrl1.Tag = bNew? (dx + "," + dy+ "," +getNumPixelforImageDisplayed()):tag;
            
        }


        //when a cam is drop form source to dest
        void ctrl1_DragDrop(object sender, DragEventArgs e)
        {
            string s_src = e.Data.GetData(DataFormats.Text).ToString();
            string s_dest = ((Control)sender).Name;
            System.Diagnostics.Debug.Print("Data:" + s_src);
            if (s_dest != s_src)
            {
                int src_cam, dest_cam;
                src_cam = int.Parse(s_src.Substring(3));
                dest_cam =int.Parse(s_dest.Substring(3));
                 
                //find if there are already any line joining src_cam and dest_cam 
                Control[] ctrlsFromTo = this.panel1.Controls.Find("line_" + src_cam + "_" + dest_cam, false);
                Control[] ctrlsToFrom = this.panel1.Controls.Find("line_" + dest_cam + "_" + src_cam, false);

                 //default color of new line
                 int defaultargb = Color.FromArgb(126, Color.Red).ToArgb();

                // no line joining src_cam and dest_cam ,so we can add line
                // naming convention is:
                // line_<src_cam>_<dest_cam>
                 if (ctrlsFromTo.Length ==0 && ctrlsToFrom.Length ==0)
                     AddLine(src_cam, dest_cam, defaultargb);
                 

            }

        //    System.Diagnostics.Debug.Print("Drop " + data + " " + s);

        }


        void ctrl1_DragEnter(object sender, DragEventArgs e)
        {
            //source cam in data
            string src_cam =e.Data.GetData(DataFormats.Text).ToString();
            //dest cam in s
            string dest_cam = ((Control)sender).Name;
          //  System.Diagnostics.Debug.Print("Data:" + data  );

            //show the cursor for the Move effect
            if (src_cam  != dest_cam )
                e.Effect = DragDropEffects.Move;

          //  System.Diagnostics.Debug.Print("Enter " +  data + " " +s);
        }



        void ctrl1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button.Equals(MouseButtons.Left) && ctrlKey )
            //    System.Diagnostics.Debug.Print("mouse up " +((ShapeControl.CustomControl1)sender).Name);
        }

        void ctrl1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(((ShapeControl.CustomControl1)sender).Tag2 + ",(" +
                ((ShapeControl.CustomControl1)sender).Left + "," + ((ShapeControl.CustomControl1)sender).Top +")", 
                (ShapeControl.CustomControl1)sender, 2000);
        }


        private Color getNextColor()
        {
            
            if (static_i >= 6) static_i = 0;
            Color retcolr = System.Drawing.Color.FromArgb(126, Color.Red);
            switch (static_i)
            {
                case 0:
                    retcolr = System.Drawing.Color.FromArgb(126, Color.Red);
                    break;
                case 1:
                    retcolr = System.Drawing.Color.FromArgb(126, Color.Blue);

                    break;

                case 2:
                    retcolr = System.Drawing.Color.FromArgb(126, Color.Green);

                    break;

                case 3:
                    retcolr = System.Drawing.Color.FromArgb(126, Color.Wheat);
                    break;
                case 4:
                    retcolr = System.Drawing.Color.FromArgb(126, Color.GreenYellow);

                    break;

                case 5:
                    retcolr = System.Drawing.Color.FromArgb(126, Color.Cyan);

                    break;

            }
            static_i++;
            return retcolr;
        }

        private void ctrl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks < 2) return;

            if(e.Button.Equals(MouseButtons.Left))
            {
                if (plusKey && !minusKey)
                {
                    if (((ShapeControl.CustomControl1)sender).Width < 80)
                    {
                      ((ShapeControl.CustomControl1)sender).Size=
                          new Size(((ShapeControl.CustomControl1)sender).Width +5,((ShapeControl.CustomControl1)sender).Height+5);
 
                    }
                    plusKey = false;
                    return;
                }

                if (minusKey && !plusKey)
                {
                    if (((ShapeControl.CustomControl1)sender).Width > 20)
                    {
                        ((ShapeControl.CustomControl1)sender).Size =
                            new Size(((ShapeControl.CustomControl1)sender).Width - 5, ((ShapeControl.CustomControl1)sender).Height - 5);

                    }
                    minusKey = false;
                    return;
                }
                if(ctrlKey && !altKey)
                {
                   DialogResult dr=MessageBox.Show (this,"Delete cam?","Delete",MessageBoxButtons.OKCancel);
                    if (dr==DialogResult.OK )
                    {
                        //delete all lines connected to the cam
                        int camindex=int.Parse(((ShapeControl.CustomControl1)sender).Name.Substring(3));
                        
                        linelist = getLines(camindex);
                        for (int i = 0; i < linelist.Count; i++)
                        { 
                            ShapeControl.CustomControl1 line =linelist[i];
                            ctrllist1.Remove(line);
                            panel1.Controls.Remove(line);
                        }

                        ctrllist.Remove((ShapeControl.CustomControl1)sender);
                        panel1.Controls.Remove((ShapeControl.CustomControl1)sender);


                    }
                    ctrlKey = false;
                    return;
      
                }

                if (altKey && !ctrlKey )
                {
                    ((ShapeControl.CustomControl1)sender).Vibrate = !((ShapeControl.CustomControl1)sender).Vibrate;
                    altKey = false;
                    return;
                }

                ((ShapeControl.CustomControl1)sender).BackColor =getNextColor();


            }

            
        }

        private void ctrl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
    
                if (ctrlKey && altKey )
                {


                    ((Control)sender).DoDragDrop(((ShapeControl.CustomControl1)sender).Name, DragDropEffects.Copy | DragDropEffects.Move);
                    
                }
                else
                {

                    string s = ((ShapeControl.CustomControl1)sender).Name.Substring(3);
                    sx = e.X;
                    sy = e.Y;

                  
                    int camindex = int.Parse(s);
                    linelist = getLines(camindex);
                }

            }

            if(e.Button.Equals(MouseButtons.Right))
            {
                //if (ctrlKey )
                //{

                //  //  AddLine(1, 2);
                //    System.Diagnostics.Debug.Print("mouse down" + ((ShapeControl.CustomControl1)sender).Name);
                //    ((Control)sender).DoDragDrop("addline", DragDropEffects.All);
                //}
                //else
               // {
                    FormProperty frm = new FormProperty();
                    frm.Caller = (ShapeControl.CustomControl1)sender;

                    frm.ShowDialog();
               // }


            }
        }

        private void ctrl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ctrlKey || altKey) return;

            if (e.Button.Equals(MouseButtons.Left))
            {
                ((Control)sender).Left = ((Control)sender).Left + (e.X - sx);
                ((Control)sender).Top = ((Control)sender).Top + (e.Y - sy);

                ((Control)sender).Refresh();

                float dy = (float)((Control)sender).Top + ((Control)sender).Height / 2 - (float)panel1.Height / 2;
                float dx = (float)((Control)sender).Left + ((Control)sender).Width / 2 - (float)panel1.Width / 2;
                ((Control)sender).Tag = dx + "," + dy + "," + getNumPixelforImageDisplayed();

                for (int i = 0; i < linelist.Count; i++)
                {
                    var ctrl1 = linelist[i];
                    setLine(ref ctrl1);
                   
                    ctrl1.Parent.Refresh();
                    ctrl1.Refresh();
                }
                    

            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            ctrlKey = e.Control;
            altKey = e.Alt;
            shiftKey = e.Shift;
            if (e.KeyCode == Keys.OemMinus) minusKey = true;
            if (e.KeyCode == Keys.Oemplus ) plusKey = true;
        }

        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            ctrlKey =false;
            altKey = false;
            shiftKey = false;
            minusKey = false;
            plusKey = false;
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.Filter="Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            DialogResult dr=openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    using (var tempImage = new Bitmap(openFileDialog1.FileName))
                    {
                        panel1.BackgroundImage = new Bitmap(tempImage);
                    }
                }
                catch
                {

                }
            }

        }

        private int getNumPixelforImageDisplayed()
        {
            float panelratio = (float)panel1.Width / panel1.Height;
            float imgratio = (float)panel1.BackgroundImage.Width / (float)panel1.BackgroundImage.Height;
            float dispwidth,dispheight;
            if (panelratio > imgratio) //height limiting
            {
                dispheight = panel1.Height;
                dispwidth = imgratio * dispheight;
            }
            else
            {
                dispwidth = panel1.Width;
                dispheight = dispwidth / imgratio;
            }

           // System.Diagnostics.Debug.Print(imgratio +"," + dispwidth + "," + dispheight);
            
            return (int) (dispwidth * dispheight);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text ="NewMap_" + Guid.NewGuid().ToString() +".map";
            panel1.BackgroundImage = new Bitmap(panel1.Width, panel1.Height);
            Graphics.FromImage(panel1.BackgroundImage).FillRectangle(Brushes.White, new Rectangle(0, 0, panel1.Width, panel1.Height));
            Graphics.FromImage(panel1.BackgroundImage).DrawString("Dbl Click here to insert floor plan..",
                                 new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, 50, 50);
            ctrllist.Clear();
            ctrllist1.Clear();
            panel1.Controls.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = File.CreateText(label1.Text))
            {
                var tempvar=ctrllist.OrderBy(x=>x.Name);
                tempvar  =tempvar.OrderBy(x=>x.Name.Length);
                List<ShapeControl.CustomControl1 > templist=tempvar.ToList();
                writer.WriteLine("CAM_COUNT=" + templist.Count);
                for (int i = 0; i < templist.Count; i++)
                {
                    writer.WriteLine("name=" + templist[i].Name + "|" +
                                     "x=" + templist[i].Left + "|" +
                                     "y=" + templist[i].Top + "|" +
                                     "w=" + templist[i].Width + "|" +
                                     "h=" + templist[i].Height + "|" +
                                     "c=" + templist[i].BackColor.ToArgb()+ "|" +
                                     "tag=" + templist[i].Tag.ToString()+"|"+
                                     "tag2="+templist[i].Tag2.ToString());
                    
                }
                writer.WriteLine("LINE_COUNT=" + ctrllist1.Count);
                for (int i = 0; i < ctrllist1.Count; i++)
                {
                    writer.WriteLine("name=" +ctrllist1[i].Name +"|c=" + ctrllist1[i].BorderColor.ToArgb());
                }
            }
            if (panel1.BackgroundImage != null)
                panel1.BackgroundImage.Save(label1.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            MessageBox.Show(label1.Text + " is saved");
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            this.DoubleBuffered = true;

            //invoke double buffer 
            typeof(Panel ).InvokeMember(
               "DoubleBuffered",
               System.Reflection.BindingFlags.NonPublic | 
               System.Reflection.BindingFlags.Instance | 
               System.Reflection.BindingFlags.SetProperty,
               null,
               panel1,
               new object[] { true });

            label2.Text = "On Cam> Right Click:Set Properties, Dbl_Click:Change Color, Ctl+Dbl_Click:Del, Alt+Dbl_Click:Vibrate, Minus+Dbl_Click:Smaller, Plus+Dbl_Click:Larger," +
                          " Ctl+Alt Drag:Add Line\n" + "On Line>Dbl_Click:Change Color, Ctl + Dbl_click:Delete, Alt+Dbl_Click:Animate Border, Shift+Dbl_Click:Change Direction";
            
            button2_Click(null, null);
        }

        private void btnImportMap_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Map files (*.map)|*.map";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {

                button2_Click(null, null);
                label1.Text = openFileDialog1.FileName;

                try
                {
                    using (StreamReader reader = File.OpenText(label1.Text))
                    {
                        string s = reader.ReadLine();
                        //info example: CAM_COUNT=5
                        var info = s.Split('=');
                        for (int i = 0; i < int.Parse(info[1]); i++)
                        {
                            s = reader.ReadLine();
                            //format for s:
                            //name=cam1|x=400|y=146|w=40|h=40|c=2130640896|tag=16,-97,359956|tag2=127.0.0.1:New cam
                            AddCam(s);
                        }

                        s = reader.ReadLine();

                        if (s != null)
                        {
                            //info example: LINE_COUNT=5
                            info = s.Split('=');
                            
                            for (int i = 0; i < int.Parse(info[1]); i++)
                            {
                               //format : name=line_3_1|c=2130640896
                                s = reader.ReadLine();
                              
                                var v = s.Split('|');
                                //name=line_3_1
                                var v0 = v[0].Split('=');
                                var vv0 = v0[1].Split('_');

                                //c=2130640896
                                var v1 = v[1].Split('=');

                                AddLine(int.Parse(vv0[1]), int.Parse(vv0[2]),int.Parse(v1[1]));
                            }
                        }

                       
                    }

                //    this.panel1.Visible = false;
                    if(File.Exists(openFileDialog1.FileName+".jpg"))
                    using (var tempImage = new Bitmap(openFileDialog1.FileName+".jpg"))
                    {
                        panel1.BackgroundImage = new Bitmap(tempImage);
                    }


                    //resize

                    updateCamPosAfterResize();
                 //   this.panel1.Visible = true;
                }
                catch
                {

                }
            }

        }


        private void updateCamPosAfterResize()
        {
            int newarea = getNumPixelforImageDisplayed();
     

            for (int i = 0; i < ctrllist.Count; i++)
            {
                var info = ctrllist[i].Tag.ToString().Split(',');
                float dx = float.Parse(info[0]);
                float dy = float.Parse(info[1]);
                int area = int.Parse(info[2]);
                float ratio = (float)Math.Sqrt((float)newarea / area);
                float newdx=ratio * dx;
                float newdy=ratio * dy;

                ctrllist[i].Left = (int)(panel1.Width / 2 + newdx - ctrllist[i].Width / 2);
                ctrllist[i].Top = (int)(panel1.Height / 2 + newdy - ctrllist[i].Height / 2);


            }

            for (int i = 0; i < ctrllist.Count; i++)
            {
                int camindex = int.Parse(ctrllist[i].Name.Substring(3));
                linelist = getLines(camindex);
                for (int j = 0; j < linelist.Count; j++)
                {
                    var ctrl1 = linelist[j];
                    setLine(ref ctrl1);

                }

            }



        }

        private void Form3_Resize(object sender, EventArgs e)
        {
  
            this.panel1.Visible = false;

            panel1.Height = this.ClientSize.Height - ( 3* panel1.Top/2);
            panel1.Width = this.ClientSize.Width - 2 * panel1.Left;
            label2.Top = panel1.Top + panel1.Height + 2;
            updateCamPosAfterResize();

            this.panel1.Visible = true;

        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }





    }
}
