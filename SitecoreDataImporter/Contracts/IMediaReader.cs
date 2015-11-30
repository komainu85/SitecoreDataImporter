using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IMediaReader
    {
        MediaItem GetMediaItem(Guid itemId);
    }
}