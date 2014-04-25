using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeRobbins.SitecoreDataImporter
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
                x.For<SitecoreDataImport.Business.Interfaces.IDataImport>()
                    .Add<SitecoreDataImport.Business.Parsers.CsvParse>()
                    .Named("CSV");

                x.For<SitecoreDataImport.Business.Interfaces.IProcessInputFile>()
                  .Add<SitecoreDataImport.Business.ProcessInputFile>();

                x.For<SitecoreDataImport.Business.Interfaces.IItemImporter>()
                  .Add<SitecoreDataImport.Business.ItemImporter>();
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
                var itemImporter = ObjectFactory.GetInstance<SitecoreDataImport.Business.Interfaces.IItemImporter>();

                var resultOutput = itemImporter.ImportItems(importItems, templateId, parentFolder, update);

                return JsonConvert.SerializeObject(resultOutput);
            }
            return "";
        }

        private List<ImportItem> ProcessInputFiles(string fileIds, TemplateItem template)
        {
            var files = GetFilesFromMediaLibrary(fileIds);

            var processInputFile = ObjectFactory.GetInstance<SitecoreDataImport.Business.Interfaces.IProcessInputFile>();
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