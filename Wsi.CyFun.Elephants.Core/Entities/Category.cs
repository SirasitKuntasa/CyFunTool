using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Category: BaseEntity
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public Function Function { get; set; }
        public Guid FunctionId { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
            
    }
}
