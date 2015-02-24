using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikeRobbins.SitecoreDataImporter.Entities
{
    public class DataItem : Sitecore.Services.Core.Model.EntityIdentity
    {
        public string Name { get; set; }
        public Guid TemplateId { get; set; }
        public Guid ParentId { get; set; }
        public Guid MediaItem { get; set; }
    }
}
