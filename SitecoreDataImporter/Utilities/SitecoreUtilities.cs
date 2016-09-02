using Sitecore.Data;
using Sitecore.Data.Items;
using System;

namespace MikeRobbins.SitecoreDataImporter.Utilities
{
    public static class SitecoreUtilities
    {
        public static string ToSitecoreSafeString(this string s)
        {
            if (s.Length > 50)
                s = s.Substring(0, 50).Trim();

            return ItemUtil.ProposeValidItemName(s);
        }

        public static ID ParseId(string id)
        {
            ID sID;

            ID.TryParse(id, out sID);

            return sID;
        }

        public static ID ParseId(Guid id)
        {
            ID sID;

            ID.TryParse(id, out sID);

            return sID;
        }
    }
}
