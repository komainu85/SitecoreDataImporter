using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IFieldReader
    {
        Dictionary<string, string> GetFieldsFromMediaItem(MediaItem file);
    }
}