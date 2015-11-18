using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
  public  interface IAuditCreator
  {
      void CreateAudit(List<Item> importedItems, ID mediaItemId);
  }
}
