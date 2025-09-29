namespace Wsi.CyFun.Elephants.Web.ViewModels
{
    public class AllScoresViewModel
    {
        public  ICollection<ScoreChartFunctionViewModel> FunctionScores { get; set; }
        public  ICollection<ScoreChartCategoryViewModel> CategoryScores { get; set; }
        public  ICollection<ScoreChartSubcategoryViewModel> SubcategoryScores { get; set; }
        public  ICollection<ScoreChartRequirementViewModel> RequirementScores { get; set; }
    }
}
