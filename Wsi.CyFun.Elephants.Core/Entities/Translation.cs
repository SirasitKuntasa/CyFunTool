namespace Wsi.CyFun.Elephants.Core.Entities;

public class Translation : BaseEntity
{
    public Guid LinkedId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsFr { get; set; }
    public bool IsDe { get; set; }
}