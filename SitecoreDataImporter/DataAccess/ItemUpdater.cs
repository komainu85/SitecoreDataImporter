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
    public class ItemUpdater
    {
        private IFieldUpdater _iFieldUpdater = null;

        public ItemUpdater(IFieldUpdater iFieldUpdater)
        {
            _iFieldUpdater = iFieldUpdater;
        }

        public void UpdateItem(ImportItem importItem)
        {
            var id = SitecoreUtilities.ParseId(importItem.ID);

            if (!id.IsNull)
            {
               
            }
        }

    }
}