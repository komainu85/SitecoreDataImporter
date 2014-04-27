using System.Collections.Generic;
using Sitecore.Globalization;

namespace MikeRobbins.SitecoreDataImporter.Entities
{
    public class ImportItem
    {
        public string Title { get; set; }
        public Language Language { get; set; }
        private Dictionary<string, string> _fields = new Dictionary<string, string>();

        public Dictionary<string, string> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }
    }
}
