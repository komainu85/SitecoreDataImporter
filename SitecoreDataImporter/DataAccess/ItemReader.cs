using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.Utilities;
using Sitecore.Data.Items;

namespace MikeRobbins.EntityServiceDemo.DataAccess
{
    public class ItemReader
    {


        public void GetItem(string id)
        {
            var id = SitecoreUtilities.ParseId(id);

            if (!id.IsNull)
            {
                var item = _iNewsReader.GetNewsItem(id);

                _iFieldUpdater.AddFieldsToItem(item, newsArticle);
            }
        }

    }
}