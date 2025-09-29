namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class SubCategoryScoreViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public double DocumentationScore { get; set; }
    public double ImplementationScore { get; set; }
    public double AverageScore { get; set; }
    public bool IsKeyMeasure { get; set; }
    public List<SubCategoryViewModel> SubCategories { get; set; } = new List<SubCategoryViewModel>();
}