using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Data.Items;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.Parsers
{
    public class FieldReader : Interfaces.IFieldReader
    {
        public Dictionary<string,string> GetFieldsFromMediaItem(MediaItem file)
        {
            IParser parser = null;

            parser = ObjectFactory.GetNamedInstance<IParser>(file.Extension.ToUpper());

            parser.MediaFile = file;

            return parser.ParseMediaItem();
        }
    }
}
