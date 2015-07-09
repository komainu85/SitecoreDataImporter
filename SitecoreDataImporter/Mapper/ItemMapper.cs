using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Mapper
{
    public class ItemMapper
    {
        public DataItem GetNewsArticle(Item item)
        {
            var dataItem = new DataItem();

            dataItem.Name = item.Name;
            dataItem.Id = item.ID.ToString();
            dataItem.Url = item.Paths.ContentPath;

            return dataItem;
        }    
    }
}
