using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;



namespace ucCodeEditor
{
    class CommandRun
    {
        public CommandRun(ucLuaEditor Tmpform)
        {
            this.Tmpform = Tmpform;
        }
        ucLuaEditor Tmpform;
        //处理Csc命令
        public string RunCsc(string cscCommand)
        {
            cscCommand = cscCommand.ToLower();
            Regex reg = new Regex(@"\s+");
            string[] strArrys = reg.Split(cscCommand);

            ComplieBuilder cp = new ComplieBuilder();
            string srcStr = Tmpform.txtEditor.Text;
            string comm = "";
            ComplieType cpType = ComplieType.exe;
            bool isWindow = false;

            //
            foreach (var i in strArrys)
            {
                if (i == "?" || i == "help")
                {
                    return "CSC 编译帮助\n" +
                           "exe        生成普通exe程序\n" +
                           "winexe     生成窗体exe程序\n" +
                           "dll        生成类库程序\n" +
                           "wd         使用窗口启动(普通exe程序有效)\n" +
                           "arg:hello  设置参数为hello(普通exe程序有效,不能出现符号'∫',要输出空格：\\s)\n";

                }
                if (i == "show")
                {
                    Tmpform.AddCsc();
                    if (Tmpform.isShow)
                        Tmpform.StartMeun();
                    return "";
                }
                switch (i)
                {
                    case "exe":
                        cpType = ComplieType.exe;
                        break;
                    case "winexe":
                        cpType = ComplieType.winexe;
                        break;
                    case "dll":
                        cpType = ComplieType.dll;
                        break;
                    case "wd":
                        isWindow = true;
                        break;
                }
                if (i.StartsWith("arg:"))
                {
                    string tmp = i;
                    tmp = tmp.Replace(@"\\", @"\");
                    tmp = tmp.Replace(@"\s", " ");
                    comm += tmp.Replace("arg:", "∫");
                }

            }
            CompliReslut.OutPutStr = cp.Creat(srcStr, comm, cpType, isWindow);
            CompliReslut.isError = cp.isError;
            CompliReslut.isHasErrorRow = cp.isHasErrorRows;
            CompliReslut.RowErrorNumber = cp.RowErrorNumber;
            ErrorMake();
            return CompliReslut.OutPutStr;
        }

        private void ErrorMake()
        {
            if (CompliReslut.isError && CompliReslut.isHasErrorRow && CompliReslut.RowErrorNumber != -1)
            {
                Tmpform.txtEditor[CompliReslut.RowErrorNumber - 1].BackgroundBrush = CommConfig.ErrorMake;
                Tmpform.txtEditor.Invalidate();
            }
        }
    }
}
