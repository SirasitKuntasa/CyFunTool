using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Models;
    
namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class CategoryViewModel : BaseViewModel
{
    public string Description { get; set; }
    public string Code { get; set; }
    public int Order { get; set; }
    public ICollection<SubCategoryViewModel> SubCategories { get; set; } = new List<SubCategoryViewModel>();
    
}