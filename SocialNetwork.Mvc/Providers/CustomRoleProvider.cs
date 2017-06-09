using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SocialNetwork.Core;
using SocialNetwork.Core.Interfaces;
using Ninject;
using Ninject.Web;
using System.Web.Mvc;

namespace SocialNetwork.Mvc.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IRoleService roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
        private readonly IUserService userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        public override string ApplicationName
        {
            get; set;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = userService.GetUserByEMail(username);
            if (user == null)
                return null;
            return new string[1] { roleService.GetRole(user.RoleId).Name };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return roleService.FindByName(roleName) != null;
        }
    }
}