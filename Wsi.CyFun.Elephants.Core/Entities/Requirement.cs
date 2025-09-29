using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Requirement:BaseEntity
    {
        public string Description { get; set; }
        public SubCategory SubCategory { get; set; }
        public Guid SubCategoryId { get; set; }
        public bool IsKeyMeasurment { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public ICollection<Score> Scores { get; set; }
        public ICollection<Guidance> Guidances { get; set; }


    }
}
