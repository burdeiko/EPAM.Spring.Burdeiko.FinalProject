using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Core;
using SocialNetwork.Mvc.Models;

namespace SocialNetwork.Mvc.Infrastructure
{
    public static  class Mappers
    {
        public static PersonViewModel ToMvcPerson(this Person person)
        {
            if (person == null)
                return null;
            return new PersonViewModel()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Address = person.Address,
                FavoriteBooks = person.FavoriteBooks,
                Hobbies = person.Hobbies,
                Avatar = person.Avatar
            };
        }
        public static Person ToBllPerson(this PersonViewModel person)
        {
            if (person == null)
                return null;
            return new Person()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Address = person.Address,
                FavoriteBooks = person.FavoriteBooks,
                Hobbies = person.Hobbies,
                Avatar = person.Avatar
            };
        }
    }
}