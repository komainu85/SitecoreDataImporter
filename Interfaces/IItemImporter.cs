using System.Collections.Generic;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IItemImporter
    {
        List<Entities.Output> ImportItems(List<Entities.ImportItem> items, Sitecore.Data.ID templateID, Item parentFolder, bool updateExisting);
    }
}