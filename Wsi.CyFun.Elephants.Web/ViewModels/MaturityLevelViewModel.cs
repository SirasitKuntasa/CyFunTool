using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class MaturityLevelViewModel : BaseViewModel
{
    public int Level { get; set; }
    public int Value { get; set; }
    public string Documentation { get; set; }
    public string Implementation { get; set; }
    public MaturityViewModel Maturity { get; set; }
    public Guid MaturityId { get; set; }
}