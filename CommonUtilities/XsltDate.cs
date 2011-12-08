// ----------------------------------------------------------------------
// <copyright file="XsltDate.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities.XML
{
    using System;
    using System.Globalization;

    public class XsltDate
    {

        public const string XsltNameSpace = "urn:XsltDate";
        private const string DATE_FORMAT = "d/M/yyyy";
        private const string NUMERIC_DATE_FORMAT = "yyyyMMdd";

        /// <summary>
        /// Gets the current date in yyyyMMdd format.
        /// </summary>
        /// <returns></returns>
        public static string CurrentDate()
        {
            return DateTime.Now.ToString(NUMERIC_DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parses the date in d/M/yyyy format to yyyyMMdd format.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ParseDateToNumber(string datetime)
        {
            return DateTime.ParseExact(datetime.ToLower(), DATE_FORMAT, CultureInfo.InvariantCulture).ToString(NUMERIC_DATE_FORMAT, CultureInfo.InvariantCulture);
        }
    }
}
