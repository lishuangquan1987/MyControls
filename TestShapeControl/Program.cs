 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestShapeControl
{       
 static public class Program
 {
         [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormSwitch f2 = new FormSwitch();
            //MultiCamForm f1 = new MultiCamForm();
            Application.Run(f2);


        }
 }
 }