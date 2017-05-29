using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using DependencyResolver;

namespace SocialNetwork.Mvc.Infrastructure
{
    public class DependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public DependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            kernel.ConfigurateForWeb();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}