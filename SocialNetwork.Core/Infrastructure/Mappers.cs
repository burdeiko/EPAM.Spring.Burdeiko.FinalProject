﻿using System;
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
            return new Dal.ORM.User()
            {
                Id = user.Id,
                E_Mail = user.EMail,
                RoleId = user.RoleId,
                PersonId = user.PersonId,
                PasswordHash = user.PasswordHash
            };
        }

        public static User ToBllUser(this Dal.ORM.User dalUser)
        {
            return new User()
            {
                Id = dalUser.Id,
                EMail = dalUser.E_Mail,
                RoleId = dalUser.RoleId,
                PersonId = dalUser.PersonId,
                PasswordHash = dalUser.PasswordHash
            };
        }

        public static Dal.ORM.Role ToDalRole(this Role role)
        {
            return new Dal.ORM.Role()
            {
                Id = role.id,
                Name = role.Name
            };
        }

        public static Role ToBllRole(this Dal.ORM.Role dalRole)
        {
            return new Role()
            {
                id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
