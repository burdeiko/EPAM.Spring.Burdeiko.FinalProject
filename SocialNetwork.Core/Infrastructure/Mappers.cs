using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Core.Infrastructure
{
    /// <summary>
    /// Provides extensions for object mapping
    /// </summary>
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

        public static Dal.ORM.Message ToDalMessage(this Message message)
        {
            if (message == null)
                return null;
            return new Dal.ORM.Message()
            {
                Id = message.Id,
                ReceiverId = message.ToId,
                SenderId = message.FromId,
                Date = message.Time,
                MessageString = message.MessageString
            };
        }

        public static Message ToBllMessage(this Dal.ORM.Message message)
        {
            if (message == null)
                return null;
            return new Message()
            {
                Id = message.Id,
                ToId = message.ReceiverId,
                FromId = message.SenderId,
                Time = message.Date,
                MessageString = message.MessageString
            };
        }
    }
}
