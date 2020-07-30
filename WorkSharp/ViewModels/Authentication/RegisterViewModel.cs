using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace WorkSharp.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        [Ignore]
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
