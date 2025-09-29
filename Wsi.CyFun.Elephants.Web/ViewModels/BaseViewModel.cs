namespace Wsi.CyFun.Elephants.Web.Models
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
        public DateTime Updated { get; set; }
    }
}
