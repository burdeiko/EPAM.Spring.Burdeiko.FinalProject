using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Hobbies { get; set; }

        public string FavoriteBooks { get; set; }

        public string Address { get; set; }

        public byte[] Avatar { get; set; }
    }
}
