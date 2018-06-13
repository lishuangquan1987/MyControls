using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Threading;

namespace ucCodeEditor
{
    /// <summary>
    /// Console emulator.
    /// </summary>
    public class ConsoleTextBox : textEditor
    {
        private volatile bool isReadLineMode;
        private volatile bool isUpdating;
        private Place StartReadPlace { get; set; }
        /// <summary>
        /// Control is waiting for line entering. 
        /// </summary>
        public bool IsReadLineMode
        {
            get { return isReadLineMode; }
            set { isReadLineMode = value; }
        }

        /// <summary>
        /// Append line to end of text.
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            IsReadLineMode = false;
            isUpdating = true;
            try
            {
                AppendText(text);
                GoEnd();
            }
            finally
            {
                isUpdating = false;
                ClearUndo();
            }
        }

        /// <summary>
        /// Wait for line entering.
        /// Set IsReadLineMode to false for break of waiting.
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            GoEnd();
            StartReadPlace = Range.End;
            IsReadLineMode = true;
            try
            {
                while (IsReadLineMode)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
            }
            finally
            {
                IsReadLineMode = false;
                ClearUndo();
            }

            return new Range(this, StartReadPlace, Range.End).Text.TrimEnd('\r', '\n');
        }

        public override void OnTextChanging(ref string text)
        {
            if (!IsReadLineMode && !isUpdating)
            {
                text = ""; //cancel changing
                return;
            }

            if (IsReadLineMode)
            {
                if (Selection.Start < StartReadPlace || Selection.End < StartReadPlace)
                    GoEnd();//move caret to entering position

                if (Selection.Start == StartReadPlace || Selection.End == StartReadPlace)
                    if (text == "\b") //backspace
                    {
                        text = ""; //cancel deleting of last char of readonly text
                        return;
                    }

                if (text != null && text.Contains('\n'))
                {
                    text = text.Substring(0, text.IndexOf('\n') + 1);
                    IsReadLineMode = false;
                }
            }

            base.OnTextChanging(ref text);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ConsoleTextBox
            // 
            this.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "ConsoleTextBox";
            this.Size = new System.Drawing.Size(179, 150);
            this.ResumeLayout(false);

        }

    }
}
