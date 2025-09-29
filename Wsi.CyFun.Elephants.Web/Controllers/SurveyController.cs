using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Data;
using Wsi.CyFun.Elephants.Web.Models;
using Wsi.CyFun.Elephants.Web.Services;
using Wsi.CyFun.Elephants.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IAdminAddDataService _adminAddDataService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SurveyController(ApplicationDbContext applicationDbContext, IAdminAddDataService adminAddDataService,
            IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _adminAddDataService = adminAddDataService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index(Guid id)
        {
            var functions = _applicationDbContext.Functions
                .ToList();

            if (functions == null)
            {
                return NotFound("No functions found in database!");
            }

            FunctionIndexViewModel functionIndexViewModel = new FunctionIndexViewModel();

            foreach (var function in functions)
            {
                functionIndexViewModel.Functions.Add(new FunctionViewModel()
                {
                    Id = function.Id,
                    Name = function.Name,
                    Code = function.Code,
                });
            }

            return View(functionIndexViewModel);
        }

        [HttpGet]
        public IActionResult ShowSurvey(Guid id)
        {
            var allMunicipalities = _applicationDbContext.Municipalities.OrderBy(m => m.Name).ToList();
            ViewBag.AllMunicipalities = allMunicipalities;
            ViewBag.FunctionId = id;

            string currentAssessmentId = HttpContext.Session?.GetString("AssessmentId");

            var function = _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(f => f.SubCategories)
                .ThenInclude(f => f.Requirements)
                .ThenInclude(f => f.Guidances)
                .FirstOrDefault(f => f.Id.Equals(id));

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

            if (!string.IsNullOrEmpty(currentAssessmentId))
            {
                var assessment = _applicationDbContext.Assessments
                    .Include(a => a.Scores)
                    .FirstOrDefault(a => a.Id == Guid.Parse(currentAssessmentId));

                foreach (var category in functionViewModel.Categories)
                {
                    foreach (var subCategory in category.SubCategories)
                    {
                        foreach (var requirement in subCategory.Requirements)
                        {
                            var score = assessment?.Scores
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
            }

            return View(functionViewModel);
        }
        
        [HttpPost]
        public IActionResult ChangeMunicipality(Guid municipalityId, Guid functionId)
        {
            HttpContext.Session.SetString("SelectedMunicipalityId", municipalityId.ToString());
        
            var allMunicipalities = _applicationDbContext.Municipalities.OrderBy(m => m.Name).ToList();
            ViewBag.AllMunicipalities = allMunicipalities;
            ViewBag.FunctionId = functionId;
        
            var selectedMunicipality = allMunicipalities.FirstOrDefault(m => m.Id == municipalityId);
            ViewBag.SelectedMunicipality = selectedMunicipality;
        
            string currentAssessmentId = HttpContext.Session?.GetString("AssessmentId");
        
            var function = _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(f => f.SubCategories)
                .ThenInclude(f => f.Requirements)
                .ThenInclude(f => f.Guidances)
                .FirstOrDefault(f => f.Id.Equals(functionId));
        
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
        
            if (!string.IsNullOrEmpty(currentAssessmentId))
            {
                var assessment = _applicationDbContext.Assessments
                    .Include(a => a.Scores)
                    .FirstOrDefault(a => a.Id == Guid.Parse(currentAssessmentId));
        
                foreach (var category in functionViewModel.Categories)
                {
                    foreach (var subCategory in category.SubCategories)
                    {
                        foreach (var requirement in subCategory.Requirements)
                        {
                            var score = assessment?.Scores
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
            }
        
            return View("ShowSurvey", functionViewModel);
        }
        

        [HttpPost]
        public IActionResult SaveSurvey(AssessmentViewModel model)
        {
            try
            {
                // Hardcoded AssessorId and MaturityId
                Guid assessorId = Guid.Parse("a93e1d4b-2c58-4f76-87a1-ccf5a9d1b3e4"); // Peter // mag weg
                Guid maturityId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"); // Initial // mag weg

                // Haal de gekozen gemeente uit de session
                string selectedMunicipalityId = HttpContext.Session.GetString("SelectedMunicipalityId");
                if (string.IsNullOrEmpty(selectedMunicipalityId))
                {
                    ModelState.AddModelError("", "Geen gemeente geselecteerd.");
                    return RedirectToAction("SelectMunicipality", new { functionId = model.Id });
                }
                Guid municipalityId = Guid.Parse(selectedMunicipalityId);

                // Retrieve assessor, maturity, and municipality from the database
                var assessor = _applicationDbContext.ApplicationUsers.FirstOrDefault(u => u.Id == assessorId);
                var maturity = _applicationDbContext.Maturities.FirstOrDefault(m => m.Id == maturityId);
                var municipality = _applicationDbContext.Municipalities.FirstOrDefault(m => m.Id == municipalityId);

                if (assessor == null || municipality == null || maturity == null)
                {
                    // If assessor or maturity is not found, return an error
                    ModelState.AddModelError("", "Invalid AssessorId, MaturityId of Gemeente");
                    return View(model);
                }

                //haal assessment id uit session
                string assessmentIdString = HttpContext.Session.GetString("AssessmentId");

                Assessment assessment;

                if (string.IsNullOrEmpty(assessmentIdString))
                {
                    // als er geen is maak je een nieuwe aan
                    assessment = new Assessment
                    {
                        Id = Guid.NewGuid(),
                        AssessorId = assessor.Id,
                        MaturityId = maturity.Id,
                        MunicipalityId = municipality.Id,
                        ApplicationUserId = Guid.Parse(HttpContext.Session.GetString("UserId")),
                        Maturity = maturity,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    _applicationDbContext.Assessments.Add(assessment);
                    _applicationDbContext.SaveChanges(); // Save changes here to get the Id

                    // sla id op in session - direct via HttpContext
                    HttpContext.Session.SetString("AssessmentId", assessment.Id.ToString());
                }
                else
                {
                    // als id al bestaat, haal op uit database
                    assessment = _applicationDbContext.Assessments
                        .Include(a => a.Scores) // Include scores for efficiency
                        .FirstOrDefault(a => a.Id == Guid.Parse(assessmentIdString));
                }

                //scores toevoegen aan assessment
                foreach (var scoreViewModel in model.Scores)
                {
                    // check of de score al bestaat voor assessment
                    var existingScore = _applicationDbContext.Scores.FirstOrDefault(s =>
                        s.AssessmentId == assessment.Id && s.RequirementId == scoreViewModel.RequirementId);

                    if (existingScore != null)
                    {
                        // update de score als het al bestaat
                        existingScore.DocumentationMaturityScore = scoreViewModel.DocumentationMaturityScore;
                        existingScore.ImplementationMaturityScore = scoreViewModel.ImplementationMaturityScore;
                        existingScore.AdditionalInfo = scoreViewModel.AdditionalInfo;
                        existingScore.AssessorComment = scoreViewModel.AssessorComment;
                    }
                    else
                    {
                        // maak een nieuwe score aan als het nog niet bestaat
                        var newScore = new Score
                        {
                            AssessmentId = assessment.Id,
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

                HttpContext.Session.SetString("AssessmentId", assessment.Id.ToString());

                // Gemeente opnieuw laten kiezen bij volgende formulier
                HttpContext.Session.Remove("SelectedMunicipalityId");

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