using System.Collections.Generic;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IParser
    {
        MediaItem MediaFile { get; set; }
        Dictionary<string,string> ParseMediaItem();
    }
}
