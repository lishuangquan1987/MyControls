using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;


namespace ucCodeEditor
{
    class ComplieBuilder
    {

        public string SrcString;
        public string Command;
        public ComplieType cpType=ComplieType.exe;
        public bool isWindow = false;
        public int RowErrorNumber { get; private set; }
        public bool isHasErrorRows { get; private set; }
        public bool isError { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcString"></param>
        /// <param name="Command"></param>
        /// <param name="cpType">= ComplieType.exe</param>
        /// <param name="isStartByWindow">=false</param>
        /// <returns></returns>
        public string Creat(string srcString, string Command, ComplieType cpType, bool isStartByWindow)
        {
            this.SrcString = srcString;
            this.Command = Command;
            this.cpType = cpType;
            this.isWindow = isStartByWindow;
            this.isHasErrorRows = false;
            this.RowErrorNumber = -1;
            isError = false;
            CreatFile();//
            StringBuilder sbr = new StringBuilder();
            sbr.AppendLine(runStringCSC(MakeCommond()));//编译
            if (File.Exists(CommConfig.ExeFilePath) || File.Exists(CommConfig.DllFilePath))
            {
                sbr = new StringBuilder();
                sbr.AppendLine("-------编译成功--------------------------------");
                if (cpType == ComplieType.exe || cpType == ComplieType.winexe)//测试*.exe文件
                {
                    if (isWindow || cpType == ComplieType.winexe)//判断是否使用CMD
                    {
                        System.Diagnostics.Process.Start(CommConfig.ExeFilePath);
                    }
                    else
                    {
                        sbr.AppendLine("\n" + runStringTemp(this.Command));
                    }
                }
            }
            else
            {
                isError = true;
                sbr.AppendLine("-------编译失败--------------------------------");

                try
                {
                    Regex reg = new Regex(@"\((\d+)\,\d+\)", RegexOptions.Multiline);
                    Match m = reg.Match(sbr.ToString());
                    
                    if (m.Success)
                    {
                        isHasErrorRows = true;
                        RowErrorNumber = int.Parse(m.Groups[1].Value);
                    }
                }
                catch (Exception e)
                {
                    isHasErrorRows = false;
                    RowErrorNumber = -1;
                }
            }

            return sbr.ToString();
        }

        //构建命令
        private string MakeCommond()
        {
            StringBuilder sb = new StringBuilder();
            switch (cpType)
            {
                case ComplieType.exe:
                    sb.Append("/target:exe /out:"+ CommConfig.ExeFilePath + " " + CommConfig.SrcFilePath);//普通exe
                    break;
                case ComplieType.winexe:
                    sb.Append("/target:winexe /out:"+ CommConfig.ExeFilePath + " " + CommConfig.SrcFilePath);//Winexe
                    break;
                case ComplieType.dll:
                    sb.Append("/target:library /out:"+ CommConfig.DllFilePath + " " + CommConfig.SrcFilePath);//dll
                    break;
                default:
                    throw new Exception("无效的输入!");
            }
            return sb.ToString();
        }
        //测试生成文件
        private string runStringTemp(string commond)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = CommConfig.ExeFilePath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string[] sp = commond.Split('∫');
            foreach (string i in sp)
            {
                if (string.IsNullOrEmpty(i))
                    continue;
                p.StandardInput.WriteLine(i);
            }
            // p.StandardInput.WriteLine(commond);
            return p.StandardOutput.ReadToEnd();

        }
        //编译函数
        private string runStringCSC(string commond)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = CommConfig.CscPath + "\\csc.exe";
            p.StartInfo.Arguments = commond;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(commond);
            return p.StandardOutput.ReadToEnd();

        }
        //生成文件
        private void CreatFile()
        {
            if (Directory.Exists(CommConfig.BinPath))
                Directory.Delete(CommConfig.BinPath, true);
            Directory.CreateDirectory(CommConfig.BinPath);
            Thread.Sleep(500);
            StreamWriter strCode = new StreamWriter(File.OpenWrite(CommConfig.SrcFilePath), System.Text.Encoding.Default);
            strCode.WriteLine(SrcString);
            strCode.Close();
        }
    }
}
