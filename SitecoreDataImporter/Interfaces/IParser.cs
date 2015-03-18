using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IParser
    {
        MediaItem MediaFile { get; set; }
        List<ImportItem> ParseMediaItem();
    }
}
