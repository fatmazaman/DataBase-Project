using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace iSharpToolkit.Text
{
    /// <summary>
    ///		Common Regular Expressions
    /// </summary>
    public static class iSharpRegEx
    {
        #region Public Static Readonly Properties
        /// <summary>
        /// Matches a single/double quoted string including the quotes, disregards escaped quotes
        /// </summary>
        public static readonly Regex QuotedString;

        /// <summary>
        /// Matches one or more spaces, tabs, or line breaks
        /// </summary>
        public static readonly Regex WhiteSpaces;

        /// <summary>
        /// Matches one space, tab, or line break
        /// </summary>
        public static readonly Regex WhiteSpace;

        /// <summary>
        /// Matches a comma and any surrounding white space.
        /// </summary>
        public static readonly Regex CommaSplitPattern;

        /// <summary>
        /// Mathes the start of a url (http://, https://, and www.), case insensitive.
        /// </summary>
        public static readonly Regex UrlStart;
        #endregion

        #region Static Constructor
        static iSharpRegEx()
        {
            QuotedString = new Regex(@"((?<!\\)[""'])(?:(?=(\\?))\2.)*?\1", RegexOptions.Compiled);

            WhiteSpaces = new Regex(@"\s+", RegexOptions.Compiled);

            WhiteSpace = new Regex(@"\s", RegexOptions.Compiled);

            CommaSplitPattern = new Regex(@"\s*,\s*", RegexOptions.Compiled);

            UrlStart = new Regex("^(?:(?:http://)|(?:https://)|(?:www.))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
