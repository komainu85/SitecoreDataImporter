using System;
using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IItemCreator
    {
        Guid TemplateId { get; set; }
        Guid ParentItemId { get; set; }
        void CreateItem(ImportItem importItem);
    }
}