using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Parsers
{
  public  class XmlParser : IParser
    {
      public MediaItem MediaFile { get; set; }
      public List<ImportItem> ParseMediaItem()
      {
          throw new NotImplementedException();
      }
    }
}
