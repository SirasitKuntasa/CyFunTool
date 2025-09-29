using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class MaturityLevel : BaseEntity
    {
        public int Level { get; set; }
        public int Value { get; set; }
        public string Documentation { get; set; }
        public string Implementation { get; set; }
        public Maturity Maturity { get; set; }
        public Guid MaturityId { get; set; }
    }
}
