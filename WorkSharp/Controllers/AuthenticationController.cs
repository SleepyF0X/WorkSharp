using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            DbUser user = new DbUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    await _signInManager.SignInAsync(user, false);
                //}
                
                return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            DbUser user = new DbUser
            {
                UserName = model.Login
            };
            //_signInManager.SignInAsync(user, model.Password);
            return RedirectToAction("Index", "Main");
        }
    }
}
