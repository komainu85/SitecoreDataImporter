using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class AuditReader : IAuditReader
    {
        private const string AuditFolderId = "{1251023A-F7E0-4559-BCDF-04340C731EBE}";

        public ImportAudit GetLatestAudit(string mediaItemId)
        {
            var importAudit = new ImportAudit();

            var auditFolder = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(AuditFolderId));

            if (auditFolder != null)
            {
                var lastestAudit = auditFolder.Children.Where(x => x["Media file"] == mediaItemId).OrderByDescending(x => x.Statistics.Created).FirstOrDefault();

                if (lastestAudit != null)
                {
                    importAudit.ImportedItems = GetTitles(lastestAudit, "Imported Items");
                }
            }

            return importAudit;
        }

        private List<string> GetTitles(Item auditItem, string fieldName)
        {
            var titles = new List<string>();

            var importedItemsField = (Sitecore.Data.Fields.MultilistField)auditItem.Fields[fieldName];

            if (importedItemsField != null)
            {
                titles.AddRange(importedItemsField.GetItems().Select(item => item.Name));
            }

            return titles;
        }
    }
}
