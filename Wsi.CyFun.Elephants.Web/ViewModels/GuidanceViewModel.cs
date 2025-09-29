using Wsi.CyFun.Elephants.Web.Models;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class GuidanceViewModel : BaseViewModel
{
    public string Description { get; set; }
    public string Code { get; set; }
    public int Order { get; set; }
    public bool IsTitle { get; set; }
}