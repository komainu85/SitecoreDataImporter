using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IItemReader
    {
        Item GetItem(ID id);
        Item GetItem(Guid id);
        Item GetItem(string id);
        bool ItemExists(Item parentItem, string title);
        TemplateItem GetTemplateItem(Guid id);
    }
}