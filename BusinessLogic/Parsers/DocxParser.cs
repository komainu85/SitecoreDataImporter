using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikeRobbins.SitecoreDataImporter.BusinessLogic.Parsers
{
    public class DocxParser : IDataImport
    {
        public Sitecore.Data.Items.MediaItem MediaFile { get; set; }

        public List<Entities.ImportItem> Parse()
        {
            var importItems = new List<ImportItem>();

          var html =  ConvertToHTML();

        }


        public Dictionary<string, string> GetFieldData()
        {
            var html = ParseToHTML();

            var fields = new Dictionary<string, string>();

            var body = html.Elements().FirstOrDefault(x => x.ToString().StartsWith("<body"));

            //patch word exported lists as spans to standard html lists            
            if (body != null)
            {
                string content = body.ToString();
                content = SpansToList(content);
                body = StringToXElement(content);
            }

            string field = "", value = "";

            foreach (XElement child in body.Elements())
            {
                if (child.ToString().Contains("pt-Title"))
                {
                    if (field != "")
                    {
                        fields.Add(field, value);
                        value = "";
                    }

                    field = child.Value;
                }
                else
                {
                    value = value + Tools.Tools.CleanHTML(child).ToString();

                    if (child.NextNode == null)
                    {
                        fields.Add(field, value);
                    }
                }
            }


            return fields;
        }


        public string ConvertToHTML()
        {
            var wmldoc = new WmlDocument("tempDoc" ,MediaFile.GetMediaStream);

            var settings = new HtmlConverterSettings()
            {
                PageTitle = "HTML Converted Page",
            };

            var html = HtmlConverter.ConvertToHtml(wmldoc, settings);
        }
    }
}
