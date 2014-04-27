using System.Collections.Generic;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IProcessInputFile
    {
       List< Sitecore.Data.Items.MediaItem> MediaFile { get; set; }
        TemplateItem Template { get; set; }
        List<Entities.ImportItem> ParseFile();
    }
}