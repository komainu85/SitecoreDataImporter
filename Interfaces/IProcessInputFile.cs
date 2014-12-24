using System.Collections.Generic;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IProcessInputFile
    {
        MediaItem MediaFile { get; set; }
        TemplateItem Template { get; set; }
        Item ParentFolder { get; set; }
        bool UpdateExisting { get; set; }
        List<Entities.Output> ParseFile(IParser parser);
    }
}