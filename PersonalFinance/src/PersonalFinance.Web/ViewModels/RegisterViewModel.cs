﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [DisplayName("Repeat Password")]
        public string RepeatPassword { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
