using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wsi.CyFun.Elephants.Core.Entities;
using Wsi.CyFun.Elephants.Web.Data;
using Wsi.CyFun.Elephants.Web.ViewModels;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _applicationDbContext.ApplicationUsers.ToListAsync();
            var userListViewModel = new UserListViewModel
            {
                Users = users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    IsAssessor = u.IsAssessor,
                    IsAdmin = u.IsAdmin,
                    UserRole = u.IsAdmin ? "Admin" : u.IsAssessor ? "Assessor" : "Gebruiker",
                    Created = u.Created
                }).ToList()
            };
            return View(userListViewModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model, string UserRole)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    Username = model.Username,
                    Created = DateTime.Now,
                    IsAdmin = UserRole == "Admin",
                    IsAssessor = UserRole == "Assessor"
                };

                _applicationDbContext.ApplicationUsers.Add(user);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await _applicationDbContext.ApplicationUsers.FindAsync(id);
            if (user == null) return NotFound();

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                IsAssessor = user.IsAssessor,
                IsAdmin = user.IsAdmin,
                UserRole = user.IsAdmin ? "Admin" : user.IsAssessor ? "Assessor" : "Gebruiker",
                Created = user.Created
            };

            return View(viewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserViewModel userViewModel, string userRole)
        {
            if (id != userViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _applicationDbContext.ApplicationUsers.FindAsync(id);
                    if (user == null) return NotFound();

                    user.Username = userViewModel.Username;
                    user.IsAdmin = userRole == "Admin";
                    user.IsAssessor = userRole == "Assessor";
                    // user.Created should not be changed

                    _applicationDbContext.Update(user);
                    await _applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_applicationDbContext.ApplicationUsers.Any(e => e.Id == userViewModel.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await _applicationDbContext.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) return NotFound();

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                IsAssessor = user.IsAssessor,
                IsAdmin = user.IsAdmin,
                UserRole = user.IsAdmin ? "Admin" : user.IsAssessor ? "Assessor" : "Gebruiker",
                Created = user.Created
            };

            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _applicationDbContext.ApplicationUsers.FindAsync(id);
            if (user != null)
            {
                if (user.IsAdmin)
                {
                    TempData["Error"] = "Admin gebruiker kan niet verwijderd worden";
                    return RedirectToAction(nameof(Index));
                }

                _applicationDbContext.ApplicationUsers.Remove(user);
                await _applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}