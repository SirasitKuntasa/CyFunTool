namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class FunctionScoreViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public double DocumentationScore { get; set; } = 0;
    public double ImplementationScore { get; set; } = 0;
    public double AverageScore { get; set; } = 0;
    public List<CategoryScoreViewModel> Categories { get; set; } = new List<CategoryScoreViewModel>();
}