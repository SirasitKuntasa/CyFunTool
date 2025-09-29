using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wsi.CyFun.Elephants.Core.Entities
{
    public class Function : BaseEntity
    {
        [Required]
        public string Description { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
