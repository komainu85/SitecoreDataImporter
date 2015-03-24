using System;
using System.Collections.Generic;
using Lucene.Net.Util;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IItemCreator
    {
        Guid TemplateId { get; set; }
        Guid ParentItemId { get; set; }
        Item CreateItem(ImportItem importItem);
        Item CreateItem(string title, Dictionary<string,string> fields);
    }
}