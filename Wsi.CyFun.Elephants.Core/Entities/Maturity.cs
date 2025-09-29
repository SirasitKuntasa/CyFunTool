using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Maturity: BaseEntity
    {
        public ICollection<Assessment> Assessments { get; set; }
        public ICollection<MaturityLevel> MaturityLevels { get; set; }
        public int Threshold { get; set; }
        public string Description { get; set; }

        public string Name { get; set; }
    }
}
