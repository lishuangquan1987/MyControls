using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Drawing;

namespace ucCodeEditor
{
    class CommConfig
    {
        public static string AppLocationPath;//需要设置


        public static Brush ErrorMake = Brushes.Red;
        public static MarkerStyle RedStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(180, Color.Red)));
        public static string ClassLibPath { get { return AppLocationPath + "\\ClassLib"; } }
        public static string BinPath { get { return AppLocationPath + "\\bin"; } }
        public static string ExeFilePath { get { return BinPath + "\\tmp.exe"; } }
        public static string DllFilePath { get { return BinPath + "\\tmp.dll"; } }
        public static string SrcFilePath { get { return BinPath + "\\tmp.cs"; } }
        public static string CscPath
        {
            get
            {
                return Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)) + @"\Microsoft.NET\Framework\v3.5";
            }
        }
        public static string fileName
        {
            get
            {
                StringBuilder sbr = new StringBuilder();
                sbr.Append(DateTime.Now.ToLocalTime().ToString("yyyyMMdd"));
                sbr.Append(DateTime.Now.Hour.ToString());
                sbr.Append(DateTime.Now.Minute.ToString());
                sbr.Append(DateTime.Now.Second.ToString());
                return ClassLibPath + "\\" + sbr.ToString() + ".cs";
            }
        }


    }
}
