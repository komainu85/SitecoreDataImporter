using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.BusinessLogic
{
    public class ProcessInputFile : IProcessInputFile
    {
        public List<MediaItem> MediaFile { get; set; }
        public TemplateItem Template { get; set; }

        public List<ImportItem> ParseFile()
        {
            var items = new List<ImportItem>();

            foreach (var mediaFile in MediaFile)
            {
                var extension = mediaFile.Extension;

                switch (extension)
                {
                    case "csv":
                        var csv = ObjectFactory.GetNamedInstance<IDataImport>("CSV");
                        csv.MediaFile = mediaFile;
                        items.AddRange(csv.Parse());
                        break;
                    default:
                        break;
                }
            }

            return items;
        }
    }
}