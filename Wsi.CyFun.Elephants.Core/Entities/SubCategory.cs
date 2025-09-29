namespace Wsi.CyFun.Elephants.Core.Entities;

public class SubCategory : BaseEntity
{
    public string Description { get; set; }
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
    public string Code { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public ICollection<Requirement> Requirements { get; set; }
}