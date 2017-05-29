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
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}