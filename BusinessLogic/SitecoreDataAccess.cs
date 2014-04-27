using System;
using System.Collections.Generic;
using System.Linq;
using MikeRobbins.SitecoreDataImporter.BusinessLogic.Tools;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace MikeRobbins.SitecoreDataImporter.BusinessLogic
{
    public class SitecoreDataAccess : ISitecoreDataAccess
    {
        private static readonly Database MasterDb = Database.GetDatabase("master");

        #region Items
        public Item CreateSitecoreItem(TemplateItem template, Item parentItem, string itemName, Dictionary<string, string> fields, Language language)
        {
            Item newItem = null;

            using (new SecurityDisabler())
            {
                //Get Master Database Versiom
                var masterParent = MasterDb.GetItem(parentItem.ID, language);
                newItem = masterParent.Add(itemName.ToSitecoreSafeString(),template);

                AddItemToCaches(template, language, newItem);

                SetFields(fields, newItem, language, false);
            }

            return newItem;
        }

        private static void AddItemToCaches(TemplateItem template, Language language, Item newItem)
        {
            var standardValues = new SafeDictionary<ID, string>();

            foreach (Field standardValue in template.StandardValues.Fields)
            {
                standardValues.Add(standardValue.ID, standardValue.Value);
            }

            var itemInfo = new ItemInformation(newItem.InnerData.Definition);

            MasterDb.Caches.ItemCache.AddItem(newItem.ID, language, newItem.Version, newItem);
            MasterDb.Caches.DataCache.AddItemInformation(newItem.ID, itemInfo);
            MasterDb.Caches.StandardValuesCache.AddStandardValues(newItem, standardValues);
        }

        public Item UpdateSitecoreItem(Item item, Dictionary<string, string> fields, Language language, bool createNewVersion)
        {
            using (new SecurityDisabler())
            {
                SetFields(fields, item, language, createNewVersion);
            }

            return item;
        }

        public static Item GetItem(string path)
        {
            return MasterDb.GetItem(path);
        }

        private static void SetFields(Dictionary<string, string> fields, Item item, Language language, bool createNewVersion)
        {
            using (new SecurityDisabler())
            {
                Item itemTargetLang = item.Language == language ? item : MasterDb.GetItem(item.ID, language);

                try
                {
                    if (itemTargetLang.Versions.Count == 0 || createNewVersion)
                    {
                        itemTargetLang = itemTargetLang.Versions.AddVersion();
                    }

                    itemTargetLang.Editing.BeginEdit();

                    foreach (var field in fields)
                    {
                        if (itemTargetLang.Fields[field.Key] != null && itemTargetLang.Fields[field.Key].TypeKey == "date" || itemTargetLang.Fields[field.Key] != null && itemTargetLang.Fields[field.Key].TypeKey == "datetime")
                        {
                            itemTargetLang.Fields[field.Key].Value = Tools.Tools.ConvertStringToDate(field.Value);
                        }
                        else if (itemTargetLang.Fields[field.Key] != null && itemTargetLang.Fields[field.Key].TypeKey == "single-line text")
                        {
                            itemTargetLang.Fields[field.Key].Value = Tools.Tools.StripHTML(field.Value);
                        }
                        else if (itemTargetLang.Fields[field.Key] != null && itemTargetLang.Fields[field.Key].TypeKey == "multi-line text")
                        {
                            itemTargetLang.Fields[field.Key].Value = Tools.Tools.HtmlToPainText(field.Value);
                        }
                        else if (itemTargetLang.Fields[field.Key] != null)
                        {
                            itemTargetLang[field.Key] = field.Value;
                        }
                    }

                    itemTargetLang.Editing.EndEdit();
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Could not update item " + item.Paths.FullPath + ": " + ex.Message, null);
                    itemTargetLang.Editing.CancelEdit();
                }
            }
        }
        #endregion

        #region Templates
        public static TemplateItem GetTemplateByID(ID templateId)
        {
            return MasterDb.GetTemplate(templateId);
        }
        #endregion

        #region Languages
        public Language GetLanguage(string languageName)
        {
            Language language;

            if (!Language.TryParse(languageName, out language) || string.IsNullOrEmpty(languageName))
            {
                language = Sitecore.Context.Language;
            }

            return language;
        }
        #endregion

        #region Validation
        public Item DoesNameAtLevelExist(Item parentItem, string itemName)
        {
            Item exist = null;
            using (new SecurityDisabler())
            {
                //Get Master Database Versiom
                var masterParent = Database.GetDatabase("master").GetItem(parentItem.ID);
                exist = masterParent.Children.FirstOrDefault(i => string.Equals(i.Name.Trim(), itemName.Trim(), StringComparison.InvariantCultureIgnoreCase));
            }

            return exist;
        }
        #endregion
    }
}