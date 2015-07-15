using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;

namespace WeeklyThaiRecipeManagement
{
	using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

	public class SmDependencyResolver : IDependencyResolver {

        private readonly IContainer _container;

        public SmDependencyResolver(IContainer container) {
            _container = container;
        }

        public object GetService(Type serviceType) {
            if (serviceType == null) return null;
            try {
                  return serviceType.IsAbstract || serviceType.IsInterface
                           ? _container.TryGetInstance(serviceType)
                           : _container.GetInstance(serviceType);
            }
            catch(Exception) {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}