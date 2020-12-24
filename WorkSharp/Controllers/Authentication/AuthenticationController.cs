using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkSharp.DAL.DbModels;
using WorkSharp.ViewModels.Authentication;

namespace WorkSharp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;

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
            var user = _mapper.Map<DbUser>(model);
            user.Id = Guid.NewGuid();
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", model.UserName),
                new Claim("Email", model.Email)
            };
            var regResult = await _userManager.CreateAsync(user, model.Password);
            if (regResult.Succeeded == true)
            {
                await _userManager.AddClaimsAsync(user, claims);
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.ErrorMessage = regResult.Errors.Select(e => e.Description).Aggregate((a, b) => a + ", " + b);
                return View(model);
            }
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