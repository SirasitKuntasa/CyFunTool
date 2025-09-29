using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class RequirementsViewModel : BaseViewModel
{
    public string Description { get; set; }
    public string Code { get; set; }
    public int Order { get; set; }
    public bool IsKeyMeasurement { get; set; }
    public Guid SubCategoryId { get; set; }
    public ICollection<GuidanceViewModel> Guidances { get; set; } = new List<GuidanceViewModel>();
    public int? DocumentationMaturityScore { get; set; }
    public int? ImplementationMaturityScore { get; set; }
    public string AdditionalInfo { get; set; }
    public string AssessorComment { get; set; }
}
