using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Parsers
{
    public class XmlParser : IParser
    {
        public MediaItem MediaFile { get; set; }

        public List<ImportItem> ParseMediaItem()
        {
            List<ImportItem> items;

            var document = DeserialiseXml();

            items = CreateImportItem(document);

            return items;
        }

        private XmlDocument DeserialiseXml()
        {
            XmlDocument document = new XmlDocument();

            using (var sr = new StreamReader(MediaFile.GetMediaStream()))
            {
                using (XmlReader reader = XmlReader.Create(sr))
                {
                    document.Load(sr);
                }
            }

            return document;
        }

        private List<ImportItem> CreateImportItem(XmlDocument document)
        {
            var items = new List<ImportItem>();

            foreach (XmlNode node in document.ChildNodes)
            {
                var item = new ImportItem() { Title = MediaFile.Name };

                if (node.Name.ToLower() == "title")
                {
                    item.Title = node.Value;
                }

                item.Fields.Add(node.Name, node.Value);

                items.Add(item);

            }

            return items;
        }
    }
}

