using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify_clone2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Spotify_clone2.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Spotify_clone2.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ArtistController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IArtistRepository artistRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _artistRepository = artistRepository;
        }

        public async Task<IActionResult> ArtistDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _artistRepository.GetByIdAsync((int)id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }
         [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    artiste = new Artiste()
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "artist");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        [Authorize(Roles = "artist")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

    }
}
