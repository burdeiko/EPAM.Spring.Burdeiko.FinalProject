using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core;
using Ninject;
using DependencyResolver;
using System.Web.Helpers;
using System.Data.Entity.Validation;

namespace DBInitializer
{
    class Program
    {
        private static IKernel kernel = new StandardKernel();
        static void Main(string[] args)
        {
            ResolverConfig.ConfigurateForConsole(kernel);
            kernel.Get<IUserService>().CreateEntity(new User() { EMail = "timedroll@gmail.com", Id = 1, PasswordHash = Crypto.HashPassword("lzlz"), RoleId = 1 });
        }

        private static void FillRolesTable(IRoleService service)
        {
            service.CreateEntity(new Role() { id = 1, Name = "user" });
            service.CreateEntity(new Role() { id = 2, Name = "moderator" });
            service.CreateEntity(new Role() { id = 3, Name = "admin" });
        }
        private static void DropRolesTable(IRoleService service)
        {
            service.DeleteEntity(new Role() { id = 1, Name = "user" });
            service.DeleteEntity(new Role() { id = 2, Name = "moderator" });
            service.DeleteEntity(new Role() { id = 3, Name = "admin" });
        }
    }
}
