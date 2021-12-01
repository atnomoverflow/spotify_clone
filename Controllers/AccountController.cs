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
            var client = await _clientRepository.GetByIdAsync(userId);
            ViewBag.clientNom = client.Nom;
            ViewBag.clientPrenom = client.Prenom;
            ViewBag.clientEmail = client.Email;
            ViewBag.clientDob = client.DOB.ToString();
            ViewBag.clientUsername = client.UserName;
            return View();
        }






        [Authorize(Roles = "client")]
        [HttpPost]
        public async Task<IActionResult> changeUserDetail(ProfileViewModel model)
        {

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                user.Nom = model.UserDetailViewModel.Nom;
                user.Prenom = model.UserDetailViewModel.Prenom;
                user.Email = model.UserDetailViewModel.Email;
                user.DOB = model.UserDetailViewModel.DOB;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return View("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Please fill with the right detail");
            }
            ViewBag.clientNom = user.Nom;
            ViewBag.clientPrenom = user.Prenom;
            ViewBag.clientEmail = user.Email;
            ViewBag.clientDob = user.DOB.ToString();
            ViewBag.clientUsername = user.UserName;
            return View("Profile", model);
        }

        [Authorize(Roles = "client")]
        [HttpPost]
        public async Task<IActionResult> changePassword(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.clientNom = user.Nom;
            ViewBag.clientPrenom = user.Prenom;
            ViewBag.clientEmail = user.Email;
            ViewBag.clientDob = user.DOB.ToString();
            ViewBag.clientUsername = user.UserName;
            if (ModelState.IsValid)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.ChangePasswordViewModel.OldPassword, model.ChangePasswordViewModel.NewPassword);

                if (result.Succeeded)
                {
                    return View("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Wrong password");
            }

            return View("Profile", model);
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
                var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberMe, false);

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
