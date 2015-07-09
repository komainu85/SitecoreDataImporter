using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.IoC;
using MikeRobbins.SitecoreDataImporter.Repositories;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using Sitecore.Services.Infrastructure.Web.Http;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.Controllers
{
    [ServicesController]
    public class ItemController : ServicesApiController
    {
        private static ICustomRepositoryActions<DataItem> _customRepositoryActions = Container.GetInstance<ICustomRepositoryActions<DataItem>>();


        public static Container Container
        {
            get
            {
                return new Container(new IoCRegistry());
            }
        }

        [HttpPut]
        public void ImportItems(DataItem dataItem)
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;

            _customRepositoryActions.Add(dataItem);
        }


        [HttpGet]
        public ImportAudit GetImportAudit()
        {
            return _customRepositoryActions.GetImportAudit();
        }
    }
}
