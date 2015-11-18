using System;
using System.Linq;
using MikeRobbins.SitecoreDataImporter.Contracts;
using MikeRobbins.SitecoreDataImporter.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class ItemReader : IItemReader
    {
        public Item GetItem(ID id)
        {
            return Sitecore.Data.Database.GetDatabase("master").GetItem(id);
        }

        public Item GetItem(Guid id)
        {
            Item item = null;

            var sId = SitecoreUtilities.ParseId(id);

            if (!sId.IsNull)
            {
                item = GetItem(sId);
            }

            return item;
        }

        public Item GetItem(string id)
        {
            Item item = null;

            var sId = SitecoreUtilities.ParseId(id);

            if (!sId.IsNull)
            {
                item = GetItem(sId);
            }

            return item;
        }

        public bool ItemExists(Item parentItem, string title)
        {
            bool exist = false;

            exist = parentItem.Children.Any(i => string.Equals(i.Name.Trim(), title.Trim(), StringComparison.InvariantCultureIgnoreCase));

            return exist;
        }

        public TemplateItem GetTemplateItem(Guid id)
        {
            Item templateItem = null;
            var item = GetItem(id);

            if (item != null)
            {
                templateItem = (TemplateItem)item;
            }

            return templateItem;
        }
    }
}