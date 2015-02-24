using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Repositories;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace MikeRobbins.SitecoreDataImporter.Controllers
{
    [ServicesController]
    public class ItemController : EntityService<DataItem>
    {
        public ItemController(IRepository<DataItem> repository)
            : base(repository)
        {
        }

        public ItemController()
            : this(new ItemRespository())
        {
        }
    }
}
