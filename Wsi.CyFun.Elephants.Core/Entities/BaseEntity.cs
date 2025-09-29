namespace Wsi.CyFun.Elephants.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Deleted { get; set; }
    public DateTime Updated { get; set; }
}