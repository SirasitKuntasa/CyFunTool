using Microsoft.AspNetCore.Mvc;
using Wsi.CyFun.Elephants.Web.ViewModels;
using Wsi.CyFun.Elephants.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Wsi.CyFun.Elephants.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using ClosedXML.Excel;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class MaturityController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MaturityController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListAssessments");
        }

        public async Task<IActionResult> ListAssessments()
        {
            var assessments = await _applicationDbContext.Assessments
                .Include(a => a.Municipality)
                .Include(a => a.ApplicationUser)
                .OrderByDescending(a => a.Created)
                .ToListAsync();

            return View(assessments);
        }

        [HttpGet]
        public async Task<IActionResult> AssessmentScores(Guid id)
        {
            var assessment = await _applicationDbContext.Assessments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assessment == null)
                return NotFound();

            var scores = await _applicationDbContext.Scores
                .Where(s => s.AssessmentId == id)
                .ToListAsync();
                
            var functions = await _applicationDbContext.Functions
                .Include(f => f.Categories)
                    .ThenInclude(c => c.SubCategories)
                        .ThenInclude(sc => sc.Requirements)
                .OrderBy(f => f.Order)
                .ToListAsync();

            var viewModel = new List<FunctionScoreViewModel>();

            foreach (var function in functions)
            {
                var functionScores = new FunctionScoreViewModel
                {
                    Id = function.Id,
                    Name = function.Name,
                    Code = function.Code,
                    Description = function.Description
                };

                var categoryScores = new List<CategoryScoreViewModel>();

                foreach (var category in function.Categories.OrderBy(c => c.Order))
                {
                    var subCategoryScores = new List<SubCategoryScoreViewModel>();

                    foreach (var subCategory in category.SubCategories.OrderBy(s => s.Order))
                    {
                        var requirements = subCategory.Requirements.ToList();

                        double subCategoryDocTotal = 0;
                        double subCategoryImplTotal = 0;

                        foreach (var req in requirements)
                        {
                            var score = scores.FirstOrDefault(s => s.RequirementId == req.Id);
                            if (score != null)
                            {
                                subCategoryDocTotal += score.DocumentationMaturityScore ?? 0;
                                subCategoryImplTotal += score.ImplementationMaturityScore ?? 0;
                            }
                        }

                        double subCategoryDocScore = requirements.Count > 0
                            ? Math.Round(subCategoryDocTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryImplScore = requirements.Count > 0
                            ? Math.Round(subCategoryImplTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryAvgScore = Math.Round((subCategoryDocScore + subCategoryImplScore) / 2, 0);

                        var subCategoryScore = new SubCategoryScoreViewModel
                        {
                            Id = subCategory.Id,
                            Name = subCategory.Name,
                            Code = subCategory.Code,
                            Description = subCategory.Description,
                            DocumentationScore = subCategoryDocScore,
                            ImplementationScore = subCategoryImplScore,
                            AverageScore = subCategoryAvgScore,
                            IsKeyMeasure = subCategory.Requirements.Any(r => r.IsKeyMeasurment)
                        };

                        subCategoryScores.Add(subCategoryScore);
                    }

                    var categoryScore = new CategoryScoreViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Code = category.Code,
                        Description = category.Description,
                        SubCategories = subCategoryScores
                    };

                    categoryScores.Add(categoryScore);
                }

                functionScores.Categories = categoryScores;
                viewModel.Add(functionScores);
            }

            ViewBag.FunctionScores = viewModel;





            var functionsChart = _applicationDbContext.Functions
            .Include(s => s.Categories)
            .ThenInclude(s => s.SubCategories)
            .ThenInclude(s => s.Requirements)
            .ThenInclude(s => s.Scores)
            .ToList();

            var scoreChartFunctionViewModels = new List<ScoreChartFunctionViewModel>();
            var scoreChartCategoryViewModels = new List<ScoreChartCategoryViewModel>();
            var scoreChartSubCategoryViewModels = new List<ScoreChartSubcategoryViewModel>();
            var scoreChartRequirementViewModels = new List<ScoreChartRequirementViewModel>();

            foreach (var function in functionsChart)
            {

                var allScores = function.Categories
                   .SelectMany(c => c.SubCategories)
                   .SelectMany(sc => sc.Requirements)
                   .SelectMany(r => r.Scores)
                   .Where(s => s.AssessmentId == id)
                   .ToList();

                int avgDocumentationScore = 0;
                int avgImplementationScore = 0;

                if (allScores.Count() > 0)
                {
                    avgDocumentationScore = (int)allScores.Average(s => s.DocumentationMaturityScore);
                    avgImplementationScore = (int)allScores.Average(s => s.ImplementationMaturityScore);
                }


                ScoreChartFunctionViewModel _scoreChartFunctionViewModel = new ScoreChartFunctionViewModel()
                {
                    Id = function.Id,
                    FunctionName = function.Name,
                    AvgDocumentationMaturityScore = (int)avgDocumentationScore,
                    AvgImplementationMaturityScore = (int)avgImplementationScore
                };

                scoreChartFunctionViewModels.Add(_scoreChartFunctionViewModel);


                foreach (var category in function.Categories)
                {

                    try
                    {
                        var allCategoryScores = category.SubCategories
                       .SelectMany(sc => sc.Requirements)
                       .SelectMany(r => r.Scores)
                       .Where(s => s.AssessmentId == id)
                       .ToList();


                        int avgImplementationScoreCategory = 0;
                        int avgDocumentationScoreCategory = 0;

                        if (allCategoryScores.Count() > 0)
                        {
                            avgDocumentationScoreCategory = (int)allCategoryScores.Average(s => s.DocumentationMaturityScore);
                            avgImplementationScoreCategory = (int)allCategoryScores.Average(s => s.ImplementationMaturityScore);
                        }


                        ScoreChartCategoryViewModel _scoreChartCategoryViewModel = new ScoreChartCategoryViewModel()
                        {
                            Id = category.Id,
                            CategoryName = category.Name,
                            AvgDocumentationMaturityScore = (int)avgDocumentationScoreCategory,
                            AvgImplementationMaturityScore = (int)avgImplementationScoreCategory
                        };

                        scoreChartCategoryViewModels.Add(_scoreChartCategoryViewModel);
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    foreach (var subcategory in category.SubCategories)
                    {
                        try
                        {
                            var allSubCategoryScores = subcategory.Requirements
                                .SelectMany(r => r.Scores)
                                .Where(s => s.AssessmentId == id)
                                .ToList();

                            int avgImplementationScoreSubCategory = 0;
                            int avgDocumentationScoreSubCategory = 0;

                            if (allSubCategoryScores.Count() > 0)
                            {
                                avgDocumentationScoreSubCategory = (int)allSubCategoryScores.Average(s => s.DocumentationMaturityScore);
                                avgImplementationScoreSubCategory = (int)allSubCategoryScores.Average(s => s.ImplementationMaturityScore);
                            }

                            ScoreChartSubcategoryViewModel _scoreChartSubcategoryViewModel = new ScoreChartSubcategoryViewModel()
                            {
                                Id = subcategory.Id,
                                SubcategoryCode = subcategory.Code,
                                AvgDocumentationMaturityScore = (int)avgDocumentationScoreSubCategory,
                                AvgImplementationMaturityScore = (int)avgImplementationScoreSubCategory
                            };

                            scoreChartSubCategoryViewModels.Add(_scoreChartSubcategoryViewModel);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                        foreach (var requirement in subcategory.Requirements)
                        {
                            try
                            {
                                var allRequirementScores = requirement.Scores
                                        .Where(s => s.AssessmentId == id)
                                        .ToList();
                                int avgDocumentationScoreRequirement = 0;
                                int avgImplementationScoreRequirement = 0;

                                if (allRequirementScores.Count() > 0)
                                {
                                    avgDocumentationScoreRequirement = (int)allRequirementScores.Average(s => s.DocumentationMaturityScore);
                                    avgImplementationScoreRequirement = (int)allRequirementScores.Average(s => s.ImplementationMaturityScore);
                                }


                                ScoreChartRequirementViewModel _scoreChartRequirementViewModel = new ScoreChartRequirementViewModel()
                                {
                                    Id = function.Id,
                                    RequirementCode = subcategory.Code,
                                    AvgDocumentationMaturityScore = (int)avgDocumentationScoreRequirement,
                                    AvgImplementationMaturityScore = (int)avgImplementationScoreRequirement
                                };

                                scoreChartRequirementViewModels.Add(_scoreChartRequirementViewModel);
                            }
                            catch (Exception error)
                            {
                                Console.WriteLine(error.Message);
                            }
                        }
                    }
                }
            }

            AllScoresViewModel allScoresViewModel = new AllScoresViewModel()
            {
                FunctionScores = scoreChartFunctionViewModels,
                CategoryScores = scoreChartCategoryViewModels,
                SubcategoryScores = scoreChartSubCategoryViewModels,
                RequirementScores = scoreChartRequirementViewModels
            };

            if (functions == null)
            {
                return NotFound("Function not found!");
            }


            return View("Index", allScoresViewModel);
        }

        public async Task<IActionResult> FunctionScoreOverview()
        {
            var functions = await _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(c => c.SubCategories)
                .ThenInclude(s => s.Requirements)
                .OrderBy(f => f.Order)
                .ToListAsync();

            var scores = await _applicationDbContext.Scores.ToListAsync();

            var viewModel = new List<FunctionScoreViewModel>();

            foreach (var function in functions)
            {
                var functionScores = new FunctionScoreViewModel
                {
                    Id = function.Id,
                    Name = function.Name,
                    Code = function.Code,
                    Description = function.Description
                };

                var categoryScores = new List<CategoryScoreViewModel>();

                foreach (var category in function.Categories.OrderBy(c => c.Order))
                {
                    var subCategoryScores = new List<SubCategoryScoreViewModel>();

                    foreach (var subCategory in category.SubCategories.OrderBy(s => s.Order))
                    {
                        var requirements = subCategory.Requirements.ToList();

                        double subCategoryDocTotal = 0;
                        double subCategoryImplTotal = 0;

                        foreach (var req in requirements)
                        {
                            var score = scores.FirstOrDefault(s => s.RequirementId == req.Id);
                            if (score != null)
                            {
                                subCategoryDocTotal += score.DocumentationMaturityScore ?? 0;
                                subCategoryImplTotal += score.ImplementationMaturityScore ?? 0;
                            }
                        }

                        double subCategoryDocScore = requirements.Count > 0
                            ? Math.Round(subCategoryDocTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryImplScore = requirements.Count > 0
                            ? Math.Round(subCategoryImplTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryAvgScore = Math.Round((subCategoryDocScore + subCategoryImplScore) / 2, 0);

                        var subCategoryScore = new SubCategoryScoreViewModel
                        {
                            Id = subCategory.Id,
                            Name = subCategory.Name,
                            Code = subCategory.Code,
                            Description = subCategory.Description,
                            DocumentationScore = subCategoryDocScore,
                            ImplementationScore = subCategoryImplScore,
                            AverageScore = subCategoryAvgScore,
                            IsKeyMeasure = subCategory.Requirements.Any(r => r.IsKeyMeasurment)
                        };

                        subCategoryScores.Add(subCategoryScore);
                    }

                    var categoryScore = new CategoryScoreViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Code = category.Code,
                        Description = category.Description,
                        SubCategories = subCategoryScores
                    };

                    categoryScores.Add(categoryScore);
                }

                functionScores.Categories = categoryScores;
                viewModel.Add(functionScores);
            }

            ViewBag.FunctionScores = viewModel;
            return View("Index");
        }
        
       

        public async Task<IActionResult> DownloadPdf()
        {
            var functions = await _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(c => c.SubCategories)
                .ThenInclude(s => s.Requirements)
                .OrderBy(f => f.Order)
                .ToListAsync();

            var scores = await _applicationDbContext.Scores.ToListAsync();

            var viewModel = new List<FunctionScoreViewModel>();

            foreach (var function in functions)
            {
                var functionScores = new FunctionScoreViewModel
                {
                    Id = function.Id,
                    Name = function.Name,
                    Code = function.Code,
                    Description = function.Description
                };

                var categoryScores = new List<CategoryScoreViewModel>();

                foreach (var category in function.Categories.OrderBy(c => c.Order))
                {
                    var subCategoryScores = new List<SubCategoryScoreViewModel>();

                    foreach (var subCategory in category.SubCategories.OrderBy(s => s.Order))
                    {
                        var requirements = subCategory.Requirements.ToList();

                        double subCategoryDocTotal = 0;
                        double subCategoryImplTotal = 0;

                        foreach (var req in requirements)
                        {
                            var score = scores.FirstOrDefault(s => s.RequirementId == req.Id);
                            if (score != null)
                            {
                                subCategoryDocTotal += score.DocumentationMaturityScore ?? 0;
                                subCategoryImplTotal += score.ImplementationMaturityScore ?? 0;
                            }
                        }

                        double subCategoryDocScore = requirements.Count > 0
                            ? Math.Round(subCategoryDocTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryImplScore = requirements.Count > 0
                            ? Math.Round(subCategoryImplTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryAvgScore = Math.Round((subCategoryDocScore + subCategoryImplScore) / 2, 0);

                        var subCategoryScore = new SubCategoryScoreViewModel
                        {
                            Id = subCategory.Id,
                            Name = subCategory.Name,
                            Code = subCategory.Code,
                            Description = subCategory.Description,
                            DocumentationScore = subCategoryDocScore,
                            ImplementationScore = subCategoryImplScore,
                            AverageScore = subCategoryAvgScore,
                            IsKeyMeasure = subCategory.Requirements.Any(r => r.IsKeyMeasurment)
                        };

                        subCategoryScores.Add(subCategoryScore);
                    }

                    var categoryScore = new CategoryScoreViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Code = category.Code,
                        Description = category.Description,
                        SubCategories = subCategoryScores
                    };

                    categoryScores.Add(categoryScore);
                }

                functionScores.Categories = categoryScores;
                viewModel.Add(functionScores);
            }

            var pdfBytes = await GeneratePdf(viewModel, User);

            return File(pdfBytes, "application/pdf", $"Score_Tabel_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        }

        public async Task<IActionResult> DownloadExcel()
        {
            var functions = await _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(c => c.SubCategories)
                .ThenInclude(s => s.Requirements)
                .OrderBy(f => f.Order)
                .ToListAsync();

            var scores = await _applicationDbContext.Scores.ToListAsync();

            var viewModel = new List<FunctionScoreViewModel>();

            foreach (var function in functions)
            {
                var functionScores = new FunctionScoreViewModel
                {
                    Id = function.Id,
                    Name = function.Name,
                    Code = function.Code,
                    Description = function.Description
                };

                var categoryScores = new List<CategoryScoreViewModel>();

                foreach (var category in function.Categories.OrderBy(c => c.Order))
                {
                    var subCategoryScores = new List<SubCategoryScoreViewModel>();

                    foreach (var subCategory in category.SubCategories.OrderBy(s => s.Order))
                    {
                        var requirements = subCategory.Requirements.ToList();

                        double subCategoryDocTotal = 0;
                        double subCategoryImplTotal = 0;

                        foreach (var req in requirements)
                        {
                            var score = scores.FirstOrDefault(s => s.RequirementId == req.Id);
                            if (score != null)
                            {
                                subCategoryDocTotal += score.DocumentationMaturityScore ?? 0;
                                subCategoryImplTotal += score.ImplementationMaturityScore ?? 0;
                            }
                        }

                        double subCategoryDocScore = requirements.Count > 0
                            ? Math.Round(subCategoryDocTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryImplScore = requirements.Count > 0
                            ? Math.Round(subCategoryImplTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryAvgScore = Math.Round((subCategoryDocScore + subCategoryImplScore) / 2, 0);

                        var subCategoryScore = new SubCategoryScoreViewModel
                        {
                            Id = subCategory.Id,
                            Name = subCategory.Name,
                            Code = subCategory.Code,
                            Description = subCategory.Description,
                            DocumentationScore = subCategoryDocScore,
                            ImplementationScore = subCategoryImplScore,
                            AverageScore = subCategoryAvgScore,
                            IsKeyMeasure = subCategory.Requirements.Any(r => r.IsKeyMeasurment)
                        };

                        subCategoryScores.Add(subCategoryScore);
                    }

                    var categoryScore = new CategoryScoreViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Code = category.Code,
                        Description = category.Description,
                        SubCategories = subCategoryScores
                    };

                    categoryScores.Add(categoryScore);
                }

                functionScores.Categories = categoryScores;
                viewModel.Add(functionScores);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Score Tabel");

                worksheet.Cell("A1").Value = "Functie";
                worksheet.Cell("B1").Value = "Categorie";
                worksheet.Cell("C1").Value = "Subcategorie Code";
                worksheet.Cell("D1").Value = "Subcategorie Naam";
                worksheet.Cell("E1").Value = "Doc Score";
                worksheet.Cell("F1").Value = "Impl Score";
                worksheet.Cell("G1").Value = "Gem. Score";
                worksheet.Cell("H1").Value = "Key Measure";

                var headerRange = worksheet.Range("A1:H1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4C78A8");
                headerRange.Style.Font.FontColor = XLColor.White;

                int currentRow = 2;
                foreach (var function in viewModel)
                {
                    foreach (var category in function.Categories)
                    {
                        foreach (var subCategory in category.SubCategories)
                        {
                            worksheet.Cell(currentRow, 1).Value = function.Name;
                            worksheet.Cell(currentRow, 2).Value = category.Name;
                            worksheet.Cell(currentRow, 3).Value = subCategory.Code;
                            worksheet.Cell(currentRow, 4).Value = subCategory.Name;
                            worksheet.Cell(currentRow, 5).Value = subCategory.DocumentationScore;
                            worksheet.Cell(currentRow, 6).Value = subCategory.ImplementationScore;
                            worksheet.Cell(currentRow, 7).Value = subCategory.AverageScore;
                            worksheet.Cell(currentRow, 8).Value = subCategory.IsKeyMeasure ? "KEY" : "";

                            double currentThreshold = 3.0;

                            if (subCategory.DocumentationScore < currentThreshold)
                            {
                                worksheet.Cell(currentRow, 5).Style.Fill.BackgroundColor = XLColor.Red;
                                worksheet.Cell(currentRow, 5).Style.Font.FontColor = XLColor.White;
                            }

                            if (subCategory.ImplementationScore < currentThreshold)
                            {
                                worksheet.Cell(currentRow, 6).Style.Fill.BackgroundColor = XLColor.Red;
                                worksheet.Cell(currentRow, 6).Style.Font.FontColor = XLColor.White;
                            }

                            if (subCategory.AverageScore < currentThreshold)
                            {
                                worksheet.Cell(currentRow, 7).Style.Fill.BackgroundColor = XLColor.Red;
                                worksheet.Cell(currentRow, 7).Style.Font.FontColor = XLColor.White;
                            }

                            currentRow++;
                        }
                    }
                }


                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Score_Tabel_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                }
            }
        }

        private async Task<byte[]> GeneratePdf(List<FunctionScoreViewModel> model,
            System.Security.Claims.ClaimsPrincipal user)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var identifyModel = model.Where(f => f.Name == "Identify").ToList();

            double basicThreshold = 2.5;
            double importantThreshold = 3.0;
            double essentialThreshold = 3.0;

            double currentThreshold = importantThreshold;
            double currentTotalThreshold = importantThreshold;

            if (user.IsInRole("Important"))
            {
                currentThreshold = importantThreshold;
                currentTotalThreshold = importantThreshold;
            }
            else if (user.IsInRole("Essential"))
            {
                currentThreshold = essentialThreshold;
                currentTotalThreshold = essentialThreshold;
            }

            double totalAverageScore = identifyModel.SelectMany(f => f.Categories)
                .SelectMany(c => c.SubCategories)
                .Average(s => s.AverageScore);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Score Tabel")
                        .SemiBold().FontSize(18).FontColor(Colors.Black);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().AlignRight()
                                .Text($"Gegenereerd op: {DateTime.Now:dd-MM-yyyy HH:mm}")
                                .FontSize(9).FontColor(Colors.Grey.Medium);

                            column.Item().PaddingTop(10);

                            var scoreText = $"Totaal Gemiddelde Score: {totalAverageScore:F2}";
                            if (totalAverageScore < currentTotalThreshold)
                            {
                                scoreText += $" (Voldoet niet aan de richtlijn van {currentTotalThreshold})";
                            }
                            else
                            {
                                scoreText += $" (Voldoet aan de richtlijn van {currentTotalThreshold})";
                            }

                            column.Item()
                                .Text(scoreText)
                                .FontSize(12)
                                .FontColor(totalAverageScore < currentTotalThreshold
                                    ? Colors.Red.Medium
                                    : Colors.Black);

                            column.Item().PaddingTop(20);

                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(HeaderCellStyle).Text("Categorie").FontColor(Colors.White)
                                        .SemiBold();
                                    header.Cell().Element(HeaderCellStyle).Text("Subcategorie Code")
                                        .FontColor(Colors.White).SemiBold();
                                    header.Cell().Element(HeaderCellStyle).Text("Doc Score").FontColor(Colors.White)
                                        .SemiBold();
                                    header.Cell().Element(HeaderCellStyle).Text("Impl Score").FontColor(Colors.White)
                                        .SemiBold();
                                    header.Cell().Element(HeaderCellStyle).Text("Gem. Score").FontColor(Colors.White)
                                        .SemiBold();
                                    header.Cell().Element(HeaderCellStyle).Text("Key").FontColor(Colors.White)
                                        .SemiBold();
                                });

                                foreach (var function in identifyModel)
                                {
                                    foreach (var category in function.Categories)
                                    {
                                        for (int i = 0; i < category.SubCategories.Count; i++)
                                        {
                                            var subCategory = category.SubCategories[i];

                                            if (i == 0)
                                            {
                                                table.Cell().RowSpan((uint)category.SubCategories.Count)
                                                    .Element(CellStyle)
                                                    .AlignCenter()
                                                    .AlignMiddle()
                                                    .Text(category.Name);
                                            }

                                            table.Cell().Element(CellStyle).Text(subCategory.Code);

                                            table.Cell()
                                                .Element(subCategory.DocumentationScore < currentThreshold
                                                    ? RedCellStyle
                                                    : CellStyle)
                                                .Text(subCategory.DocumentationScore.ToString())
                                                .FontColor(subCategory.DocumentationScore < currentThreshold
                                                    ? Colors.White
                                                    : Colors.Black);

                                            table.Cell()
                                                .Element(subCategory.ImplementationScore < currentThreshold
                                                    ? RedCellStyle
                                                    : CellStyle)
                                                .Text(subCategory.ImplementationScore.ToString())
                                                .FontColor(subCategory.ImplementationScore < currentThreshold
                                                    ? Colors.White
                                                    : Colors.Black);

                                            table.Cell()
                                                .Element(subCategory.AverageScore < currentThreshold
                                                    ? RedCellStyle
                                                    : CellStyle)
                                                .Text(subCategory.AverageScore.ToString())
                                                .FontColor(subCategory.AverageScore < currentThreshold
                                                    ? Colors.White
                                                    : Colors.Black);

                                            table.Cell().Element(CellStyle).AlignCenter()
                                                .Text(subCategory.IsKeyMeasure ? "KEY" : "");
                                        }
                                    }
                                }
                            });

                            column.Item().PaddingTop(20);

                            column.Item()
                                .Text($"Drempelwaarde: {currentThreshold}")
                                .FontSize(9)
                                .FontColor(Colors.Grey.Medium);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Pagina ");
                            x.CurrentPageNumber();
                        });
                });
            });

            return document.GeneratePdf();

            static IContainer CellStyle(IContainer container)
            {
                return container
                    .Border(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Background(Colors.White)
                    .Padding(8)
                    .AlignCenter()
                    .AlignMiddle();
            }

            static IContainer HeaderCellStyle(IContainer container)
            {
                return container
                    .Border(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Background(Colors.Blue.Darken4)
                    .Padding(8)
                    .AlignCenter()
                    .AlignMiddle();
            }

            static IContainer RedCellStyle(IContainer container)
            {
                return container
                    .Border(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Background(Colors.Red.Medium)
                    .Padding(8)
                    .AlignCenter()
                    .AlignMiddle();
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var maturity = await _applicationDbContext.Maturities
                .Include(m => m.MaturityLevels)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (maturity == null)
                return NotFound();

            var viewModel = MapToViewModel(maturity);

            var scores = await _applicationDbContext.Scores.ToListAsync();

            double avgScore = 0;
            if (scores.Any())
            {
                avgScore = scores.Average(s =>
                    ((s.DocumentationMaturityScore ?? 0) + (s.ImplementationMaturityScore ?? 0)) / 2.0);
            }

            ViewBag.MeetsThreshold = MeetsMaturityThreshold(viewModel, avgScore);
            ViewBag.CategoryThreshold = MeetsCategoryThreshold(viewModel, avgScore);
            ViewBag.TotalMaturityThreshold = MeetsTotalMaturityThreshold(viewModel, avgScore);
            ViewBag.CurrentScore = avgScore;

            return View(viewModel);
        }

        public async Task<IActionResult> FunctionScores()
        {
            var functions = await _applicationDbContext.Functions
                .Include(f => f.Categories)
                .ThenInclude(c => c.SubCategories)
                .ThenInclude(s => s.Requirements)
                .OrderBy(f => f.Order)
                .ToListAsync();

            var scores = await _applicationDbContext.Scores.ToListAsync();

            var viewModel = new List<FunctionScoreViewModel>();

            foreach (var function in functions)
            {
                var functionScores = new FunctionScoreViewModel
                {
                    Id = function.Id,
                    Name = function.Name,
                    Code = function.Code,
                    Description = function.Description
                };

                var categoryScores = new List<CategoryScoreViewModel>();

                foreach (var category in function.Categories.OrderBy(c => c.Order))
                {
                    var subCategoryScores = new List<SubCategoryScoreViewModel>();

                    foreach (var subCategory in category.SubCategories.OrderBy(s => s.Order))
                    {
                        var requirements = subCategory.Requirements.ToList();

                        double subCategoryDocTotal = 0;
                        double subCategoryImplTotal = 0;

                        foreach (var req in requirements)
                        {
                            var score = scores.FirstOrDefault(s => s.RequirementId == req.Id);
                            if (score != null)
                            {
                                subCategoryDocTotal += score.DocumentationMaturityScore ?? 0;
                                subCategoryImplTotal += score.ImplementationMaturityScore ?? 0;
                            }
                        }

                        double subCategoryDocScore = requirements.Count > 0
                            ? Math.Round(subCategoryDocTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryImplScore = requirements.Count > 0
                            ? Math.Round(subCategoryImplTotal / requirements.Count, 0)
                            : 0;
                        double subCategoryAvgScore = Math.Round((subCategoryDocScore + subCategoryImplScore) / 2, 0);

                        var subCategoryScore = new SubCategoryScoreViewModel
                        {
                            Id = subCategory.Id,
                            Name = subCategory.Name,
                            Code = subCategory.Code,
                            Description = subCategory.Description,
                            DocumentationScore = subCategoryDocScore,
                            ImplementationScore = subCategoryImplScore,
                            AverageScore = subCategoryAvgScore,
                            IsKeyMeasure = subCategory.Requirements.Any(r => r.IsKeyMeasurment)
                        };

                        subCategoryScores.Add(subCategoryScore);
                    }

                    var categoryScore = new CategoryScoreViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Code = category.Code,
                        Description = category.Description,
                        SubCategories = subCategoryScores,
                    };

                    categoryScores.Add(categoryScore);
                }

                functionScores.Categories = categoryScores;
                viewModel.Add(functionScores);
            }

            ViewBag.FunctionScores = viewModel;
            return View();
        }

        private bool MeetsMaturityThreshold(MaturityViewModel maturity, double score)
        {
            if (maturity == null) return false;

            var maturityFromDb = _applicationDbContext.Maturities.FirstOrDefault(m => m.Name == maturity.Name);

            if (maturityFromDb != null)
            {
                return score >= maturityFromDb.Threshold;
            }

            return false;
        }

        private bool MeetsCategoryThreshold(MaturityViewModel maturity, double score)
        {
            if (maturity == null) return false;

            var maturityFromDb = _applicationDbContext.Maturities.FirstOrDefault(m => m.Name == maturity.Name);

            if (maturityFromDb != null)
            {
                return score >= maturityFromDb.Threshold;
            }

            return true;
        }

        private bool MeetsTotalMaturityThreshold(MaturityViewModel maturity, double score)
        {
            if (maturity == null) return false;

            var maturityFromDb = _applicationDbContext.Maturities.FirstOrDefault(m => m.Name == maturity.Name);

            if (maturityFromDb != null)
            {
                return score >= maturityFromDb.Threshold;
            }

            return false;
        }

        private MaturityViewModel MapToViewModel(Maturity maturity)
        {
            return new MaturityViewModel
            {
                Id = maturity.Id,
                Name = maturity.Name,
                Description = maturity.Description,
                Threshold = maturity.Threshold,
                MaturityLevels = maturity.MaturityLevels?.Select(ml => new MaturityLevelViewModel
                {
                    Id = ml.Id,
                    Level = ml.Level,
                    Value = ml.Value,
                    Documentation = ml.Documentation,
                    Implementation = ml.Implementation,
                    MaturityId = maturity.Id
                }).ToList()
            };
        }
    }
}