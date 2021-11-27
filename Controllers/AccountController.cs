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
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IClientRepository _clientRepository;
        public AccountController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IClientRepository clientRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clientRepository = clientRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            User client = await _clientRepository.GetByIdAsync(userId);
            ViewBag.clientNom = client.Nom;
            ViewBag.clientPrenom = client.Prenom;
            ViewBag.clientEmail = client.Email;
            ViewBag.clientDob = client.DOB;

            return View();
        }






        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(HttpContext.User);
                User client = await _clientRepository.GetByIdAsync(userId);
                client.UserName = model.UserDetailViewModel.Username;
                client.Email = model.UserDetailViewModel.Email;
                client.Nom = model.UserDetailViewModel.Nom;
                client.Prenom = model.UserDetailViewModel.Prenom;
                var result = await _clientRepository.UpdateAsync(client);
            }
            return View();
        }

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
                    client = new Client()
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "client");
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
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


    }
}
