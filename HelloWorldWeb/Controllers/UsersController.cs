using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWeb.Data;
using HelloWorldWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWeb.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            this.ViewData["Administrators"] = await this.userManager.GetUsersInRoleAsync("Administrators");
            return this.View(await this.userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> AssignAdminRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.AddToRoleAsync(user, "Administrators");
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> AssignUsualRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, "Administrators");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
