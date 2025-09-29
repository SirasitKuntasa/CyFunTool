using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class MaturityViewModel : BaseViewModel
{
    public ICollection<AssessmentViewModel> Assessments { get; set; }
    public ICollection<MaturityLevelViewModel> MaturityLevels { get; set; }
    public int Threshold { get; set; }
    public string Description { get; set; }
}