using System.Collections.Generic;

namespace MikeRobbins.SitecoreDataImporter.Interfaces
{
    public interface ISitecoreDataAccess
    {
        Item CreateSitecoreItem(TemplateItem template, Item parentItem, string itemName, Dictionary<string, string> fields, Language language);
        Item DoesNameAtLevelExist(Item parentItem, string itemName);
        Language GetLanguage(string languageName);
    }
}