using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Assessment : BaseEntity
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Municipality Municipality { get; set; }
        public Guid MunicipalityId { get; set; }
        public ApplicationUser Assessor { get; set; }
        public Guid AssessorId { get; set; }
        public Maturity Maturity { get; set; }
        public Guid MaturityId { get; set; }
        public ICollection<Score> Scores { get; set; }

    }
}
