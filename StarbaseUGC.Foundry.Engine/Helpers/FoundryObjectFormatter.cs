using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Helpers
{
    public static class FoundryObjectFormatter
    {
        private const string DialogStartChars = "<&";
        private const string DialogEndChars = "&>";
        private const string UnixNewLine = "\\n";

        /// <summary>
        /// Formats the regularly encountered strings that is injected into text
        /// 1. gets rid of <& and &> from the beginnings
        /// 2. gets rid of " from the beginning and ending
        /// 3. replaces \n with environment new line
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static  string FormatRegularText(string text)
        {
            if (text.StartsWith(DialogStartChars) && text.EndsWith(DialogEndChars))
            {
                text = text.Substring(2, text.Length - 4);
            }
            else if (text.StartsWith("\"") && text.EndsWith("\""))
            {
                text = text.Substring(1, text.Length - 2);
            }

            text = text.Replace(UnixNewLine, Environment.NewLine);

            return text;
        }
    }
}
