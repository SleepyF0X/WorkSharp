using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository;
using WorkSharp.ViewModels;

namespace WorkSharp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
