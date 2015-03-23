using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using MikeRobbins.SitecoreDataImporter.DataAccess;
using Sitecore.Data;
using Sitecore.Globalization;

namespace MikeRobbins.SitecoreDataImporter.Utilities
{
    public static class SitecoreUtilities
    {
        private const string RegionRegEx = "[a-z]{2,3}-[A-Z]{2}";

        public static string ToSitecoreSafeString(this string s)
        {
            var cleaned = s.Trim()
                           .Replace("–", "_")
                           .Replace("-", "_")
                           .Replace("&", "and")
                           .Replace("'", "")
                           .Replace(".", "")
                           .Replace("__", "_")
                           .Replace("__", "_")
                           .Replace("/", " ")
                           .Replace("\\", " ")
                           .Replace(":", " ")
                           .Replace("(", "_")
                           .Replace(")", "_")
                           .Replace("?", " ")
                           .Replace("\"", " ")
                           .Replace("<", " ")
                           .Replace(">", " ")
                           .Replace("[", " ")
                           .Replace("]", " ")
                           .Replace(";", " ")
                           .Replace("\n", " ")
                           .Replace("\r", " ")
                           .Trim();

            if (cleaned.Length > 50)
            {
                cleaned = cleaned.Substring(0, 50).Trim();
            }

            return cleaned;
        }

        public static string ConvertStringToDate(string value)
        {
            DateTime date;
            DateTime.TryParse(value, out date);

            return Sitecore.DateUtil.ToIsoDate(date);
        }

        public static ID ParseId(string id)
        {
            var sID = ID.Null;

            ID.TryParse(id, out sID);

            return sID;
        }

        public static ID ParseId(Guid id)
        {
            var sID = ID.Null;

            ID.TryParse(id, out sID);

            return sID;
        }


        public static string StripHTML(string value)
        {
            var noHTML = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();

            return Regex.Replace(noHTML, @"\s{2,}", " ");
        }

        public static string HtmlToPainText(string value)
        {
            return Regex.Replace(value, @"<(.|\n)*?>", "");
        }

        public static string CleanHTML(XElement element)
        {
            // Remove all inline styles
            element.Attributes("style").Remove();
            element.Attributes("class").Remove();

            foreach (var node in element.Descendants())
            {
                node.Attributes("style").Remove();
                node.Attributes("class").Remove();
            }

            var value = element.ToString();
            return value.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "").Trim();
        }

        public static Language GetLanguageFromFile(string fileName)
        {
            var matches = Regex.Match(fileName, RegionRegEx);

            var langCode = "";

            if (matches.Success)
            {
                langCode = matches.Groups[0].Value;
            }

            var da = new SitecoreDataAccess();
            return da.GetLanguage(langCode);
        }

        public static string GetDocumentTitleFromFile(string fileName, string extension)
        {
            var matches = Regex.Match(fileName, RegionRegEx);

            var langCode = "";

            if (matches.Success)
            {
                langCode = matches.Groups[0].Value;
                fileName = fileName.Replace(langCode, "");
            }

            fileName = fileName.Replace(extension, "");

            return fileName;
        }
    }
}
