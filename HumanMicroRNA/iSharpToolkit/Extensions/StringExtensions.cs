using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace iSharpToolkit.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert string to Int16
        /// </summary>
        /// <param name="num">string num</param>
        /// <returns>Returns Int16</returns>
        public static Int16 ToInt16(this string num)
        {
            Int16 int16Temp = 0;

            if (Int16.TryParse(num, out int16Temp))
                return int16Temp;
            else return 0;
        }
        /// <summary>
        /// Convert string to Int32
        /// </summary>
        /// <param name="num">string num</param>
        /// <returns>Returns Int32</returns>
        public static Int32 ToInt32(this string num)
        {
            Int32 int32Temp = 0;

            if (Int32.TryParse(num, out int32Temp))
                return int32Temp;
            else return 0;
        }
        /// <summary>
        /// Convert string to Int64
        /// </summary>
        /// <param name="num">string nu</param>
        /// <returns>Returns Int64</returns>
        public static Int64 ToInt64(this string num)
        {
            Int64 int64Temp = 0;

            if (Int64.TryParse(num, out int64Temp))
                return int64Temp;
            else return 0;
        }
        /// <summary>
        /// Convert string to Decimal.
        /// </summary>
        /// <param name="num">string num</param>
        /// <returns>Return decimal</returns>
        public static Decimal ToDecimal(this string num)
        {
            Decimal decimalTemp = 0;

            if (Decimal.TryParse(num, out decimalTemp))
                return decimalTemp;
            else return 0;
        }
        /// <summary>
        /// This method is designed in order to validate the syntax
        /// of a string passed to it as a valid email address.
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns>Returns a validated email address string.</returns>
        public static bool isValidEmail(this string emailAddress)
        {
            return IsValidEmail(emailAddress);
        }
        /// <summary>
        /// This method is designed in order to check whether a
        /// password passed to it meets the basic requirement of
        /// a somewhat strong password.
        /// </summary>
        /// <param name="password">string password</param>
        /// <returns>Returns a boolean value indicating whether the password is strong.</returns>
        public static bool isPasswordStrong(this string password)
        {
            //(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+
            return Regex.IsMatch(password, @"^.{8,}(?<=\d.*)(?<=[^a-zA-Z0-9].*)$");
        }


        #region Validate Email
        private static bool invalid = false;
        public static bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

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
