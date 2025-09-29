using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class FunctionViewModel : BaseViewModel
{
    public string Description { get; set; }
    public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    public Guid AssessorId { get; set; }  // Add this
    public Guid MaturityId { get; set; } 
    public string Code { get; set; }
    public int Order { get; set; }
    public List<RequirementScoreViewModel> Scores { get; set; } = new List<RequirementScoreViewModel>();

}