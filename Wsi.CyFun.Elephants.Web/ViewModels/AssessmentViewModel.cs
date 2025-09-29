using Microsoft.AspNetCore.Mvc.Rendering;
using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class AssessmentViewModel : BaseViewModel
{
    public List<SelectListItem> DocumentationScore { get; set; }
    public Guid SelectedDocumentationScoreId{ get; set; }

    public List<SelectListItem> ImplementationScore { get; set; }
    public Guid SelectedImplementationScoreId{ get; set; }
    
    public string AdditionalInfo { get; set; }
    public string AssessorComment { get; set; }
    
    public Guid AssessorId { get; set; }
    public Guid MaturityId { get; set; }
    
    public ICollection<Score> Scores { get; set; }
    

}