using System.Collections.Generic;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IDataImport
    {
        MediaItem MediaFile { get; set; }
        List<Entities.ImportItem> Parse();
    }
}
