﻿using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace WorkSharp.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Skills { get; set; }

        [Required]
        public string Age { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Ignore]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}