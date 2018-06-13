using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace ucCodeEditor
{
    /// <summary>
    /// 自动提示输入
    /// </summary>
    class KeyWordsAuto
    {

        //static string[] keywords = { "function","local","abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
        //                              "checked", "class", "const", "continue", "decimal", "default", "delegate",
        //                              "do", "double", "else", "enum", "event", "explicit", "extern", "false", 
        //                              "finally", "fixed", "float", "for", "foreach", "goto", 
        //                              "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", 
        //                              "namespace", "new", "null", "object", "operator", "out", "override", "params", 
        //                              "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", 
        //                              "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this",
        //                              "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", 
        //                              "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", 
        //                              "orderby", "partial", "remove", "select", "set", "value", "var"};

        static string[] keywords = { "function","local","while","do","select","set","value","var","elseif","end","then"};

        static string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()", "Length", "Count" };

        static string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", 
                                      "for(int i=0;i<^;i++)\n{\n;\n}","foreach(var i in ^)\n{\n;\n}" ,"while(^)\n{\n;\n}", 
                                      "do${\n^;\n}while();"};
        static string[] declarationSnippets = { };


        static AutocompleteMenu popupMenu;
        static textEditor CurrentTB;
        public static void CreatMenu(textEditor fctb, ImageList imgList)
        {
            //create autocomplete popup menu
            popupMenu = new AutocompleteMenu(fctb);
            CurrentTB = fctb;
            popupMenu.Items.ImageList = imgList;
            popupMenu.Opening += new EventHandler<System.ComponentModel.CancelEventArgs>(popupMenu_Opening);
            BuildAutocompleteMenu();
        }

        static void popupMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //---block autocomplete menu for comments
            //get index of green style (used for comments)
            var iGreenStyle = CurrentTB.GetStyleIndex(CurrentTB.SyntaxHighlighter.GreenStyle);
            if (iGreenStyle >= 0)
                if (CurrentTB.Selection.Start.iChar > 0)
                {
                    //current char (before caret)
                    var c = CurrentTB[CurrentTB.Selection.Start.iLine][CurrentTB.Selection.Start.iChar - 1];
                    //green Style
                    var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
                    //if char contains green style then block popup menu
                    if ((c.style & greenStyleIndex) != 0)
                        e.Cancel = true;
                }
        }
        private static void BuildAutocompleteMenu()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 0 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 1 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item) { ImageIndex = 3 });


            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            popupMenu.Items.SetAutocompleteItems(items);
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
        }
        /// <summary>
        /// This item appears when any part of snippet text is typed
        /// </summary>
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Escape(fragmentText);
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }

        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m == null)
                    return fragment;
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            Place enterPlace = Place.Empty;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var r = Parent.Fragment.Clone();
                while (r.Start.iChar > 0)
                {
                    if (r.CharBeforeStart == '}')
                    {
                        enterPlace = r.Start;
                        return CompareResult.Visible;
                    }

                    r.GoLeftThroughFolded();
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                //extend range
                Range r = Parent.Fragment;
                Place end = r.End;
                r.Start = enterPlace;
                r.End = r.End;
                //insert line break
                return Environment.NewLine + r.Text;
            }

            public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
            {
                base.OnSelected(popupMenu, e);
                if (Parent.Fragment.tb.AutoIndent)
                    Parent.Fragment.tb.DoAutoIndent();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }
        static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        public static RegexOptions RegexCompiledOption
        {
            get
            {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                else
                    return RegexOptions.None;
            }
        }
    }
}
