﻿using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify_clone2.ViewModels;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Spotify_clone2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Spotify_clone2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IClientRepository _clientRepository;
        private IWebHostEnvironment _hostingEnv;

        public AccountController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IClientRepository clientRepository,
                              IWebHostEnvironment hostingEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clientRepository = clientRepository;
            _hostingEnv = hostingEnv;

        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var client = await _userManager.FindByIdAsync(userId);
            ViewBag.clientNom = client.Nom;
            ViewBag.clientPrenom = client.Prenom;
            ViewBag.clientEmail = client.Email;
            ViewBag.clientDob = client.DOB.ToString();
            ViewBag.clientUsername = client.UserName;
            ViewBag.avatar = client.avatar;
            return View();
        }
        
        [Authorize]
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
                    return RedirectToAction("dashboard", "Artist");
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
        [HttpPost]
        public async Task<IActionResult> changePhoto(ProfileViewModel model)
        {

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                var uniqueAvatarName = GetUniqueFileName(model.changePhoto.coverPhoto.FileName);
                var avatarDir = Path.Combine(_hostingEnv.ContentRootPath, "wwwroot/img");
                if (!Directory.Exists(avatarDir))
                {
                    Directory.CreateDirectory(avatarDir);
                }
                var avatarPath = Path.Combine(avatarDir, uniqueAvatarName);
                await model.changePhoto.coverPhoto.CopyToAsync(new FileStream(avatarPath, FileMode.Create));
                user.avatar = uniqueAvatarName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { status = "success" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Please fill with the right detail");
            }
            ViewBag.avatar = user.avatar;
            return Json(new { status = "fail" });
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
        [Authorize]
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
                    Client = new Client()
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
        public async Task<IActionResult> Login(LoginViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userModel.Username, userModel.Password, userModel.RememberMe, false);

                if (result.Succeeded)
                {
                    var client = _clientRepository.getByUserName(userModel.Username);
                    if (client!=null)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "You are not a client");
                    await _signInManager.SignOutAsync();
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(userModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


    }
}
