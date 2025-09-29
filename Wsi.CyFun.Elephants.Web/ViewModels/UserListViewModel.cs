using System.Collections.Generic;
using Wsi.CyFun.Elephants.Core.Entities;

namespace Wsi.CyFun.Elephants.Web.ViewModels;

public class UserListViewModel
{
    public List<UserViewModel> Users { get; set; } = new();
}
