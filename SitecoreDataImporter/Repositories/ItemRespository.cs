using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.DataAccess;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using Sitecore.Diagnostics.PerformanceCounters;
using StructureMap;
using MikeRobbins.SitecoreDataImporter.IoC;

namespace MikeRobbins.SitecoreDataImporter.Repositories
{
    public class ItemRespository : ICustomRepositoryActions<DataItem>
    {
        private Container _container;

        public ItemRespository()
        {
            _container = new Container(new IoCRegistry());
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
            IAuditCreator auditCreator = _container.GetInstance<IAuditCreator>();

            var mediaItem = mediaReader.GetMediaItem(entity.MediaItemId);

            var importItems = fieldReader.GetFieldsFromMediaItem(mediaItem);

            var importResults = new List<Item>();

            foreach (var importItem in importItems)
            {
                itemCreator.ParentItemId = entity.ParentId;
                itemCreator.TemplateId = entity.TemplateId;

                var newItem = itemCreator.CreateItem(importItem);

                importResults.Add(newItem);
            }

            auditCreator.CreateAudit(importResults);
        }

        public bool Exists(DataItem entity)
        {
            IItemReader itemReader = _container.GetInstance<IItemReader>();

            var parent = itemReader.GetItem(entity.ParentId.ToString());

            return itemReader.ItemExists(parent, entity.Name);
        }

        public void Update(DataItem entity)
        {
            IItemUpdater itemUpdater = _container.GetInstance<IItemUpdater>();

            itemUpdater.UpdateItem(entity);

        }

        public void Delete(DataItem entity)
        {
            throw new NotImplementedException();
        }

        public ImportAudit GetImportAudit()
        {
            IAuditReader auditReader = _container.GetInstance<IAuditReader>();

            return auditReader.GetLatestAudit();
        }
    }
}
