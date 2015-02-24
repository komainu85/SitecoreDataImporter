using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IItemCreator
    {
        TemplateItem Template { get; set; }
        Item ParentItem { get; set; }
        void CreateItem(DataItem dataitem, Dictionary<string, string> fields);
    }
}