using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
  public  interface IAuditCreator
  {
      void CreateAudit(List<Item> importedItems, ID mediaItemId);
  }
}
