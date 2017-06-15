using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Core;
using SocialNetwork.Mvc.Models;
using System.Web.Mvc;

namespace SocialNetwork.Mvc.Infrastructure
{
    public static class Mappers
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
        public static Message ToBllMessage(this MessageViewModel message)
        {
            if (message == null)
                return null;
            return new Message
            {
                FromId = message.SenderId,
                ToId = message.ReceiverId,
                MessageString = message.Message,
                Time = message.Time
            };
        }

        public static MessageViewModel ToMvcMessage(this Message message)
        {
            if (message == null)
                return null;
            return new MessageViewModel
            {
                Message = message.MessageString,
                ReceiverId = message.ToId,
                SenderId = message.FromId,
                SenderName = System.Web.Mvc.DependencyResolver.Current.GetService<Core.Interfaces.IPersonService>().GetById(message.FromId).FirstName,
                Time = message.Time
            };
        }
    }
}