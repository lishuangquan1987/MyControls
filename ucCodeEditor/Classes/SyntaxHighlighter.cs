using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

namespace ucCodeEditor
{
    public class SyntaxHighlighter : IDisposable
    {

        //styles for twzy
        public readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public readonly Style BlueBoldStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        public readonly Style BoldStyle = new TextStyle(Brushes.Orange, null, FontStyle.Bold);
        public readonly Style GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        public readonly Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);

        public readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        public readonly Style BrownStyle = new TextStyle(Brushes.Firebrick, null, FontStyle.Regular);
        public readonly Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        public readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);

        //
        Dictionary<string, SyntaxDescriptor> descByXMLfileNames = new Dictionary<string, SyntaxDescriptor>();
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

        /// <summary>
        /// Highlights syntax for given language
        /// </summary>
        public virtual void HighlightSyntax(Language language, Range range)
        {
            switch (language)
            {
                case Language.CSharp: CSharpSyntaxHighlight(range); break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Highlights syntax for given XML description file
        /// </summary>
        public virtual void HighlightSyntax(string XMLdescriptionFile, Range range)
        {
            SyntaxDescriptor desc = null;
            if (!descByXMLfileNames.TryGetValue(XMLdescriptionFile, out desc))
            {
                var doc = new XmlDocument();
                string file = XMLdescriptionFile;
                if (!File.Exists(file))
                    file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(file));

                doc.LoadXml(File.ReadAllText(file));
                desc = ParseXmlDescription(doc);
                descByXMLfileNames[XMLdescriptionFile] = desc;
            }
            HighlightSyntax(desc, range);
        }

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            textEditor tb = (sender as textEditor);
            Language language = tb.Language;
            switch (language)
            {
                case Language.CSharp: CSharpAutoIndentNeeded(sender, args); break;
                default:
                    break;
            }
        }

        private void CSharpAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        public static SyntaxDescriptor ParseXmlDescription(XmlDocument doc)
        {
            SyntaxDescriptor desc = new SyntaxDescriptor();
            XmlNode brackets = doc.SelectSingleNode("doc/brackets");
            if (brackets != null)
            {
                if (brackets.Attributes["left"] == null || brackets.Attributes["right"] == null ||
                    brackets.Attributes["left"].Value == "" || brackets.Attributes["right"].Value == "")
                {
                    desc.leftBracket = '\x0';
                    desc.rightBracket = '\x0';
                }
                else
                {
                    desc.leftBracket = brackets.Attributes["left"].Value[0];
                    desc.rightBracket = brackets.Attributes["right"].Value[0];
                }

                if (brackets.Attributes["left2"] == null || brackets.Attributes["right2"] == null ||
    brackets.Attributes["left2"].Value == "" || brackets.Attributes["right2"].Value == "")
                {
                    desc.leftBracket2 = '\x0';
                    desc.rightBracket2 = '\x0';
                }
                else
                {
                    desc.leftBracket2 = brackets.Attributes["left2"].Value[0];
                    desc.rightBracket2 = brackets.Attributes["right2"].Value[0];
                }
            }

            Dictionary<string, Style> styleByName = new Dictionary<string, Style>();

            foreach (XmlNode style in doc.SelectNodes("doc/style"))
            {
                var s = ParseStyle(style);
                styleByName[style.Attributes["name"].Value] = s;
                desc.styles.Add(s);
            }
            foreach (XmlNode rule in doc.SelectNodes("doc/rule"))
                desc.rules.Add(ParseRule(rule, styleByName));
            foreach (XmlNode folding in doc.SelectNodes("doc/folding"))
                desc.foldings.Add(ParseFolding(folding));

            return desc;
        }

        private static FoldingDesc ParseFolding(XmlNode foldingNode)
        {
            FoldingDesc folding = new FoldingDesc();
            //regex
            folding.startMarkerRegex = foldingNode.Attributes["start"].Value;
            folding.finishMarkerRegex = foldingNode.Attributes["finish"].Value;
            //options
            var optionsA = foldingNode.Attributes["options"];
            if (optionsA != null)
                folding.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), optionsA.Value);

            return folding;
        }

        private static RuleDesc ParseRule(XmlNode ruleNode, Dictionary<string, Style> styles)
        {
            RuleDesc rule = new RuleDesc();
            rule.pattern = ruleNode.InnerText;
            //
            var styleA = ruleNode.Attributes["style"];
            var optionsA = ruleNode.Attributes["options"];
            //Style
            if (styleA == null)
                throw new Exception("Rule must contain style name.");
            if (!styles.ContainsKey(styleA.Value))
                throw new Exception("Style '" + styleA.Value + "' is not found.");
            rule.style = styles[styleA.Value];
            //options
            if (optionsA != null)
                rule.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), optionsA.Value);

            return rule;
        }

        private static Style ParseStyle(XmlNode styleNode)
        {
            var typeA = styleNode.Attributes["type"];
            var colorA = styleNode.Attributes["color"];
            var backColorA = styleNode.Attributes["backColor"];
            var fontStyleA = styleNode.Attributes["fontStyle"];
            var nameA = styleNode.Attributes["name"];
            //colors
            SolidBrush foreBrush = null;
            if (colorA != null)
                foreBrush = new SolidBrush(ParseColor(colorA.Value));
            SolidBrush backBrush = null;
            if (backColorA != null)
                backBrush = new SolidBrush(ParseColor(backColorA.Value));
            //fontStyle
            FontStyle fontStyle = FontStyle.Regular;
            if (fontStyleA != null)
                fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), fontStyleA.Value);

            return new TextStyle(foreBrush, backBrush, fontStyle);
        }

        private static Color ParseColor(string s)
        {
            if (s.StartsWith("#"))
            {
                if (s.Length <= 7)
                    return Color.FromArgb(255, Color.FromArgb(Int32.Parse(s.Substring(1), System.Globalization.NumberStyles.AllowHexSpecifier)));
                else
                    return Color.FromArgb(Int32.Parse(s.Substring(1), System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            else
                return Color.FromName(s);
        }

        public void HighlightSyntax(SyntaxDescriptor desc, Range range)
        {
            //set style order
            range.tb.ClearStylesBuffer();
            for (int i = 0; i < desc.styles.Count; i++)
                range.tb.Styles[i] = desc.styles[i];
            //brackets
            range.tb.LeftBracket = desc.leftBracket;
            range.tb.RightBracket = desc.rightBracket;
            range.tb.LeftBracket2 = desc.leftBracket2;
            range.tb.RightBracket2 = desc.rightBracket2;
            //clear styles of range
            range.ClearStyle(desc.styles.ToArray());
            //highlight syntax
            foreach (var rule in desc.rules)
                range.SetStyle(rule.style, rule.Regex);
            //clear folding
            range.ClearFoldingMarkers();
            //folding markers
            foreach (var folding in desc.foldings)
                range.SetFoldingMarkers(folding.startMarkerRegex, folding.finishMarkerRegex, folding.options);
        }

        Regex CSharpStringRegex, 
            CSharpCommentRegex1, 
            CSharpCommentRegex2, 
            CSharpCommentRegex3, 
            CSharpNumberRegex, 
            CSharpAttributeRegex, 
            CSharpClassNameRegex, 
            CSharpKeywordRegex;

        void InitCShaprRegex()
        {
            CSharpStringRegex = new Regex(@"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'", RegexCompiledOption);
            CSharpCommentRegex1 = new Regex(@"--.*$", RegexOptions.Multiline | RegexCompiledOption);

            //CSharpCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            //CSharpCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);

            CSharpCommentRegex2 = new Regex(@"(/--[[.*?\--]])|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            CSharpCommentRegex3 = new Regex(@"(/--[[.*?\--]])|(.*\--\]\])", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);

            CSharpNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b", RegexCompiledOption);
            CSharpAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            CSharpClassNameRegex = new Regex(@"\b(class)\s+(?<range>\w+?)\b", RegexCompiledOption);
            CSharpKeywordRegex = new Regex(@"\b(do|elseif|else|false|for\b|goto|if|is\b|null|operator|params|return|string|switch|true|
                               between|while|and|var\b|function|local|require|package|end|then|#region\b|#endregion\b)", RegexCompiledOption);

        }
        /// <summary>
        /// Highlights C# code
        /// </summary>
        /// <param name="range"></param>
        public virtual void CSharpSyntaxHighlight(Range range)
        {
            range.tb.CommentPrefix = "--";
            range.tb.LeftBracket = '(';
            range.tb.RightBracket = ')';
            range.tb.LeftBracket2 = '\x0';
            range.tb.RightBracket2 = '\x0';
            //clear style of changed range
            range.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);
            //
            if (CSharpStringRegex == null)
                InitCShaprRegex();
            //string highlighting
            range.SetStyle(BrownStyle, CSharpStringRegex);
            //comment highlighting
            range.SetStyle(GreenStyle, CSharpCommentRegex1);
            range.SetStyle(GreenStyle, CSharpCommentRegex2);
            range.SetStyle(GreenStyle, CSharpCommentRegex3);
            //number highlighting
            range.SetStyle(MagentaStyle, CSharpNumberRegex);
            //attribute highlighting
            range.SetStyle(GreenStyle, CSharpAttributeRegex);
            //class name highlighting
            range.SetStyle(BoldStyle, CSharpClassNameRegex);
            //keyword highlighting
            range.SetStyle(BlueStyle, CSharpKeywordRegex);


            //find document comments
            foreach (var r in range.GetRanges(@"^\s*///.*$", RegexOptions.Multiline))
            {
                //remove C# highlighting from this fragment
                r.ClearStyle(StyleIndex.All);
                //do XML highlighting
                //if (HTMLTagRegex == null)
                //    InitHTMLRegex();
                //
                r.SetStyle(GreenStyle);
                //tags
                //foreach (var rr in r.GetRanges(HTMLTagContentRegex))
                //{
                //    rr.ClearStyle(StyleIndex.All);
                //    rr.SetStyle(GrayStyle);
                //}
                //prefix '///'
                foreach (var rr in r.GetRanges(@"^\s*///", RegexOptions.Multiline))
                {
                    rr.ClearStyle(StyleIndex.All);
                    rr.SetStyle(GrayStyle);
                }
            }

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            //range.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            range.SetFoldingMarkers(@"--\[\[", @"--\]\]");//allow to collapse --[[ --]] blocks
            
        }




        public void Dispose()
        {
            foreach (var desc in descByXMLfileNames.Values)
                desc.Dispose();
        }
    }

    /// <summary>
    /// Language
    /// </summary>
    public enum Language
    {
        Custom, CSharp
    }
}
