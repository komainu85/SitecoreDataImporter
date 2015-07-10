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
    }
}
