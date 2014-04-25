using System.Collections.Generic;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IDataImport
    {
        MediaItem MediaFile { get; set; }
        List<Entities.ImportItem> Parse();
    }
}
