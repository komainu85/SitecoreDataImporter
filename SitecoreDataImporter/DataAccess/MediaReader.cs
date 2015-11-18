using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Contracts;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class MediaReader : IMediaReader
    {
        public MediaItem GetMediaItem(Guid itemId)
        {
            return (MediaItem)Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(itemId));
        }
    }
}
