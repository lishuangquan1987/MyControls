using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace TestShapeControl
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private ShapeControl.CustomControl1 shapeControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private Bitmap bm;
		private int sx,sy;
       // private Region tempregion;
		private ShapeControl.CustomControl1 shapeControl3;
		private ShapeControl.CustomControl1 shapeControl4;
        private ShapeControl.CustomControl1 shapeControl5;
        private ShapeControl.CustomControl1 shapeControl7;
        private ShapeControl.CustomControl1 shapeControl6;
        private ShapeControl.CustomControl1 customControl11;
        private ShapeControl.CustomControl1 customControl12;
        private ShapeControl.CustomControl1 customControl13;
        private ShapeControl.CustomControl1 customControl14;
        private ShapeControl.CustomControl1 customControl15;
        private ShapeControl.CustomControl1 customControl16;
        private ShapeControl.CustomControl1 customControl17;
        private ShapeControl.CustomControl1 customControl18;
        private ShapeControl.CustomControl1 customControl19;
        private ShapeControl.CustomControl1 customControl110;
        private ShapeControl.CustomControl1 customControl111;
        private ShapeControl.CustomControl1 customControl112;
        private ShapeControl.CustomControl1 customControl113;
		private ShapeControl.CustomControl1 shapeControl2;


		public Form1()
		{
			
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			bm=new Bitmap(this.panel1.Width,this.panel1.Height);
			Graphics g=System.Drawing.Graphics.FromImage(bm);
			g.FillRectangle(Brushes.LightBlue,new Rectangle(0,0,this.panel1.Width,this.panel1.Height));
			StringFormat sf=new StringFormat();
			sf.Alignment=StringAlignment.Center;
			sf.LineAlignment=StringAlignment.Center;
			g.DrawString("THIS IS THE TEST BACKGROUND",new Font("Arial",18,FontStyle.Bold),Brushes.Brown,
				        new Rectangle(new Point(0,0),new Size(this.panel1.Width,this.panel1.Height)),sf);
			this.panel1.BackgroundImage =bm;

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.customControl113 = new ShapeControl.CustomControl1();
            this.shapeControl6 = new ShapeControl.CustomControl1();
            this.customControl112 = new ShapeControl.CustomControl1();
            this.customControl111 = new ShapeControl.CustomControl1();
            this.customControl110 = new ShapeControl.CustomControl1();
            this.customControl19 = new ShapeControl.CustomControl1();
            this.customControl18 = new ShapeControl.CustomControl1();
            this.customControl17 = new ShapeControl.CustomControl1();
            this.customControl16 = new ShapeControl.CustomControl1();
            this.customControl15 = new ShapeControl.CustomControl1();
            this.customControl14 = new ShapeControl.CustomControl1();
            this.customControl13 = new ShapeControl.CustomControl1();
            this.customControl12 = new ShapeControl.CustomControl1();
            this.customControl11 = new ShapeControl.CustomControl1();
            this.shapeControl5 = new ShapeControl.CustomControl1();
            this.shapeControl7 = new ShapeControl.CustomControl1();
            this.shapeControl4 = new ShapeControl.CustomControl1();
            this.shapeControl3 = new ShapeControl.CustomControl1();
            this.shapeControl2 = new ShapeControl.CustomControl1();
            this.shapeControl1 = new ShapeControl.CustomControl1();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.shapeControl1);
            this.panel1.Location = new System.Drawing.Point(10, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 338);
            this.panel1.TabIndex = 2;
            // 
            // customControl113
            // 
            this.customControl113.AnimateBorder = false;
            this.customControl113.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.customControl113.Blink = false;
            this.customControl113.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl113.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl113.BorderWidth = 0;
            this.customControl113.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl113.Direction = ShapeControl.LineDirection.None;
            this.customControl113.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl113.Location = new System.Drawing.Point(896, 235);
            this.customControl113.Name = "customControl113";
            this.customControl113.Shape = ShapeControl.ShapeType.Rectangle;
            this.customControl113.ShapeImage = ((System.Drawing.Image)(resources.GetObject("customControl113.ShapeImage")));
            this.customControl113.Size = new System.Drawing.Size(68, 62);
            this.customControl113.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl113.TabIndex = 21;
            this.customControl113.Tag2 = "";
            this.customControl113.UseGradient = false;
            this.customControl113.Vibrate = false;
            // 
            // shapeControl6
            // 
            this.shapeControl6.AnimateBorder = false;
            this.shapeControl6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(221)))), ((int)(((byte)(152)))), ((int)(((byte)(53)))));
            this.shapeControl6.Blink = false;
            this.shapeControl6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl6.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl6.BorderWidth = 0;
            this.shapeControl6.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(202)))), ((int)(((byte)(91)))), ((int)(((byte)(171)))));
            this.shapeControl6.Direction = ShapeControl.LineDirection.None;
            this.shapeControl6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl6.Location = new System.Drawing.Point(323, 262);
            this.shapeControl6.Name = "shapeControl6";
            this.shapeControl6.Shape = ShapeControl.ShapeType.Ellipse;
            this.shapeControl6.ShapeImage = ((System.Drawing.Image)(resources.GetObject("shapeControl6.ShapeImage")));
            this.shapeControl6.Size = new System.Drawing.Size(125, 93);
            this.shapeControl6.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(198)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
            this.shapeControl6.TabIndex = 9;
            this.shapeControl6.Tag2 = "";
            this.shapeControl6.UseGradient = true;
            this.shapeControl6.Vibrate = false;
            // 
            // customControl112
            // 
            this.customControl112.AnimateBorder = false;
            this.customControl112.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(255)))));
            this.customControl112.Blink = false;
            this.customControl112.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl112.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl112.BorderWidth = 0;
            this.customControl112.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl112.Direction = ShapeControl.LineDirection.None;
            this.customControl112.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl112.Location = new System.Drawing.Point(762, 104);
            this.customControl112.Name = "customControl112";
            this.customControl112.Shape = ShapeControl.ShapeType.Rectangle;
            this.customControl112.ShapeImage = ((System.Drawing.Image)(resources.GetObject("customControl112.ShapeImage")));
            this.customControl112.Size = new System.Drawing.Size(67, 63);
            this.customControl112.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl112.TabIndex = 20;
            this.customControl112.Tag2 = "";
            this.customControl112.UseGradient = false;
            this.customControl112.Vibrate = false;
            // 
            // customControl111
            // 
            this.customControl111.AnimateBorder = false;
            this.customControl111.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl111.Blink = false;
            this.customControl111.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl111.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl111.BorderWidth = 0;
            this.customControl111.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(164)))), ((int)(((byte)(232)))), ((int)(((byte)(0)))));
            this.customControl111.Direction = ShapeControl.LineDirection.None;
            this.customControl111.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl111.Location = new System.Drawing.Point(762, 173);
            this.customControl111.Name = "customControl111";
            this.customControl111.Shape = ShapeControl.ShapeType.Rectangle;
            this.customControl111.ShapeImage = ((System.Drawing.Image)(resources.GetObject("customControl111.ShapeImage")));
            this.customControl111.Size = new System.Drawing.Size(67, 56);
            this.customControl111.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(0)))));
            this.customControl111.TabIndex = 19;
            this.customControl111.Tag2 = "";
            this.customControl111.UseGradient = true;
            this.customControl111.Vibrate = false;
            // 
            // customControl110
            // 
            this.customControl110.AnimateBorder = false;
            this.customControl110.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl110.Blink = false;
            this.customControl110.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl110.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl110.BorderWidth = 0;
            this.customControl110.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(164)))), ((int)(((byte)(232)))), ((int)(((byte)(0)))));
            this.customControl110.Direction = ShapeControl.LineDirection.None;
            this.customControl110.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl110.Location = new System.Drawing.Point(905, 172);
            this.customControl110.Name = "customControl110";
            this.customControl110.Shape = ShapeControl.ShapeType.Rectangle;
            this.customControl110.ShapeImage = ((System.Drawing.Image)(resources.GetObject("customControl110.ShapeImage")));
            this.customControl110.Size = new System.Drawing.Size(67, 56);
            this.customControl110.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(0)))));
            this.customControl110.TabIndex = 18;
            this.customControl110.Tag2 = "";
            this.customControl110.UseGradient = true;
            this.customControl110.Vibrate = false;
            // 
            // customControl19
            // 
            this.customControl19.AnimateBorder = false;
            this.customControl19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl19.Blink = false;
            this.customControl19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl19.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl19.BorderWidth = 0;
            this.customControl19.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(164)))), ((int)(((byte)(232)))), ((int)(((byte)(0)))));
            this.customControl19.Direction = ShapeControl.LineDirection.None;
            this.customControl19.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl19.Location = new System.Drawing.Point(830, 172);
            this.customControl19.Name = "customControl19";
            this.customControl19.Shape = ShapeControl.ShapeType.Rectangle;
            this.customControl19.ShapeImage = ((System.Drawing.Image)(resources.GetObject("customControl19.ShapeImage")));
            this.customControl19.Size = new System.Drawing.Size(68, 56);
            this.customControl19.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(61)))), ((int)(((byte)(0)))));
            this.customControl19.TabIndex = 17;
            this.customControl19.Tag2 = "";
            this.customControl19.UseGradient = true;
            this.customControl19.Vibrate = false;
            // 
            // customControl18
            // 
            this.customControl18.AnimateBorder = false;
            this.customControl18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl18.Blink = false;
            this.customControl18.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl18.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl18.BorderWidth = 8;
            this.customControl18.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl18.Direction = ShapeControl.LineDirection.RightDown;
            this.customControl18.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl18.Location = new System.Drawing.Point(640, 122);
            this.customControl18.Name = "customControl18";
            this.customControl18.Shape = ShapeControl.ShapeType.LineVertical;
            this.customControl18.ShapeImage = null;
            this.customControl18.Size = new System.Drawing.Size(24, 107);
            this.customControl18.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl18.TabIndex = 16;
            this.customControl18.Tag2 = "";
            this.customControl18.Text = "customControl18";
            this.customControl18.UseGradient = false;
            this.customControl18.Vibrate = false;
            // 
            // customControl17
            // 
            this.customControl17.AnimateBorder = false;
            this.customControl17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl17.Blink = false;
            this.customControl17.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl17.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl17.BorderWidth = 8;
            this.customControl17.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl17.Direction = ShapeControl.LineDirection.RightDown;
            this.customControl17.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl17.Location = new System.Drawing.Point(646, 226);
            this.customControl17.Name = "customControl17";
            this.customControl17.Shape = ShapeControl.ShapeType.LineDown;
            this.customControl17.ShapeImage = null;
            this.customControl17.Size = new System.Drawing.Size(169, 93);
            this.customControl17.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl17.TabIndex = 15;
            this.customControl17.Tag2 = "";
            this.customControl17.Text = "customControl17";
            this.customControl17.UseGradient = false;
            this.customControl17.Vibrate = false;
            // 
            // customControl16
            // 
            this.customControl16.AnimateBorder = false;
            this.customControl16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl16.Blink = false;
            this.customControl16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl16.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl16.BorderWidth = 8;
            this.customControl16.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl16.Direction = ShapeControl.LineDirection.LeftUp;
            this.customControl16.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl16.Location = new System.Drawing.Point(640, 58);
            this.customControl16.Name = "customControl16";
            this.customControl16.Shape = ShapeControl.ShapeType.LineHorizontal;
            this.customControl16.ShapeImage = null;
            this.customControl16.Size = new System.Drawing.Size(175, 20);
            this.customControl16.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl16.TabIndex = 14;
            this.customControl16.Tag2 = "";
            this.customControl16.Text = "customControl16";
            this.customControl16.UseGradient = false;
            this.customControl16.Vibrate = false;
            // 
            // customControl15
            // 
            this.customControl15.AnimateBorder = false;
            this.customControl15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl15.Blink = false;
            this.customControl15.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(213)))), ((int)(((byte)(251)))));
            this.customControl15.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl15.BorderWidth = 4;
            this.customControl15.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl15.Direction = ShapeControl.LineDirection.None;
            this.customControl15.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl15.Location = new System.Drawing.Point(884, 127);
            this.customControl15.Name = "customControl15";
            this.customControl15.Shape = ShapeControl.ShapeType.LineVertical;
            this.customControl15.ShapeImage = null;
            this.customControl15.Size = new System.Drawing.Size(24, 144);
            this.customControl15.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl15.TabIndex = 13;
            this.customControl15.Tag2 = "";
            this.customControl15.Text = "customControl15";
            this.customControl15.UseGradient = false;
            this.customControl15.Vibrate = false;
            // 
            // customControl14
            // 
            this.customControl14.AnimateBorder = false;
            this.customControl14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl14.Blink = false;
            this.customControl14.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(213)))), ((int)(((byte)(251)))));
            this.customControl14.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl14.BorderWidth = 4;
            this.customControl14.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl14.Direction = ShapeControl.LineDirection.None;
            this.customControl14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl14.Location = new System.Drawing.Point(774, 226);
            this.customControl14.Name = "customControl14";
            this.customControl14.Shape = ShapeControl.ShapeType.LineHorizontal;
            this.customControl14.ShapeImage = null;
            this.customControl14.Size = new System.Drawing.Size(169, 11);
            this.customControl14.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl14.TabIndex = 12;
            this.customControl14.Tag2 = "";
            this.customControl14.Text = "customControl14";
            this.customControl14.UseGradient = false;
            this.customControl14.Vibrate = false;
            // 
            // customControl13
            // 
            this.customControl13.AnimateBorder = false;
            this.customControl13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl13.Blink = false;
            this.customControl13.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(213)))), ((int)(((byte)(251)))));
            this.customControl13.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl13.BorderWidth = 4;
            this.customControl13.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl13.Direction = ShapeControl.LineDirection.None;
            this.customControl13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl13.Location = new System.Drawing.Point(822, 102);
            this.customControl13.Name = "customControl13";
            this.customControl13.Shape = ShapeControl.ShapeType.LineUp;
            this.customControl13.ShapeImage = null;
            this.customControl13.Size = new System.Drawing.Size(24, 170);
            this.customControl13.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl13.TabIndex = 11;
            this.customControl13.Tag2 = "";
            this.customControl13.Text = "customControl13";
            this.customControl13.UseGradient = false;
            this.customControl13.Vibrate = false;
            // 
            // customControl12
            // 
            this.customControl12.AnimateBorder = false;
            this.customControl12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl12.Blink = false;
            this.customControl12.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(213)))), ((int)(((byte)(251)))));
            this.customControl12.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl12.BorderWidth = 4;
            this.customControl12.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl12.Direction = ShapeControl.LineDirection.None;
            this.customControl12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl12.Location = new System.Drawing.Point(762, 154);
            this.customControl12.Name = "customControl12";
            this.customControl12.Shape = ShapeControl.ShapeType.LineUp;
            this.customControl12.ShapeImage = null;
            this.customControl12.Size = new System.Drawing.Size(180, 27);
            this.customControl12.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl12.TabIndex = 10;
            this.customControl12.Tag2 = "";
            this.customControl12.Text = "customControl12";
            this.customControl12.UseGradient = false;
            this.customControl12.Vibrate = false;
            // 
            // customControl11
            // 
            this.customControl11.AnimateBorder = false;
            this.customControl11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.customControl11.Blink = false;
            this.customControl11.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl11.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.customControl11.BorderWidth = 0;
            this.customControl11.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.customControl11.Direction = ShapeControl.LineDirection.None;
            this.customControl11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.customControl11.Location = new System.Drawing.Point(478, 127);
            this.customControl11.Name = "customControl11";
            this.customControl11.Shape = ShapeControl.ShapeType.Ellipse;
            this.customControl11.ShapeImage = null;
            this.customControl11.Size = new System.Drawing.Size(111, 99);
            this.customControl11.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(11)))));
            this.customControl11.TabIndex = 9;
            this.customControl11.Tag2 = "";
            this.customControl11.UseGradient = true;
            this.customControl11.Vibrate = false;
            // 
            // shapeControl5
            // 
            this.shapeControl5.AnimateBorder = false;
            this.shapeControl5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(227)))), ((int)(((byte)(251)))), ((int)(((byte)(72)))));
            this.shapeControl5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shapeControl5.Blink = false;
            this.shapeControl5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl5.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.shapeControl5.BorderWidth = 2;
            this.shapeControl5.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl5.Direction = ShapeControl.LineDirection.None;
            this.shapeControl5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shapeControl5.Location = new System.Drawing.Point(311, 13);
            this.shapeControl5.Name = "shapeControl5";
            this.shapeControl5.Shape = ShapeControl.ShapeType.BallonSE;
            this.shapeControl5.ShapeImage = null;
            this.shapeControl5.Size = new System.Drawing.Size(173, 114);
            this.shapeControl5.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.shapeControl5.TabIndex = 6;
            this.shapeControl5.Tag2 = "";
            this.shapeControl5.Text = "Hello, I am Tommy";
            this.shapeControl5.UseGradient = false;
            this.shapeControl5.Vibrate = false;
            this.shapeControl5.Visible = false;
            // 
            // shapeControl7
            // 
            this.shapeControl7.AnimateBorder = false;
            this.shapeControl7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.shapeControl7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shapeControl7.Blink = false;
            this.shapeControl7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl7.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl7.BorderWidth = 1;
            this.shapeControl7.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl7.Direction = ShapeControl.LineDirection.None;
            this.shapeControl7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl7.Location = new System.Drawing.Point(514, 87);
            this.shapeControl7.Name = "shapeControl7";
            this.shapeControl7.Shape = ShapeControl.ShapeType.CustomPolygon;
            this.shapeControl7.ShapeImage = null;
            this.shapeControl7.Size = new System.Drawing.Size(27, 24);
            this.shapeControl7.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl7.TabIndex = 8;
            this.shapeControl7.Tag2 = "";
            this.shapeControl7.UseGradient = false;
            this.shapeControl7.Vibrate = false;
            // 
            // shapeControl4
            // 
            this.shapeControl4.AnimateBorder = false;
            this.shapeControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(92)))), ((int)(((byte)(159)))), ((int)(((byte)(83)))));
            this.shapeControl4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shapeControl4.Blink = false;
            this.shapeControl4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(131)))), ((int)(((byte)(255)))), ((int)(((byte)(4)))));
            this.shapeControl4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl4.BorderWidth = 3;
            this.shapeControl4.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl4.Direction = ShapeControl.LineDirection.None;
            this.shapeControl4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl4.Location = new System.Drawing.Point(473, 253);
            this.shapeControl4.Name = "shapeControl4";
            this.shapeControl4.Shape = ShapeControl.ShapeType.RoundedRectangle;
            this.shapeControl4.ShapeImage = null;
            this.shapeControl4.Size = new System.Drawing.Size(116, 38);
            this.shapeControl4.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.shapeControl4.TabIndex = 5;
            this.shapeControl4.Tag2 = "";
            this.shapeControl4.Text = "Click Me!";
            this.shapeControl4.UseGradient = false;
            this.shapeControl4.Vibrate = false;
            this.shapeControl4.Click += new System.EventHandler(this.shapeControl4_Click);
            // 
            // shapeControl3
            // 
            this.shapeControl3.AnimateBorder = false;
            this.shapeControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(92)))), ((int)(((byte)(159)))), ((int)(((byte)(83)))));
            this.shapeControl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shapeControl3.Blink = false;
            this.shapeControl3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(131)))), ((int)(((byte)(255)))), ((int)(((byte)(4)))));
            this.shapeControl3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl3.BorderWidth = 3;
            this.shapeControl3.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl3.Direction = ShapeControl.LineDirection.None;
            this.shapeControl3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl3.Location = new System.Drawing.Point(473, 309);
            this.shapeControl3.Name = "shapeControl3";
            this.shapeControl3.Shape = ShapeControl.ShapeType.RoundedRectangle;
            this.shapeControl3.ShapeImage = null;
            this.shapeControl3.Size = new System.Drawing.Size(116, 38);
            this.shapeControl3.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.shapeControl3.TabIndex = 4;
            this.shapeControl3.Tag2 = "";
            this.shapeControl3.Text = "Close";
            this.shapeControl3.UseGradient = false;
            this.shapeControl3.Vibrate = false;
            this.shapeControl3.Click += new System.EventHandler(this.shapeControl3_Click);
            // 
            // shapeControl2
            // 
            this.shapeControl2.AnimateBorder = false;
            this.shapeControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shapeControl2.Blink = false;
            this.shapeControl2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(133)))), ((int)(((byte)(4)))), ((int)(((byte)(9)))));
            this.shapeControl2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl2.BorderWidth = 0;
            this.shapeControl2.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(163)))), ((int)(((byte)(126)))), ((int)(((byte)(59)))));
            this.shapeControl2.Direction = ShapeControl.LineDirection.None;
            this.shapeControl2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.shapeControl2.Location = new System.Drawing.Point(434, 76);
            this.shapeControl2.Name = "shapeControl2";
            this.shapeControl2.Shape = ShapeControl.ShapeType.TriangleUp;
            this.shapeControl2.ShapeImage = null;
            this.shapeControl2.Size = new System.Drawing.Size(183, 35);
            this.shapeControl2.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.shapeControl2.TabIndex = 3;
            this.shapeControl2.Tag2 = "";
            this.shapeControl2.UseGradient = true;
            this.shapeControl2.Vibrate = false;
            // 
            // shapeControl1
            // 
            this.shapeControl1.AnimateBorder = false;
            this.shapeControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.shapeControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shapeControl1.Blink = false;
            this.shapeControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.shapeControl1.BorderWidth = 0;
            this.shapeControl1.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.shapeControl1.Direction = ShapeControl.LineDirection.None;
            this.shapeControl1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shapeControl1.ForeColor = System.Drawing.Color.Yellow;
            this.shapeControl1.Location = new System.Drawing.Point(38, 39);
            this.shapeControl1.Name = "shapeControl1";
            this.shapeControl1.Shape = ShapeControl.ShapeType.Diamond;
            this.shapeControl1.ShapeImage = null;
            this.shapeControl1.Size = new System.Drawing.Size(143, 159);
            this.shapeControl1.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.shapeControl1.TabIndex = 0;
            this.shapeControl1.Tag2 = "";
            this.shapeControl1.Text = "Transparency Test Drag Me Around";
            this.shapeControl1.UseGradient = true;
            this.shapeControl1.Vibrate = false;
            this.shapeControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shapeControl1_MouseDown);
            this.shapeControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.shapeControl1_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(815, 372);
            this.ControlBox = false;
            this.Controls.Add(this.customControl113);
            this.Controls.Add(this.shapeControl6);
            this.Controls.Add(this.customControl112);
            this.Controls.Add(this.customControl111);
            this.Controls.Add(this.customControl110);
            this.Controls.Add(this.customControl19);
            this.Controls.Add(this.customControl18);
            this.Controls.Add(this.customControl17);
            this.Controls.Add(this.customControl16);
            this.Controls.Add(this.customControl15);
            this.Controls.Add(this.customControl14);
            this.Controls.Add(this.customControl13);
            this.Controls.Add(this.customControl12);
            this.Controls.Add(this.customControl11);
            this.Controls.Add(this.shapeControl5);
            this.Controls.Add(this.shapeControl7);
            this.Controls.Add(this.shapeControl4);
            this.Controls.Add(this.shapeControl3);
            this.Controls.Add(this.shapeControl2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void shapeControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button.Equals(MouseButtons.Left))
			{
				sx=e.X;
				sy=e.Y;
                //tempregion = ((ShapeControl.CustomControl1)sender).Region.Clone();
                //((ShapeControl.CustomControl1)sender).Region = null;
               
			}
		}

		private void shapeControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button.Equals(MouseButtons.Left))
			{
				((Control) sender).Left =((Control) sender).Left+(e.X -sx);
				((Control) sender).Top =((Control) sender).Top+(e.Y -sy);
              
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            this.DoubleBuffered = true;
            typeof(Panel).InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               panel1,
               new object[] { true });		
		}

		private void shapeControl4_Click(object sender, System.EventArgs e)
		{
			shapeControl5.Visible=true;
            customControl11.AnimateBorder = true;
            customControl16.AnimateBorder = true;
            customControl17.AnimateBorder = true;
            customControl18.AnimateBorder = true;
			this.Refresh();
         
            
            long t1 = DateTime.Now.Ticks +20000000;
            while (DateTime.Now.Ticks< t1)
            {
                Application.DoEvents();
            }

            customControl11.AnimateBorder = false;
            customControl16.AnimateBorder = false;
            customControl17.AnimateBorder = false;
            customControl18.AnimateBorder = false;
            shapeControl5.Visible=false;

			this.Refresh();

		}

		private void shapeControl3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}




	}
}
