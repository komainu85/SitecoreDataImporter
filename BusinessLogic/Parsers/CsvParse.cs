using System.Collections.Generic;
using System.IO;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;

namespace MikeRobbins.SitecoreDataImporter.BusinessLogic.Parsers
{
    public class CsvParse : IDataImport
    {
        public MediaItem MediaFile { get; set; }
        public List<string> Documents { get; set; }

        public List<ImportItem> Parse()
        {
            var items = new List<ImportItem>();
            var language = Tools.Tools.GetLanguageFromFile(MediaFile.Name);

            using (var csv = new CsvReader(new StreamReader(MediaFile.GetMediaStream()), true))
            {
                var fieldCount = csv.FieldCount;

                var headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord())
                {
                    var item = new ImportItem { Language = language, Title = MediaFile.Name };      

                    for (var i = 0; i < fieldCount; i++)
                    {
                        item.Fields.Add(headers[i], csv[i]);

                       // if (headers[i].ToLower() == "title")
                      //  {
                       //     item.Title = csv[i];
                       // }
                    }

                    items.Add(item);
                }
            }
            return items;
        }
    }
}
