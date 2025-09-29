using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Data;
using Wsi.CyFun.Elephants.Web.Models;
using Wsi.CyFun.Elephants.Web.Services;
using Wsi.CyFun.Elephants.Web.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class AssessorController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IAdminAddDataService _adminAddDataService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AssessorController(ApplicationDbContext applicationDbContext, IAdminAddDataService adminAddDataService,
            IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _adminAddDataService = adminAddDataService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var assessments = _applicationDbContext.Assessments
                .Include(a => a.Municipality)
                .Include(a => a.ApplicationUser)
                .Include(a => a.Maturity)
                .ToList();

            var assessmentListViewModel = new AssessmentListViewModel
            {
                Assessments = assessments.Select(a => new AssessmentSummaryViewModel
                {
                    Id = a.Id,
                    MunicipalityName = a.Municipality?.Name,
                    UserName = a.ApplicationUser?.Username,
                    MaturityName = a.Maturity?.Name,
                    Created = a.Created
                }).ToList()
            };

            return View(assessmentListViewModel);
        }

        [HttpGet]
        public IActionResult ShowAssessment(Guid id)
        {
            var assessment = _applicationDbContext.Assessments
                .Include(a => a.Scores)
                .FirstOrDefault(a => a.Id == id);

            if (assessment == null)
            {
                return NotFound("Assessment not found!");
            }

            var function = _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(f => f.SubCategories)
                .ThenInclude(f => f.Requirements)
                .ThenInclude(f => f.Guidances)
                .FirstOrDefault();
            
            if (function == null)
            {
                return NotFound("Function not found!");
            }

            FunctionViewModel functionViewModel = new FunctionViewModel()
            {
                Id = function.Id,
                Name = function.Name,
                Description = function.Description,
                Order = function.Order,
                Code = function.Code,
                Categories = function.Categories.Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Code = c.Code,
                    Order = c.Order,
                    SubCategories = c.SubCategories.Select(s => new SubCategoryViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Code = s.Code,
                        Order = s.Order,
                        Requirements = s.Requirements.Select(r => new RequirementsViewModel
                        {
                            Id = r.Id,
                            Name = r.Code,
                            Description = r.Description,
                            Code = r.Code,
                            Order = r.Order,
                            IsKeyMeasurement = r.IsKeyMeasurment,
                            Guidances = r.Guidances.Select(g => new GuidanceViewModel
                            {
                                Id = g.Id,
                                Description = g.Description,
                                Order = g.Order,
                                IsTitle = g.IsTitle,
                            }).ToList(),
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            foreach (var category in functionViewModel.Categories)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    foreach (var requirement in subCategory.Requirements)
                    {
                        var score = assessment.Scores
                            .FirstOrDefault(sc => sc.RequirementId == requirement.Id);
                        
                        if (score != null)
                        {
                            requirement.DocumentationMaturityScore = score.DocumentationMaturityScore;
                            requirement.ImplementationMaturityScore = score.ImplementationMaturityScore;
                            requirement.AdditionalInfo = score.AdditionalInfo;
                            requirement.AssessorComment = score.AssessorComment;
                        }
                    }
                }
            }

            ViewBag.AssessmentId = assessment.Id;

            return View(functionViewModel);
        }

        [HttpPost]
        public IActionResult SaveAssessment(AssessmentViewModel model, Guid assessmentId)
        {
            try
            {
                Guid assessorId = Guid.Parse(HttpContext.Session.GetString("UserId")); 

                var assessment = _applicationDbContext.Assessments
                    .Include(a => a.Scores)
                    .FirstOrDefault(a => a.Id == assessmentId);

                if (assessment == null)
                {
                    ModelState.AddModelError("", "Assessment not found");
                    return View(model);
                }

                assessment.AssessorId = assessorId;

                foreach (var scoreViewModel in model.Scores)
                {
                    var existingScore = assessment.Scores
                        .FirstOrDefault(s => s.RequirementId == scoreViewModel.RequirementId);

                    if (existingScore != null)
                    {
                        existingScore.AssessorComment = scoreViewModel.AssessorComment;
                    }
                    else
                    {
                       
                        var newScore = new Score
                        {
                            AssessmentId = assessmentId,
                            RequirementId = scoreViewModel.RequirementId,
                            DocumentationMaturityScore = scoreViewModel.DocumentationMaturityScore,
                            ImplementationMaturityScore = scoreViewModel.ImplementationMaturityScore,
                            AdditionalInfo = scoreViewModel.AdditionalInfo,
                            AssessorComment = scoreViewModel.AssessorComment,
                        };
                        _applicationDbContext.Scores.Add(newScore);
                    }
                }

                _applicationDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}