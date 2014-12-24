using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.Controllers
{
    public class ImporterController : Controller
    {
        readonly Database _master = Database.GetDatabase("master");

        public ImporterController()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<MikeRobbins.SitecoreDataImporter.Interfaces.IParser>()
                    .Add<SitecoreDataImporter.BusinessLogic.Parsers.CsvParse>()
                    .Named("CSV");

                //x.For<MikeRobbins.SitecoreDataImporter.Interfaces.IParser>()
                //    .Add<SitecoreDataImporter.BusinessLogic.Parsers.CsvParse>()
                //    .Named("DOCX");

                x.For<SitecoreDataImporter.Interfaces.IProcessInputFile>()
                  .Add<SitecoreDataImporter.BusinessLogic.ProcessInputFile>();
            });
        }

        public string Import()
        {
            var selectedtemplateId = WebUtil.GetFormValue("template");
            var selectedFolderId = WebUtil.GetFormValue("folder");
            var fileIds = WebUtil.GetFormValue("fileIds");

            bool update;
            bool.TryParse(WebUtil.GetFormValue("update"), out update);

            var templateItem = _master.GetTemplate(ID.Parse((selectedtemplateId)));
            var parentFolder = _master.GetItem(new ID(selectedFolderId));

            return ProcessInputFiles(fileIds, templateItem, parentFolder, update);
        }

        private string ProcessInputFiles(string fileIds, TemplateItem template, Item parentFolder, bool update)
        {
          var results = new List<Output>();

            var files = GetFilesFromMediaLibrary(fileIds);

            IParser parser = null;

            foreach (var file in files)
            {
                parser = GetParserFromExtension(file, parser);

                var processInputFile = ObjectFactory.GetInstance<IProcessInputFile>();
                processInputFile.MediaFile = file;
                processInputFile.Template = template;
                processInputFile.ParentFolder = parentFolder;
                processInputFile.UpdateExisting = update;

                results.AddRange(processInputFile.ParseFile(parser));
            }

            return JsonConvert.SerializeObject(results); ;
        }

        private static IParser GetParserFromExtension(MediaItem file, IParser parser)
        {
            var extension = file.Extension;

            switch (extension)
            {
                case "csv":
                    parser = ObjectFactory.GetNamedInstance<IParser>("CSV");
                    break;
                default:
                    break;
            }
            return parser;
        }

        private List<MediaItem> GetFilesFromMediaLibrary(string fileNames)
        {
            var mediaIds = fileNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return (from mediaId in mediaIds select _master.GetItem(new ID(mediaId)) into item where item != null select (MediaItem)item).ToList();
        }
    }
}