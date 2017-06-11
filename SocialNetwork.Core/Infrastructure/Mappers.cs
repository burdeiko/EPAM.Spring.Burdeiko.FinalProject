using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Core.Infrastructure
{
    public static class Mappers
    {
        public static Dal.ORM.User ToDalUser(this User user)
        {
            if (user == null) return null;
            return new Dal.ORM.User()
            {
                Id = user.Id,
                E_Mail = user.EMail,
                RoleId = user.RoleId,
                PasswordHash = user.PasswordHash,
                Person = user.Person.ToDalPerson()
            };
        }

        public static User ToBllUser(this Dal.ORM.User dalUser)
        {
            if (dalUser == null) return null;
            return new User()
            {
                Id = dalUser.Id,
                EMail = dalUser.E_Mail,
                RoleId = dalUser.RoleId,
                PasswordHash = dalUser.PasswordHash,
                Person = dalUser.Person.ToBllPerson()
            };
        }

        public static Dal.ORM.Role ToDalRole(this Role role)
        {
            if (role == null)
                return null;
            return new Dal.ORM.Role()
            {
                Id = role.id,
                Name = role.Name
            };
        }

        public static Role ToBllRole(this Dal.ORM.Role dalRole)
        {
            if (dalRole == null)
                return null;
            return new Role()
            {
                id = dalRole.Id,
                Name = dalRole.Name
            };
        }

        public static Person ToBllPerson(this Dal.ORM.Person dalPerson)
        {
            if (dalPerson == null)
                return null;
            return new Person()
            {
                Id = dalPerson.Id,
                FirstName = dalPerson.FirstName,
                LastName = dalPerson.LastName,
                Address = dalPerson.Address,
                Hobbies = dalPerson.Hobbies,
                Avatar = dalPerson.Avatar,
                FavoriteBooks = dalPerson.FavoriteBooks
            };
        }

        public static Dal.ORM.Person ToDalPerson(this Person person)
        {
            if (person == null)
                return null;
            return new Dal.ORM.Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Address = person.Address,
                Hobbies = person.Hobbies,
                Avatar = person.Avatar,
                FavoriteBooks = person.FavoriteBooks
            };
        }
    }
}
