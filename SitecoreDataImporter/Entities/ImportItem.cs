using System.Collections.Generic;
using Sitecore.Globalization;

namespace MikeRobbins.SitecoreDataImporter.Entities
{
    public class ImportItem
    {
        public string Title { get; set; }

        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    }
}
