namespace WeeklyThaiRecipeManagement.DependencyResolution
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

	public class ServiceResolverAdapter : IDependencyResolver
    {
        private readonly IDependencyResolver dependencyResolver;

        public ServiceResolverAdapter(IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null)
            {
                throw new ArgumentNullException("dependencyResolver");
            }

            this.dependencyResolver = dependencyResolver;
        }

        public object GetService(Type serviceType)
        {
            return dependencyResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return dependencyResolver.GetServices(serviceType);
        }
    }
}
