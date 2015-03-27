using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.SitecoreDataImporter.DataAccess;
using MikeRobbins.SitecoreDataImporter.Entities;
using MikeRobbins.SitecoreDataImporter.Interfaces;
using MikeRobbins.SitecoreDataImporter.Parsers;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Data;
using StructureMap.Configuration.DSL;

namespace MikeRobbins.SitecoreDataImporter.IoC
{
    public class IoCRegistry : Registry
    {
        public IoCRegistry()
        {
            For<IParser>().Use<CsvParse>().Named("CSV");
            For<IItemCreator>().Use<ItemCreator>();
            For<IMediaReader>().Use<MediaReader>();
            For<IFieldUpdater>().Use<FieldUpdater>();
            For<IFieldReader>().Use<FieldReader>();
            For<IItemUpdater>().Use<ItemUpdater>();
            For<IItemReader>().Use<ItemReader>();
            For<IAuditCreator>().Use<AuditCreator>();
            For<IAuditReader>().Use<AuditReader>();
            For(typeof(ICustomRepositoryActions<>)).Use(typeof(MikeRobbins.SitecoreDataImporter.Repositories.ItemRespository));
        }
    }
}
