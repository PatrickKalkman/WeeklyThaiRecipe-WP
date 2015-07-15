namespace WeeklyThaiRecipeManagement.DependencyResolution
{
	using System.Web.Mvc;

	public static class ServiceResolverExtensions
    {
        public static IDependencyResolver ToServiceResolver(this IDependencyResolver dependencyResolver)
        {
            return new ServiceResolverAdapter(dependencyResolver);
        }
    }
}