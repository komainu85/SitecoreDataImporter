using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikeRobbins.SitecoreDataImporter.IoC
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            For<MikeRobbins.SitecoreDataImporter.Interfaces.IParser>().Add<SitecoreDataImporter.BusinessLogic.Parsers.CsvParse>().Named("CSV");
        }
    }
}
