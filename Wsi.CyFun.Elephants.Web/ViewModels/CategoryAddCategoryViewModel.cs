using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class CategoryAddCategoryViewModel : BaseViewModel
{
    [Required(ErrorMessage = "Vul een beschrijving in")]
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Vul een geldige naam in")]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Vul een geldige code in")]
    [MaxLength(10)]
    public string Code { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Getal moet 0 of hoger zijn")]
    public int Order { get; set; }
    
    
    
    public Guid SelectedFunctionId { get; set; }
    public IEnumerable<SelectListItem> Functions { get; set; }

}