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
        IItemCreator _itemCreator;
        IMediaReader _mediaReader;
        IFieldReader _fieldReader;
        IAuditCreator _auditCreator;
        IItemReader _itemReader;
        IItemUpdater _itemUpdater;
        IAuditReader _auditReader;

        public ItemRespository(IItemCreator itemCreator, IMediaReader mediaReader, IFieldReader fieldReader, IAuditCreator auditCreator, IItemReader itemReader, IItemUpdater itemUpdater,IAuditReader auditReader)
        {
            this._itemCreator = itemCreator;
            this._mediaReader = mediaReader;
            this._fieldReader = fieldReader;
            this._auditCreator = auditCreator;
            this._itemReader = itemReader;
            this._itemUpdater = itemUpdater;
            this._auditReader = auditReader;
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
            var mediaItem = _mediaReader.GetMediaItem(entity.MediaItemId);

            var importItems = _fieldReader.GetFieldsFromMediaItem(mediaItem);

            var importResults = new List<Item>();

            foreach (var importItem in importItems)
            {
                _itemCreator.ParentItemId = entity.ParentId;
                _itemCreator.TemplateId = entity.TemplateId;

                var newItem = _itemCreator.CreateItem(importItem);

                importResults.Add(newItem);
            }

            _auditCreator.CreateAudit(importResults, mediaItem.ID);
        }

        public bool Exists(DataItem entity)
        {
            var parent = _itemReader.GetItem(entity.ParentId.ToString());

            return _itemReader.ItemExists(parent, entity.Name);
        }

        public void Update(DataItem entity)
        {
            _itemUpdater.UpdateItem(entity);
        }

        public void Delete(DataItem entity)
        {
            throw new NotImplementedException();
        }

        public ImportAudit GetImportAudit(string mediaItemId)
        {
            return _auditReader.GetLatestAudit(mediaItemId);
        }
    }
}
