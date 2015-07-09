using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikeRobbins.SitecoreDataImporter.Entities
{
    public class ImportAudit : Sitecore.Services.Core.Model.EntityIdentity
    {
        public List<string> ImportedItems { get; set; }
    }
}
