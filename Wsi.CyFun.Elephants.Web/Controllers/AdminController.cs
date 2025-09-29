using Microsoft.AspNetCore.Mvc;
using Wsi.CyFun.Elephants.Web.ViewModels;
using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Data;

using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Wsi.CyFun.Elephants.Web.Services;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IAdminAddDataService _adminAddDataService;

        public AdminController(ApplicationDbContext applicationDbContext, IAdminAddDataService adminAddDataService)
        {
            _applicationDbContext = applicationDbContext;
            _adminAddDataService = adminAddDataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult Functions()
        {
            var functions = _applicationDbContext.Functions
                .OrderBy(f => f.Order)
                .ToList();

            var functionViewModels = functions.Select(f => new FunctionViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                Order = f.Order,
                Code = f.Code,
                Created = f.Created,
                Deleted = f.Deleted,
                Updated = f.Updated
            }).ToList();

            var functionIndexViewModel = new FunctionIndexViewModel
            {
                Functions = functionViewModels
            };

            return View("Functions", functionIndexViewModel);
        }
        public IActionResult AddFunction(Guid? id)
        {
            var functionAddFunctionViewModel = new FunctionAddFunctionViewModel();

            if (id.HasValue)
            {
                var functions = _applicationDbContext.Functions
                    .FirstOrDefault(c => c.Id == id);

                if (functions != null)
                {
                    functionAddFunctionViewModel.Name = functions.Name;
                    functionAddFunctionViewModel.Code = functions.Code;
                    functionAddFunctionViewModel.Description = functions.Description;
                    functionAddFunctionViewModel.Order = functions.Order;
                    functionAddFunctionViewModel.Id = functions.Id;
                }
            }

            return View("AddFunction", functionAddFunctionViewModel);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveFunction(FunctionAddFunctionViewModel functionAddFunctionViewModel)
        {
            if (ModelState.IsValid)
            {
                var functions = _applicationDbContext.Functions.ToList();
                var newFunction = new Function()
                {
                    Id = Guid.NewGuid(),
                    Name = functionAddFunctionViewModel.Name,
                    Description = functionAddFunctionViewModel.Description,
                    Order = functionAddFunctionViewModel.Order,
                    Code = functionAddFunctionViewModel.Code,
                    Created = DateTime.Now,
                    Deleted = DateTime.Now,
                    Updated = DateTime.Now
                };

                _applicationDbContext.Functions.Add(newFunction);
                _applicationDbContext.SaveChanges();
            }

            return RedirectToAction("Functions");
        }
        
        [HttpGet]
        public IActionResult UpdateFunction(Guid id)
        {
            var function = _applicationDbContext.Functions
                .FirstOrDefault(f => f.Id == id);

            if (function == null)
            {
                return NotFound();
            }

            var functionAddFunctionViewModel = new FunctionAddFunctionViewModel(){
            
                Id = function.Id,
                Name = function.Name,
                Description = function.Description,
                Code = function.Code,
                Order = function.Order,
                
            };

            return View("UpdateFunction", functionAddFunctionViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFunction(FunctionAddFunctionViewModel functionAddFunctionViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingFunction = _applicationDbContext.Functions 
                    .FirstOrDefault(c => c.Id == functionAddFunctionViewModel.Id);

                if (existingFunction != null)
                {
                    existingFunction.Name = functionAddFunctionViewModel.Name;
                    existingFunction.Description = functionAddFunctionViewModel.Description;
                    existingFunction.Code = functionAddFunctionViewModel.Code;
                    existingFunction.Order = functionAddFunctionViewModel.Order; 
                    existingFunction.Updated = DateTime.Now;

                    _applicationDbContext.Functions.Update(existingFunction);
                    _applicationDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Functions");
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var function = await _applicationDbContext.Functions
                .FirstOrDefaultAsync(m => m.Id == id);

            if (function == null) return NotFound();

            var functionViewModel = new FunctionViewModel
            {
                Name = function.Name,
              
            };

            return View(functionViewModel);
        }

   
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var function = await _applicationDbContext.Functions.FindAsync(id);
            if (function != null)
            {
                _applicationDbContext.Functions.Remove(function);
                await _applicationDbContext.SaveChangesAsync();
            }

           

            return RedirectToAction("Functions");
        }
        
        
        
        
        
        [HttpGet]
        public IActionResult Categories()
        {
            var categories = _applicationDbContext.Categories
                .OrderBy(c => c.Order)
                .ToList();

            var categoriesViewModels = categories.Select(f => new CategoryViewModel()
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                Order = f.Order,
                Code = f.Code,
                Created = f.Created,
                Deleted = f.Deleted,
                Updated = f.Updated
            }).ToList();

            var functionViewModel = new FunctionViewModel()
            {
                Categories = categoriesViewModels
            };


            return View("Categories", functionViewModel);
        }
        public IActionResult AddCategory(Guid? id)
        {
            var categoryAddCategoryViewModel = new CategoryAddCategoryViewModel();
            categoryAddCategoryViewModel.Functions = _adminAddDataService.GetFunctions();

            if (id.HasValue)
            {
                var category = _applicationDbContext.Categories
                    .FirstOrDefault(c => c.Id == id);

                if (category != null)
                {
                    categoryAddCategoryViewModel.Name = category.Name;
                    categoryAddCategoryViewModel.Code = category.Code;
                    categoryAddCategoryViewModel.Description = category.Description;
                    categoryAddCategoryViewModel.Order = category.Order;
                    categoryAddCategoryViewModel.SelectedFunctionId = category.FunctionId;
                    categoryAddCategoryViewModel.Id = category.Id;
                }
            }

            return View("AddCategory", categoryAddCategoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(CategoryAddCategoryViewModel categoryAddCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var functions = _applicationDbContext.Categories.ToList();
                var newCategory = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = categoryAddCategoryViewModel.Name,
                    Description = categoryAddCategoryViewModel.Description,
                    Order = categoryAddCategoryViewModel.Order,
                    Code = categoryAddCategoryViewModel.Code,
                    Created = DateTime.Now,
                    Deleted = DateTime.Now,
                    Updated = DateTime.Now,
                    FunctionId = categoryAddCategoryViewModel.SelectedFunctionId,

                };

                _applicationDbContext.Categories.Add(newCategory);
                _applicationDbContext.SaveChanges();
            }

            return RedirectToAction("Categories");
        }
        [HttpGet]
        public IActionResult UpdateCategory(Guid id)
        {
            var category = _applicationDbContext.Categories
                .FirstOrDefault(f => f.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryAddCategoryViewModel = new CategoryAddCategoryViewModel(){
            
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Code = category.Code,
                Order = category.Order,
                SelectedFunctionId = category.FunctionId, 
                Functions = _adminAddDataService.GetFunctions()
            };

            return View("UpdateCategory", categoryAddCategoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(CategoryAddCategoryViewModel categoryAddCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _applicationDbContext.Categories 
                    .FirstOrDefault(c => c.Id == categoryAddCategoryViewModel.Id);

                if (existingCategory != null)
                {
                    existingCategory.Name = categoryAddCategoryViewModel.Name;
                    existingCategory.Description = categoryAddCategoryViewModel.Description;
                    existingCategory.Code = categoryAddCategoryViewModel.Code;
                    existingCategory.FunctionId = categoryAddCategoryViewModel.SelectedFunctionId; 
                    existingCategory.Order = categoryAddCategoryViewModel.Order; 
                    existingCategory.Updated = DateTime.Now;

                    _applicationDbContext.Categories.Update(existingCategory);
                    _applicationDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Categories");
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Guid? id)
        {
            if (id == null) return NotFound();

            var category = await _applicationDbContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();

            var categoryViewModel = new CategoryViewModel
            {
                Name = category.Name,
            };

            return View("DeleteCategory", categoryViewModel);
        }

        [HttpPost, ActionName("DeleteCategoryConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(Guid id)
        {
            var category = await _applicationDbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _applicationDbContext.Categories.Remove(category);
                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Categories");
        }
        
        
        
        
        
        
        
        public IActionResult Subcategories()
        
        {
            var subcategories = _applicationDbContext.SubCategories
                .OrderBy(s => s.Order)
                .ToList();

            var subCategoryViewModels = subcategories.Select(f => new SubCategoryViewModel()
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                Order = f.Order,
                Code = f.Code,
                Created = f.Created,
                Deleted = f.Deleted,
                Updated = f.Updated
            }).ToList();

            var categoriesViewModels = new CategoryViewModel()
            {
                SubCategories = subCategoryViewModels
            };
      

            return View("Subcategories", categoriesViewModels);
        }

        public IActionResult AddSubcategory(Guid? id)
        {

            var subcategoryAddSubcategoryViewModel = new SubcategoryAddSubcategoryViewModel();
            subcategoryAddSubcategoryViewModel.Categories = _adminAddDataService.GetCategories();
            
            var subcategory = _applicationDbContext.SubCategories
                .FirstOrDefault(f => f.Id == id);


            if (id.HasValue)
            {
                 subcategory = _applicationDbContext.SubCategories
                    .FirstOrDefault(c => c.Id == id);

                if (subcategory != null)
                {
                    subcategoryAddSubcategoryViewModel.Name = subcategory.Name;
                    subcategoryAddSubcategoryViewModel.Code = subcategory.Code;
                    subcategoryAddSubcategoryViewModel.Description = subcategory.Description;
                    subcategoryAddSubcategoryViewModel.Order = subcategory.Order;
                    subcategoryAddSubcategoryViewModel.SelectedCategoryId = subcategory.CategoryId;
                    subcategoryAddSubcategoryViewModel.Id = subcategory.Id;
                }
            }

            return View("AddSubcategory", subcategoryAddSubcategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSubcategory(SubcategoryAddSubcategoryViewModel subcategoryAddSubcategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var subcategories = _applicationDbContext.SubCategories.ToList();
                var newSubcategory = new SubCategory()
                {
                    Id = Guid.NewGuid(),
                    Name = subcategoryAddSubcategoryViewModel.Name,
                    Description = subcategoryAddSubcategoryViewModel.Description,
                    Order = subcategoryAddSubcategoryViewModel.Order,
                    Code = subcategoryAddSubcategoryViewModel.Code,
                    Created = DateTime.Now,
                    Deleted = DateTime.Now,
                    Updated = DateTime.Now,
                    CategoryId = subcategoryAddSubcategoryViewModel.SelectedCategoryId,

                };

                _applicationDbContext.SubCategories.Add(newSubcategory);
                _applicationDbContext.SaveChanges();
            }

            return RedirectToAction("Subcategories");
        }
        [HttpGet]
        public IActionResult UpdateSubcategory(Guid id)
        {
            var subcategories = _applicationDbContext.SubCategories
                .FirstOrDefault(f => f.Id == id);

            if (subcategories == null)
            {
                return NotFound();
            }

            var subcategoryAddSubcategoryViewModel = new SubcategoryAddSubcategoryViewModel(){
            
                Id = subcategories.Id,
                Name = subcategories.Code,
                Description = subcategories.Description,
                Code = subcategories.Code,
                Order = subcategories.Order,
                SelectedCategoryId = subcategories.CategoryId, 
                Categories = _adminAddDataService.GetCategories()
            };

            return View("UpdateSubcategory", subcategoryAddSubcategoryViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSubcategory(SubcategoryAddSubcategoryViewModel subcategoryAddSubcategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingSubcategory = _applicationDbContext.SubCategories 
                    .FirstOrDefault(c => c.Id == subcategoryAddSubcategoryViewModel.Id);

                if (existingSubcategory != null)
                {
                    existingSubcategory.Name = subcategoryAddSubcategoryViewModel.Name;
                    existingSubcategory.Code = subcategoryAddSubcategoryViewModel.Code;
                    existingSubcategory.Order = subcategoryAddSubcategoryViewModel.Order;
                    existingSubcategory.Description = subcategoryAddSubcategoryViewModel.Description;
                    existingSubcategory.CategoryId = subcategoryAddSubcategoryViewModel.SelectedCategoryId;
                    existingSubcategory.Updated = DateTime.Now;

                    _applicationDbContext.SubCategories.Update(existingSubcategory);
                    _applicationDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Subcategories");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSubcategory(Guid? id)
        {
            if (id == null) return NotFound();

            var subcategory = await _applicationDbContext.SubCategories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subcategory == null) return NotFound();

            var subcategoryViewModel = new SubCategoryViewModel
            {
                Name = subcategory.Name,
            };

            return View("DeleteSubcategory", subcategoryViewModel);
        }

        [HttpPost, ActionName("DeleteSubcategoryConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubcategoryConfirmed(Guid id)
        {
            var subcategory = await _applicationDbContext.SubCategories.FindAsync(id);
            if (subcategory != null)
            {
                _applicationDbContext.SubCategories.Remove(subcategory);
                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Subcategories");
        }

        
        
        
        [HttpGet]
        public IActionResult Requirements()
        {
            var requirements = _applicationDbContext.Requirements
                .OrderBy(r => r.Order)
                .ToList();

            var requirementsViewModels = requirements.Select(f => new RequirementsViewModel()
            {
                Id = f.Id,
                Name = f.Code,
                Description = f.Description,
                Order = f.Order,
                Code = f.Code,
                Created = f.Created,
                Deleted = f.Deleted,
                Updated = f.Updated
            }).ToList();

            var subCategoryViewModels = new SubCategoryViewModel()
            {
                Requirements = requirementsViewModels
            };
      

            return View("Requirements", subCategoryViewModels);
        }


        public IActionResult AddRequirement(Guid? id)
        {
            var requirementAddRequirementViewModel = new RequirementAddRequirementViewModel();
            requirementAddRequirementViewModel.Subcategories = _adminAddDataService.GetSubCategories();

            if (id.HasValue)
            {
                var requirement = _applicationDbContext.Requirements
                    .FirstOrDefault(c => c.Id == id);

                if (requirement != null)
                {
                    requirementAddRequirementViewModel.Code = requirement.Code;
                    requirementAddRequirementViewModel.Description = requirement.Description;
                    requirementAddRequirementViewModel.Order = requirement.Order;
                    requirementAddRequirementViewModel.SelectedSubcategoryId = requirement.SubCategoryId;
                    requirementAddRequirementViewModel.Id = requirement.Id;
                }
            }

            return View("AddRequirement", requirementAddRequirementViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveRequirement(RequirementAddRequirementViewModel requirementAddRequirementViewModel)
        {
            if (ModelState.IsValid)
            {
                var requirements = _applicationDbContext.Requirements.ToList();
                var newRequirement = new Requirement()
                {
                    Id = Guid.NewGuid(),
                    Description = requirementAddRequirementViewModel.Description,
                    Order = requirementAddRequirementViewModel.Order,
                    Code = requirementAddRequirementViewModel.Code,
                    Created = DateTime.Now,
                    Deleted = DateTime.Now,
                    Updated = DateTime.Now,
                    SubCategoryId = requirementAddRequirementViewModel.SelectedSubcategoryId,

                };

                _applicationDbContext.Requirements.Add(newRequirement);
                _applicationDbContext.SaveChanges();

            }


            return RedirectToAction("Requirements");


        }
        
        [HttpGet]
        public IActionResult UpdateRequirement(Guid id)
        {
            var requirements = _applicationDbContext.Requirements
                .FirstOrDefault(f => f.Id == id);

            if (requirements == null)
            {
                return NotFound();
            }

            var requirementAddRequirementViewModel = new RequirementAddRequirementViewModel(){
            
                Id = requirements.Id,
                Name = requirements.Code,
                Description = requirements.Description,
                Code = requirements.Code,
                Order = requirements.Order,
                SelectedSubcategoryId = requirements.SubCategoryId, 
                Subcategories = _adminAddDataService.GetSubCategories()
            };

            return View("UpdateRequirement", requirementAddRequirementViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        
        public IActionResult UpdateRequirement(RequirementAddRequirementViewModel requirementAddRequirementViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingRequirement = _applicationDbContext.Requirements 
                    .FirstOrDefault(c => c.Id == requirementAddRequirementViewModel.Id);

                if (existingRequirement != null)
                {
                    existingRequirement.Description = requirementAddRequirementViewModel.Description;
                    existingRequirement.Code = requirementAddRequirementViewModel.Code;
                    existingRequirement.SubCategoryId = requirementAddRequirementViewModel.SelectedSubcategoryId; 
                    existingRequirement.Order = requirementAddRequirementViewModel.Order; 
                    existingRequirement.Updated = DateTime.Now;

                    _applicationDbContext.Requirements.Update(existingRequirement);
                    _applicationDbContext.SaveChanges();
                }

            }

            return RedirectToAction("Requirements");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteRequirement(Guid? id)
        {
            if (id == null) return NotFound();

            var requirement = await _applicationDbContext.Requirements
                .FirstOrDefaultAsync(m => m.Id == id);

            if (requirement == null) return NotFound();

            var requirementsViewModel = new RequirementsViewModel
            {
                Code = requirement.Code,
            };

            return View("DeleteRequirement", requirementsViewModel);
        }

        [HttpPost, ActionName("DeleteRequirementConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRequirementConfirmed(Guid id)
        {
            var requirement = await _applicationDbContext.Requirements.FindAsync(id);
            if (requirement != null)
            {
                _applicationDbContext.Requirements.Remove(requirement);
                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Requirements");
        }










        [HttpGet]
        public IActionResult Guidances()
        {
            var guidances = _applicationDbContext.Guidances
                .OrderBy(g => g.Order)
                .ToList();

            var guidanceViewModels = guidances.Select(f => new GuidanceViewModel()
            {
                Id = f.Id,
                Description = f.Description,
                Order = f.Order,
                Created = f.Created,
                Deleted = f.Deleted,
                Updated = f.Updated
            }).ToList();

            var requirementViewModels = new RequirementsViewModel()
            {
                Guidances = guidanceViewModels
            };
      

            return View("Guidances", requirementViewModels);
        }


        public IActionResult AddGuidance(Guid? id)
        {
            var guidanceAddRequirementViewModel = new GuidanceAddGuidanceViewModel();
            guidanceAddRequirementViewModel.Requirements = _adminAddDataService.GetRequirements();

            if (id.HasValue)
            {
                var guidance = _applicationDbContext.Guidances
                    .FirstOrDefault(c => c.Id == id);

                if (guidance != null)
                {
 
                    guidanceAddRequirementViewModel.Description = guidance.Description;
                    guidanceAddRequirementViewModel.Order = guidance.Order;
                    guidanceAddRequirementViewModel.IsTitle = guidance.IsTitle;
                    guidanceAddRequirementViewModel.SelectedRequirementId = guidance.RequirementId;
                    guidanceAddRequirementViewModel.Id = guidance.Id;
                }
            }

            return View("AddGuidance", guidanceAddRequirementViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveGuidance(GuidanceAddGuidanceViewModel guidanceAddRequirementViewModel)
        {
            if (ModelState.IsValid)
            {
                var guidances = _applicationDbContext.Guidances.ToList();
                var newGuidance = new Guidance()
                {
                    Id = Guid.NewGuid(),
                    Description = guidanceAddRequirementViewModel.Description,
                    Order = guidanceAddRequirementViewModel.Order,
                    Created = DateTime.Now,
                    Deleted = DateTime.Now,
                    Updated = DateTime.Now,
                    IsTitle = guidanceAddRequirementViewModel.IsTitle,
                    RequirementId = guidanceAddRequirementViewModel.SelectedRequirementId,

                };

                _applicationDbContext.Guidances.Add(newGuidance);
                _applicationDbContext.SaveChanges();


            }
            return RedirectToAction("Guidances");

        }
        
        [HttpGet]
        public IActionResult UpdateGuidance(Guid id)
        {
            var guidances = _applicationDbContext.Guidances
                .FirstOrDefault(f => f.Id == id);

            if (guidances == null)
            {
                return NotFound();
            }

            var guidanceAddGuidanceViewModel = new GuidanceAddGuidanceViewModel(){
            
                Id = guidances.Id,
                Description = guidances.Description,
                Order = guidances.Order,
                SelectedRequirementId = guidances.RequirementId, 
                Requirements = _adminAddDataService.GetRequirements()
            };

            return View("UpdateGuidance", guidanceAddGuidanceViewModel);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateGuidance(GuidanceAddGuidanceViewModel guidanceAddGuidanceViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingGuidance = _applicationDbContext.Guidances 
                    .FirstOrDefault(c => c.Id == guidanceAddGuidanceViewModel.Id);

                if (existingGuidance != null)
                {
                    existingGuidance.Description = guidanceAddGuidanceViewModel.Description;
                    existingGuidance.RequirementId = guidanceAddGuidanceViewModel.SelectedRequirementId;
                    existingGuidance.Order = guidanceAddGuidanceViewModel.Order; 
                    existingGuidance.Updated = DateTime.Now;

                    _applicationDbContext.Guidances.Update(existingGuidance);
                    _applicationDbContext.SaveChanges();
                }

            }

            return RedirectToAction("Guidances");
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteGuidance(Guid? id)
        {
            if (id == null) return NotFound();

            var guidance = await _applicationDbContext.Guidances
                .FirstOrDefaultAsync(m => m.Id == id);

            if (guidance == null) return NotFound();

            var guidanceViewModel = new GuidanceViewModel
            {
                Description = guidance.Description,
            };

            return View("DeleteGuidance", guidanceViewModel);
        }

        [HttpPost, ActionName("DeleteGuidanceConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGuidanceConfirmed(Guid id)
        {
            var guidance = await _applicationDbContext.Guidances.FindAsync(id);
            if (guidance != null)
            {
                _applicationDbContext.Guidances.Remove(guidance);
                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Guidances");
        }

    }
}