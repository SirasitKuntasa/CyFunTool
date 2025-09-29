using System.ComponentModel.DataAnnotations;
using Wsi.CyFun.Elephants.Web.Models;


namespace Wsi.CyFun.Elephants.Web.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        [Required]
        public string UserName { get; set; }

        // [Required]
        // public string? ErrorMessage { get; set; }
    }
}
