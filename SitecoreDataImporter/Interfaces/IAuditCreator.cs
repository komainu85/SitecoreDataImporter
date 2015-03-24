using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
  public  interface IAuditCreator
  {
      void CreateAudit(List<Item> importedItems);
  }
}
