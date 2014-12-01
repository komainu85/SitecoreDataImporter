using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MikeRobbins.SitecoreDataImporter.Entities;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.Controllers
{
    public class ImporterController : Controller
    {
        #region Properties
        readonly Database _master = Database.GetDatabase("master");
        #endregion

        #region Constructor
        public ImporterController()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<MikeRobbins.SitecoreDataImporter.Interfaces.IDataImport>()
                    .Add<SitecoreDataImporter.BusinessLogic.Parsers.CsvParse>()
                    .Named("CSV");

                x.For<SitecoreDataImporter.Interfaces.IProcessInputFile>()
                  .Add<SitecoreDataImporter.BusinessLogic.ProcessInputFile>();

                x.For<SitecoreDataImporter.Interfaces.IItemImporter>()
                  .Add<SitecoreDataImporter.BusinessLogic.ItemImporter>();
            });
        }
        #endregion

        #region Public Method
        public string Import()
        {
            var selectedtemplateId = WebUtil.GetFormValue("template");
            var selectedFolderId = WebUtil.GetFormValue("folder");
            var fileIds = WebUtil.GetFormValue("fileIds");

            bool update;
            bool.TryParse(WebUtil.GetFormValue("update"), out update);

            var templateItem = _master.GetTemplate(ID.Parse((selectedtemplateId)));
            var parentFolder = _master.GetItem(new ID(selectedFolderId));

            var importItems = ProcessInputFiles(fileIds, templateItem);

            return ImportSitecoreItems(importItems, templateItem.ID, parentFolder, update);
        }
        #endregion

        #region Private Methods

        private string ImportSitecoreItems(List<ImportItem> importItems, ID templateId, Item parentFolder, bool update)
        {
            if (importItems.Any())
            {
                var itemImporter = ObjectFactory.GetInstance<MikeRobbins.SitecoreDataImporter. Interfaces.IItemImporter>();

                var resultOutput = itemImporter.ImportItems(importItems, templateId, parentFolder, update);

                return JsonConvert.SerializeObject(resultOutput);
            }
            return "";
        }

        private List<ImportItem> ProcessInputFiles(string fileIds, TemplateItem template)
        {
            var files = GetFilesFromMediaLibrary(fileIds);

            var processInputFile = ObjectFactory.GetInstance<SitecoreDataImporter.Interfaces.IProcessInputFile>();
            processInputFile.MediaFile = files;
            processInputFile.Template = template;

            var importItems = processInputFile.ParseFile();

            return importItems;
        }

        private List<MediaItem> GetFilesFromMediaLibrary(string fileNames)
        {
            var mediaIds = fileNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return (from mediaId in mediaIds select _master.GetItem(new ID(mediaId)) into item where item != null select (MediaItem)item).ToList();
        }
        #endregion
    }
}