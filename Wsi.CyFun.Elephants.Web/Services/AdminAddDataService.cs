using Microsoft.AspNetCore.Mvc.Rendering;
using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Data;

namespace Wsi.CyFun.Elephants.Web.Services;

public class AdminAddDataService : IAdminAddDataService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AdminAddDataService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }


  
    public List<SelectListItem> GetFunctions()
    {
        return _applicationDbContext.Functions
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,

            }).ToList();
    }

    public List<SelectListItem> GetCategories()
    {
        return _applicationDbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,

            }).ToList();
    }

    public List<SelectListItem> GetSubCategories()
    {
        return _applicationDbContext.SubCategories
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Code,

            }).ToList();  
    }

    public List<SelectListItem> GetRequirements()
    {
        return _applicationDbContext.Requirements
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Code,

            }).ToList();
    }
}