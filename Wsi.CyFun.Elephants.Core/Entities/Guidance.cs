using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Guidance : BaseEntity
    {
        public string Description { get; set; }
        public int Order { get; set; }
        public Requirement Requirement { get; set; }
        public Guid RequirementId { get; set; }
        public bool IsTitle { get; set; }
    }
}
