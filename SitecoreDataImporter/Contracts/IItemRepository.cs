using MikeRobbins.SitecoreDataImporter.Entities;
using Sitecore.Services.Core;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IItemRepository<T> where T : IEntityIdentity
    {
        void Add(DataItem entity);

        bool Exists(DataItem entity);

        void Update(DataItem entity);

        ImportAudit GetImportAudit(string fileName);
    }
}
