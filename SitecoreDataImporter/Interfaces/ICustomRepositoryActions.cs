using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Services.Core;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface ICustomRepositoryActions<T> : Sitecore.Services.Core.IRepository<T> where T : IEntityIdentity
    {
        ImportAudit GetImportAudit();
    }
}
