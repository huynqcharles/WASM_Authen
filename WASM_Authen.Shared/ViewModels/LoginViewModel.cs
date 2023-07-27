﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASM_Authen.Shared.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password toi thieu 6 ky tu.")]
        public string Password { get; set; }
    }
}