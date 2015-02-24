using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.DataAccess;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace MikeRobbins.SitecoreDataImporter.Repositories
{
    public class ItemRespository : Sitecore.Services.Core.IRepository<DataItem>
    {
        private Container _container;

        public ItemRespository()
        {
            _container = new Container(new Registry());
        }

        public IQueryable<DataItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public DataItem FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(DataItem entity)
        {
            IItemCreator itemCreator = _container.GetInstance<IItemCreator>();
            IMediaReader mediaReader = _container.GetInstance<IMediaReader>();
            IFieldReader fieldReader = _container.GetInstance<IFieldReader>();

            var mediaItem = mediaReader.GetMediaItem(entity.MediaItem);

            var fields = fieldReader.GetFieldsFromMediaItem(mediaItem);

            itemCreator.CreateItem(entity, fields);
        }

        public bool Exists(DataItem entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DataItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DataItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
