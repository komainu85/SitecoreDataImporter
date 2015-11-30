using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.Contracts;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.IoC;
using Sitecore.Data.Items;
using StructureMap;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class FieldReader : IFieldReader
    {
        private Container _container = null;

        public FieldReader()
        {
           _container =new Container(new IoCRegistry());
        }

        public List<ImportItem> GetFieldsFromMediaItem(MediaItem file)
        {
            IParser parser = null;

            parser = _container.GetInstance<IParser>(file.Extension.ToUpper());

            parser.MediaFile = file;

            var importItems = parser.ParseMediaItem();

            return importItems;
        }
    }
}
