using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class SubCategoryViewModel : BaseViewModel
{
    public string Description { get; set; }
    public string Code { get; set; }
    public int Order { get; set; }
    public ICollection<RequirementsViewModel> Requirements { get; set; } = new List<RequirementsViewModel>();
    
}