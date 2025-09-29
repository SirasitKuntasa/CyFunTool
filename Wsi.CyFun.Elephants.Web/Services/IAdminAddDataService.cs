using Microsoft.AspNetCore.Mvc.Rendering;
using Wsi.CyFun.Elephants.Core.Entities;

namespace Wsi.CyFun.Elephants.Web.Services;

public interface IAdminAddDataService
{
   List<SelectListItem> GetFunctions();
   List<SelectListItem> GetCategories();
   List<SelectListItem> GetSubCategories();
   List<SelectListItem> GetRequirements();
}