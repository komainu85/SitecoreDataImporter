using System;
using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IItemCreator
    {
        Guid TemplateId { get; set; }
        Guid ParentItemId { get; set; }
        Item CreateItem(ImportItem importItem);
        Item CreateItem(string title, Dictionary<string,string> fields);
    }
}