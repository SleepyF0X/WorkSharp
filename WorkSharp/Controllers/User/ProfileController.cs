using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;
using WorkSharp.ViewModels.User;

namespace WorkSharp.Controllers.User
{
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<DbUser> _userManager;
        private readonly IUserRepository _userRepository;
        public ProfileController( IMapper mapper, UserManager<DbUser> userManager, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Profile()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            var userId = GetUserId();
            var dbUser = _userRepository.GetById(userId);
            var userViewModel = _mapper.Map<UserViewModel>(dbUser);
            ViewData["Profile"] = userViewModel;
            return View("~/Views/User/Profile/Profile.cshtml");
        }
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var dbUser = _userRepository.GetById(userId);
            var userViewModel = _mapper.Map<UserViewModel>(dbUser);
            ViewData["Profile"] = userViewModel;
            return View("~/Views/User/Profile/Profile.cshtml");
        }
        public async Task<IActionResult> EditProfile(UserViewModel userViewModel)
        {
            var userId = GetUserId();
            if (userId.Equals(userViewModel.Id))
            {
                //var dbUser = _mapper.Map<DbUser>(userViewModel);
                var dbUser = await _userManager.FindByIdAsync(userId.ToString());
                dbUser.Age = userViewModel.Age;
                dbUser.Email = userViewModel.Email;
                dbUser.Name = userViewModel.Name;
                dbUser.Surname = userViewModel.Surname;
                dbUser.Skills = userViewModel.Skills;
                dbUser.UserName = userViewModel.UserName;
                var result = await _userManager.UpdateAsync(dbUser);
                if (!result.Succeeded)
                {
                    
                    TempData["ErrorMessage"] = result.Errors.Select(e=>e.Description).Aggregate((a, b) => a + ", " + b);
                    return RedirectToAction("Profile");
                }
            }
            return RedirectToAction("Profile");
        }
        private Guid GetUserId()
        {
            return Guid.Parse((ReadOnlySpan<char>) HttpContext.User.Claims.SingleOrDefault(c=>c.Type.Equals("Id"))?.Value);
        }
    }
}
