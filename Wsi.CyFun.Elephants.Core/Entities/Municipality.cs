using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Municipality : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Assessment> Assessments { get; set; }
    }
}
