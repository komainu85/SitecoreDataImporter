using System;
using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Entities.Enums;
using MikeRobbins.SitecoreDataImporter.Interfaces;

namespace MikeRobbins.SitecoreDataImporter.BusinessLogic
{
    public class ItemImporter : IItemImporter
    {
        public List<Output> ImportItems(List<ImportItem> items, Sitecore.Data.ID templateID, Item parentFolder, bool updateExisting)
        {
            var output = new List<Output>();

            var da = new SitecoreDataAccess();
            var template = SitecoreDataAccess.GetTemplateByID(templateID);

            var i = 1;

            foreach (var item in items)
            {
                try
                {
                    var itemExists = da.DoesNameAtLevelExist(parentFolder, item.Title);

                    if (itemExists == null)
                    {
                        da.CreateSitecoreItem(template, parentFolder, item.Title, item.Fields, item.Language);
                        output.Add(new Output() {Number = i, Item = item.Title, Result = Result.Success.ToString() });
                    }
                    else
                    {
                        var langVersion = Sitecore.Data.Database.GetDatabase("master").GetItem(itemExists.ID, item.Language);

                        if (updateExisting || langVersion.Versions.Count == 0)
                        {
                            da.UpdateSitecoreItem(itemExists, item.Fields, item.Language, langVersion.Versions.Count > 0);
                            output.Add(new Output() {Number = i, Item = item.Title, Result = Result.Updated.ToString() });
                        }
                        else
                        {
                            output.Add(new Output() { Number = i, Item = item.Title, Result = Result.Skipped.ToString(), Reason = "Item already exists with this name" });
                        }
                    }

                }
                catch (Exception ex)
                {
                    output.Add(new Output() {Number = i,Item = !string.IsNullOrEmpty(item.Title) ? item.Title : "NAME MISSING!", Result = Result.Failure.ToString(), Reason = ex.Message });
                }

                i++;
            }

            return output;
        }
    }
}
