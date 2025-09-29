using Wsi.CyFun.Elephants.Core.Entities;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class FunctionIndexViewModel
{
        public ICollection<FunctionViewModel> Functions { get; set; } = new List<FunctionViewModel>();

}