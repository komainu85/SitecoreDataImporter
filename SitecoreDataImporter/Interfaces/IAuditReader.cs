using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IAuditReader
    {
        ImportAudit GetLatestAudit(string fileName);
    }
}
