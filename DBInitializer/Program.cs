using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core;
using Ninject;
using DependencyResolver;

namespace DBInitializer
{
    class Program
    {
        private static IKernel kernel = new StandardKernel();
        static void Main(string[] args)
        {
            ResolverConfig.ConfigurateForConsole(kernel);
            Console.WriteLine(kernel.Get<IUserService>().GetUser(1).EMail);
            Console.Read();
        }

        private static void FillRolesTable(IService<Role> service)
        {
            service.CreateEntity(new Role() { id = 1, Name = "user" });
            service.CreateEntity(new Role() { id = 2, Name = "moderator" });
            service.CreateEntity(new Role() { id = 3, Name = "admin" });
        }
        private static void DropRolesTable(IService<Role> service)
        {
            service.DeleteEntity(new Role() { id = 1, Name = "user" });
            service.DeleteEntity(new Role() { id = 2, Name = "moderator" });
            service.DeleteEntity(new Role() { id = 3, Name = "admin" });
        }
    }
}
