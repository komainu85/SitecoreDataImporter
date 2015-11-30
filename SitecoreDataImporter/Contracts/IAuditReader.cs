using MikeRobbins.SitecoreDataImporter.Entities;

namespace MikeRobbins.SitecoreDataImporter.Contracts
{
    public interface IAuditReader
    {
        ImportAudit GetLatestAudit(string fileName);
    }
}
