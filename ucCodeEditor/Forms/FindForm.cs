using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace ucCodeEditor
{
    public partial class FindForm : Form
    {

        bool firstSearch = true;
        Place startPlace;
        textEditor tb;

        public FindForm()
        {
            InitializeComponent();
        }

        public FindForm(textEditor te)
        {
            InitializeComponent();
            this.tb = te;
        }


        public void FindNext(string pattern)
        {
            try
            {
                RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                Range range = tb.Selection.Clone();
                range.Normalize();
                //
                if (firstSearch)
                {
                    startPlace = range.Start;
                    firstSearch = false;
                }
                //
                range.Start = range.End;
                if (range.Start >= startPlace)
                    range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                else
                    range.End = startPlace;
                //
                foreach (var r in range.GetRangesByLines(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return;
                }
                //
                if (range.Start >= startPlace && startPlace > Place.Empty)
                {
                    tb.Selection.Start = new Place(0, 0);
                    FindNext(pattern);
                    return;
                }
                MessageBox.Show("已经到文本末尾！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
