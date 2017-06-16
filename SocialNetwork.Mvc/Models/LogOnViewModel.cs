using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Mvc.Models
{
    public class LogOnViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}