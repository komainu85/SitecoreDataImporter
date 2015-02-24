using System;
using System.Collections.Generic;
using System.Linq;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class SitecoreDataAccess : ISitecoreDataAccess
    {
        private static readonly Database MasterDb = Database.GetDatabase("master");

        #region Items
        public Item CreateSitecoreItem(TemplateItem template, Item parentItem, string itemName, Dictionary<string, string> fields, Language language)
        {
            Item newItem;

            //Get Master Database Versiom
            var masterParent = MasterDb.GetItem(parentItem.ID, language);
            newItem = masterParent.Add(itemName.ToSitecoreSafeString(), template);

            SetFields(fields, newItem, language, false);


            return newItem;
        }

        public Item UpdateSitecoreItem(Item item, Dictionary<string, string> fields, Language language, bool createNewVersion)
        {

            SetFields(fields, item, language, createNewVersion);


            return item;
        }

        public static Item GetItem(string path)
        {
            return MasterDb.GetItem(path);
        }

        private static void SetFields(Dictionary<string, string> fields, Item item, Language language, bool createNewVersion)
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
                        itemTargetLang.Fields[field.Key].Value = SitecoreUtilities.ConvertStringToDate(field.Value);
                    }
                    else if (itemTargetLang.Fields[field.Key] != null && itemTargetLang.Fields[field.Key].TypeKey == "single-line text")
                    {
                        itemTargetLang.Fields[field.Key].Value = SitecoreUtilities.StripHTML(field.Value);
                    }
                    else if (itemTargetLang.Fields[field.Key] != null && itemTargetLang.Fields[field.Key].TypeKey == "multi-line text")
                    {
                        itemTargetLang.Fields[field.Key].Value = SitecoreUtilities.HtmlToPainText(field.Value);
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