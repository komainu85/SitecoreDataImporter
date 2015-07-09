using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Sites;

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
