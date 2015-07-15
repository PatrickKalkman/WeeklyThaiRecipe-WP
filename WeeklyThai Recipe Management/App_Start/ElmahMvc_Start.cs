[assembly: WebActivator.PreApplicationStartMethod(typeof(WeeklyThaiRecipeManagement.App_Start.ElmahMvc), "Start")]
namespace WeeklyThaiRecipeManagement.App_Start
{
    public class ElmahMvc
    {
        public static void Start()
        {
            Elmah.Mvc.Bootstrap.Initialize();
        }
    }
}