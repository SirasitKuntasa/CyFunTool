using System;
using System.ComponentModel.DataAnnotations;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Gebruikersnaam is verplicht")]
    public string Username { get; set; }

    public bool IsAssessor { get; set; }
    public bool IsAdmin { get; set; }

    public string UserRole { get; set; }

    public DateTime Created { get; set; }
}
