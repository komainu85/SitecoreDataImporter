using System;
using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Contracts;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Utilities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class ItemCreator : IItemCreator
    {
        private IFieldUpdater _iFieldUpdater;
        private IItemReader _itemReader;

        public ItemCreator(IFieldUpdater iFieldUpdater,IItemReader iItemReader)
        {
            _iFieldUpdater = iFieldUpdater;
            _itemReader = iItemReader;
        }

        public Guid TemplateId { get; set; }
        public Guid ParentItemId { get; set; }

        public Item CreateItem(ImportItem importItem)
        {
            var parentItem = _itemReader.GetItem(ParentItemId);
            var template = _itemReader.GetTemplateItem(TemplateId);

            var newItem = parentItem.Add(importItem.Title.ToSitecoreSafeString(), template);

            _iFieldUpdater.AddFieldsDictionaryToItem(newItem, importItem.Fields);

            return newItem;
        }

        public Item CreateItem(string title, Dictionary<string,string> fields)
        {
            var parentItem = _itemReader.GetItem(ParentItemId);
            var template = _itemReader.GetTemplateItem(TemplateId);

            var newItem = parentItem.Add(title, template);

            _iFieldUpdater.AddFieldsDictionaryToItem(newItem, fields);

            return newItem;
        }
    }
}