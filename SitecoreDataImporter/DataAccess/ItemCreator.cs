using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class ItemCreator : IItemCreator
    {
        private IFieldUpdater _iFieldUpdater;

        public ItemCreator(IFieldUpdater iFieldUpdater)
        {
            _iFieldUpdater = iFieldUpdater;
        }

        public TemplateItem Template { get; set; }
        public Item ParentItem { get; set; }

        public void CreateItem(ImportItem importItem)
        {
            var newItem = ParentItem.Add(importItem.Title, Template);

            _iFieldUpdater.AddFieldsToItem(newItem, importItem.Fields);
        }
    }
}