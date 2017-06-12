using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Mvc.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Hobbies { get; set; }

        public string FavoriteBooks { get; set; }

        public string Address { get; set; }
        public byte[] Avatar { get; set; }
    }
}