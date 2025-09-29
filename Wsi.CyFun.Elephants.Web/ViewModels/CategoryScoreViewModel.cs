namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class CategoryScoreViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public double DocumentationScore { get; set; }
    public double ImplementationScore { get; set; }
    public double AverageScore { get; set; }
    public List<SubCategoryScoreViewModel> SubCategories { get; set; } = new List<SubCategoryScoreViewModel>();
}