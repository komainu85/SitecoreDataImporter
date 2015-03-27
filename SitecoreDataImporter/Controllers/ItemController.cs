using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.IoC;
using MikeRobbins.SitecoreDataImporter.Repositories;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.Controllers
{
    [ServicesController]
    public class ItemController : EntityService<DataItem>
    {
        private ICustomRepositoryActions<DataItem> _customRepositoryActions;

        private Container _container;

        public static Container Container
        {
            get
            {
                return new Container(new IoCRegistry());
            }
        }


        public ItemController(ICustomRepositoryActions<DataItem> repository)
            : base(repository)
        {
            _customRepositoryActions = repository;

        }

        public ItemController()
            : this(Container.GetInstance<ICustomRepositoryActions<DataItem>>())
        {
        }

        [HttpGet]
        [ActionName("GetImportAudit")]
        public ImportAudit Get()
        {
            return _customRepositoryActions.GetImportAudit();
        }
    }
}
