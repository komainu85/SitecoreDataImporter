using System.Collections.Generic;
using System.IO;
using MikeRobbins.SitecoreDataImporter.Contracts;
using MikeRobbins.SitecoreDataImporter.Entities;
using Newtonsoft.Json;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Parsers
{
    public class JsonParser : IParser
    {
        public MediaItem MediaFile { get; set; }

        public List<ImportItem> ParseMediaItem()
        {
            var items = new List<ImportItem>();

            dynamic json = DeserialiseJson();

            CreateImportItem(json, items);

            return items;
        }

        private void CreateImportItem(dynamic json, List<ImportItem> items)
        {
            foreach (var jsonItem in json)
            {
                var item = new ImportItem() {Title = MediaFile.Name};

                foreach (var property in jsonItem)
                {
                    if (property.Name.ToLower() == "title")
                    {
                        item.Title = property.Value.Value;
                    }

                    item.Fields.Add(property.Name, property.Value.Value);
                }

                items.Add(item);
            }
        }

        private dynamic DeserialiseJson()
        {
            dynamic json;
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(MediaFile.GetMediaStream()))
            {
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    json = serializer.Deserialize(jsonTextReader);
                }
            }

            return json;
        }
    }
}
