using System;
using System.Collections.Generic;
using System.Linq;
using MikeRobbins.SitecoreDataImporter.BusinessLogic.Tools;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Entities.Enums;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.BusinessLogic
{
    public class ProcessInputFile : IProcessInputFile
    {
        public MediaItem MediaFile { get; set; }
        public TemplateItem Template { get; set; }
        public Item ParentFolder { get; set; }
        public bool UpdateExisting { get; set; }

        public List<Output> ParseFile(IParser parser)
        {
            var items = new List<ImportItem>();

            parser.MediaFile = MediaFile;
            items.AddRange(parser.Parse());

            return ImportItems(items);
        }

        public List<Output> ImportItems(List<ImportItem> items)
        {
            var output = new List<Output>();

            var da = new SitecoreDataAccess();
            var i = 1;

            foreach (var item in items)
            {
                try
                {
                    var itemExists = da.DoesNameAtLevelExist(ParentFolder, item.Title);

                    if (itemExists == null)
                    {
                        da.CreateSitecoreItem(Template, ParentFolder, item.Title, item.Fields, item.Language);
                        output.Add(new Output() { Number = i, Item = item.Title, Result = Result.Success.ToString() });
                    }
                    else
                    {
                        var langVersion = Sitecore.Data.Database.GetDatabase("master").GetItem(itemExists.ID, item.Language);

                        if (UpdateExisting || langVersion.Versions.Count == 0)
                        {
                            da.UpdateSitecoreItem(itemExists, item.Fields, item.Language, langVersion.Versions.Count > 0);
                            output.Add(new Output() { Number = i, Item = item.Title, Result = Result.Updated.ToString() });
                        }
                        else
                        {
                            output.Add(new Output() { Number = i, Item = item.Title, Result = Result.Skipped.ToString(), Reason = "Item already exists with this name" });
                        }
                    }

                }
                catch (Exception ex)
                {
                    output.Add(new Output() { Number = i, Item = !string.IsNullOrEmpty(item.Title) ? item.Title : "NAME MISSING!", Result = Result.Failure.ToString(), Reason = ex.Message });
                }

                i++;
            }

            return output;
        }

    }
}