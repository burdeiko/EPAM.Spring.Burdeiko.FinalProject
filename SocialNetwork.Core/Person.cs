using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core
{
    public class Person: IEquatable<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Hobbies { get; set; }

        public string FavoriteBooks { get; set; }

        public string Address { get; set; }

        public byte[] Avatar { get; set; }

        public bool Equals(Person other)
        {
            if (other == null)
                return false;
            if (Id == other.Id && FirstName == other.FirstName && LastName == other.LastName)
                return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            var other = obj as Person;
            return Equals(other);
        }
    }
}
