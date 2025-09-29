using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Score
    {
        public Assessment Assessment { get; set; }
        public Guid AssessmentId { get; set; }
        public Requirement Requirement { get; set; }
        public Guid RequirementId { get; set; }
        public int? DocumentationMaturityScore { get; set; }
        public int? ImplementationMaturityScore { get; set; }
        public string AdditionalInfo { get; set; }
        public string AssessorComment { get; set; }

    }
}
