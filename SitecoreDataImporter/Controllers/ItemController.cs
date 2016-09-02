using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MikeRobbins.SitecoreDataImporter.Contracts;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.IoC;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.Controllers
{
    [ServicesController]
    public class ItemController : ServicesApiController
    {
        private static IItemRepository<DataItem> _itemRepository = Container.GetInstance<IItemRepository<DataItem>>();

        public static Container Container => new Container(new IoCRegistry());

        [HttpPut]
        [Authorize]
        public void ImportItems(DataItem dataItem)
        {
             IndexCustodian.PauseIndexing();
            _itemRepository.Add(dataItem);
            IndexCustodian.ResumeIndexing();
        }

        [HttpGet]
        [Authorize]
        public ImportAudit GetImportAudit(string id)
        {
            return _itemRepository.GetImportAudit(id);
        }
    }
}
