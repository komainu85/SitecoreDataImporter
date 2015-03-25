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

            _itemCreator.ParentItemId = new Guid("{1251023A-F7E0-4559-BCDF-04340C731EBE}");
            _itemCreator.TemplateId = new Guid("{1AE3FFBE-5B18-4BC9-881B-ED9948864411}");

            _itemCreator.CreateItem("Import" + DateTime.Now.ToString("dd mm yyyy HHmm"), fields);
        }
    }
}
