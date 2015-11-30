using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IParser
    {
        MediaItem MediaFile { get; set; }
        List<ImportItem> ParseMediaItem();
    }
}
