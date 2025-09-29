namespace Wsi.CyFun.Elephants.Core.Entities;

public class ApplicationUser : BaseEntity
{
    public bool IsAssessor { get; set; }
    public bool IsAdmin { get; set; }
    public string Username { get; set; }
    public ICollection<Assessment> AssessmentsAssessors { get; set; }
    public ICollection<Assessment> AssessmentsUsers { get; set; }
    
}