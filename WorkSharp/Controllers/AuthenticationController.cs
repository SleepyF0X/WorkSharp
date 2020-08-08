﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.ViewModels.Authentication;

namespace WorkSharp.Controllers
{
    public class AuthenticationController : Controller
    {
        private IMapper _mapper;
        private UserManager<DbUser> _userManager;
        private SignInManager<DbUser> _signInManager;

        public AuthenticationController(IMapper mapper, UserManager<DbUser> userManager, SignInManager<DbUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View("Register", model);
            DbUser user = _mapper.Map<DbUser>(model);
            var claims = new List<Claim>
            {
                new Claim("Name", model.UserName),
                new Claim("Email", model.Email)
            };
            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddClaimsAsync(user, claims);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View("Login", model);
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View("Login", model);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }
    }
}
