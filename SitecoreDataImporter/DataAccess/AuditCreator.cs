using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class AuditCreator : IAuditCreator
    {
        private IItemCreator _itemCreator;

        public AuditCreator(IItemCreator itemCreator)
        {
            _itemCreator = itemCreator;
        }

        public void CreateAudit(List<Item> importedItems)
        {
            var fields = new Dictionary<string, string>();

            var successfullImports = importedItems.Aggregate("", (current, importedItem) => current + importedItem.ID.ToString() + "|");

            fields.Add("Imported Items", successfullImports);

            _itemCreator.CreateItem("Import" + DateTime.Now.ToString("ddmmyyyy HHmm"), fields);
        }
    }
}
