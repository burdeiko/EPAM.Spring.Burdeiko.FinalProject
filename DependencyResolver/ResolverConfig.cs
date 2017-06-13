using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Dal.Interfaces;
using SocialNetwork.Dal.Infrastructure;
using SocialNetwork.Dal.ORM;
using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateForWeb(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRepository<User>>().To<UserRepository>();
            kernel.Bind<IRepository<Role>>().To<RoleRepository>();
            kernel.Bind<IRepository<Person>>().To<PersonRepository>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IPersonService>().To<PersonService>();
            kernel.Bind<IFriendRequestRepository>().To<FriendRequestRepository>();
        }

        public static void ConfigurateForConsole(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRepository<User>>().To<UserRepository>();
            kernel.Bind<IRepository<Role>>().To<RoleRepository>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IPersonService>().To<PersonService>();
        }
    }
}
