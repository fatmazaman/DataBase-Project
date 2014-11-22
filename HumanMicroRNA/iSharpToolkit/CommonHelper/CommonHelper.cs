using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace iSharpToolkit.CommonHelper
{
    public class CommonHelper
    {

        #region Email Validation
        private static bool invalid = false;
        /// <summary>
        /// The following method is desinged in order to
        /// validate the email address and return a boolean
        /// value indicating whether it is of a correct format
        /// or not.
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns>Returns a boolean value indicating whether an email format is valid.</returns>
        public static bool IsValidEmail(string emailAddress)
        {
            invalid = false;
            if (String.IsNullOrEmpty(emailAddress))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            string domainStr = DomainMapper(Regex.Match(emailAddress, @"(@)(.+)$"));
            emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", domainStr);

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(emailAddress,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// The following method is designed in order
        /// to validate the domain.
        /// </summary>
        /// <param name="match">Match match</param>
        /// <returns>Returns the domain as a string.</returns>
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        } 
        #endregion
 
    }
}
