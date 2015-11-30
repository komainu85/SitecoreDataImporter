using System.Collections.Generic;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Services.Core.ComponentModel;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IFieldUpdater
    {
        void AddFieldsToItem<T>(Item item, T sourceObject);

        void AddFieldsDictionaryToItem(Item item, Dictionary<string,string> fields );
    }
}