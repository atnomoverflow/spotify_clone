using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.ViewModels;
using Microsoft.AspNetCore.Identity; 

namespace Spotify_clone2.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult createRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> createRole(RoleViewModel model)
        {
            var roleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (!roleExist)
            {
                var role = new IdentityRole
                {
                    Name = model.Name,
                };
                var result = await _roleManager.CreateAsync(role);
            }
            return View(model);
        }
    }
}
