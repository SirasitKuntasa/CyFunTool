namespace Wsi.CyFun.Elephants.Web.ViewModels;


public class RequirementScoreViewModel
{
    public Guid RequirementId { get; set; }  
    public int DocumentationMaturityScore { get; set; }
    public int ImplementationMaturityScore { get; set; }
    public string AdditionalInfo { get; set; }
    public string AssessorComment { get; set; }
}