using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Parsers
{
    public class CsvParse : IParser
    {
        public MediaItem MediaFile { get; set; }

        public List<ImportItem> ParseMediaItem()
        {
            var items = new List<ImportItem>();

            using (var csv = new CsvReader(new StreamReader(MediaFile.GetMediaStream()), true))
            {
                var fieldCount = csv.FieldCount;

                var headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord())
                {
                    var item = new ImportItem { Title = MediaFile.Name };

                    for (var i = 0; i < fieldCount; i++)
                    {
                        item.Fields.Add(headers[i], csv[i]);

                        if (headers[i].ToLower() == "title")
                        {
                            item.Title = csv[i];
                        }
                    }

                    items.Add(item);
                }
            }

            return items;
        }
    }
}
