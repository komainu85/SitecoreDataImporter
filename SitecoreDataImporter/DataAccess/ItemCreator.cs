using System;
using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Contracts;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Utilities;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class ItemCreator : IItemCreator
    {
        private readonly IFieldUpdater _iFieldUpdater;
        private readonly IItemReader _itemReader;

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
            var template = GetTemplate(TemplateId);

            //SecurityDisabler and EnforceVersionPresenceDisabler added to bypass LanguageFallback and security. Remove if not desired
            using (new SecurityDisabler())
            {
                using (new EnforceVersionPresenceDisabler())
                {
                    var newItem = parentItem.Add(importItem.Title.ToSitecoreSafeString(), template);

                    _iFieldUpdater.AddFieldsDictionaryToItem(newItem, importItem.Fields);

                    return newItem;
                }
            }
        }

        public Item CreateItem(string title, Dictionary<string,string> fields)
        {
            var parentItem = _itemReader.GetItem(ParentItemId);
            var template = GetTemplate(TemplateId);

            var newItem = parentItem.Add(title, template);

            _iFieldUpdater.AddFieldsDictionaryToItem(newItem, fields);

            return newItem;
        }

        /// <summary>
        /// Casts item to BranchItem or TemplateItem depending on templatename
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        private dynamic GetTemplate(Guid templateId)
        {
            var templateItem = _itemReader.GetItem(templateId);
            if (templateItem != null)
            {
                if (templateItem.TemplateName.ToLowerInvariant().Equals("branch"))
                    return (BranchItem)templateItem;

                return (TemplateItem)templateItem;
            }
            return null;
        }
    }
}