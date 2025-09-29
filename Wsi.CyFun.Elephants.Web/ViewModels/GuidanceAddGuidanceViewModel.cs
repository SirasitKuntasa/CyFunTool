using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels
{
    public class GuidanceAddGuidanceViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "Vul een beschrijving in")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Getal moet 0 of hoger zijn")]
        public int Order { get; set; }
        public bool IsTitle { get; set; }

        public Guid SelectedRequirementId { get; set; }
        public IEnumerable<SelectListItem> Requirements { get; set; }
    }
}
