using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels
{
    public class RequirementAddRequirementViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "Vul een beschrijving in")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vul een geldige code in")]
        [MaxLength(10)]
        public string Code { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Getal moet 0 of hoger zijn")]
        public int Order { get; set; }

        public Guid SelectedSubcategoryId { get; set; }
        public IEnumerable<SelectListItem> Subcategories { get; set; }
    }
}
