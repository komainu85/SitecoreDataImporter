using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class ItemReader : IItemReader
    {
        public Item GetItem(ID id)
        {
            return Sitecore.Context.Database.GetItem(id);

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

    }
}