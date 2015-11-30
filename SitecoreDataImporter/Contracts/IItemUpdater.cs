using MikeRobbins.SitecoreDataImporter.Entities;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IItemUpdater
    {
        void UpdateItem(DataItem dataItem);
    }
}