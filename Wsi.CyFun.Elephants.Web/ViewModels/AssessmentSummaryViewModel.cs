namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class AssessmentSummaryViewModel
{
    public Guid Id { get; set; }
    public string MunicipalityName { get; set; }
    public string UserName { get; set; }
    public string MaturityName { get; set; }
    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }
}