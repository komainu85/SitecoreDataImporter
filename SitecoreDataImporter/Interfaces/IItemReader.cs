using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface IItemReader
    {
        Item GetItem(ID id);
        Item GetItem(string id);
        TemplateItem GetTemplateItem(string id);
    }
}