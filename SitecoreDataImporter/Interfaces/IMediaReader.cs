using System;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IMediaReader
    {
        MediaItem GetMediaItem(Guid itemId);
    }
}