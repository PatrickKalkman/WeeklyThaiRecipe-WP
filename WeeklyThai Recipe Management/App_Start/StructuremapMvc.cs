using System.Web.Http;
using System.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(WeeklyThaiRecipeManagement.App_Start.StructuremapMvc), "Start")]

namespace WeeklyThaiRecipeManagement.App_Start 
{
	using StructureMap.ServiceLocatorAdapter;

	using WeeklyThaiRecipeManagement.DependencyResolution;

    public static class StructuremapMvc 
    {
        public static void Start() 
        {
            var container = IoC.Initialize();
        	var resolver = new SmDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);

            // this override is needed because WebAPI is not using DependencyResolver to build controllers 
				GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);

        }
    }
}