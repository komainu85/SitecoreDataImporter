using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.Utilities;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class ItemUpdater : IItemUpdater
    {
        private IFieldUpdater _iFieldUpdater = null;
        private IItemReader _itemReader = null;

        public ItemUpdater(IFieldUpdater iFieldUpdater, IItemReader itemReader)
        {
            _iFieldUpdater = iFieldUpdater;
            _itemReader = itemReader;
        }

        public void UpdateItem(DataItem dataItem)
        {
            var item = _itemReader.GetItem(dataItem.Id);

            _iFieldUpdater.AddFieldsToItem(item,dataItem);
        }

    }
}