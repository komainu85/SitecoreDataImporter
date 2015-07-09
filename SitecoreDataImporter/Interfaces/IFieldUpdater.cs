using System.Collections.Generic;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IFieldUpdater
    {
        void AddFieldsToItem<T>(Item item, T sourceObject);

        void AddFieldsDictionaryToItem(Item item, Dictionary<string,string> fields );
    }
}