namespace WeeklyThaiRecipeManagement.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Elmah;

    using WeeklyThaiRecipeManagement.Repositories;

    [Authorize]
    public class RecipeController : ApiController
    {
        private readonly IRecipeRepository recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        // GET /api/recipe
        public int Get()
        {
            try
            {
                return recipeRepository.GetCurrentRecipeId();
            }
            catch (Exception error)
            {
                ErrorSignal.FromCurrentContext().Raise(error);
                throw;
            }
        }

        // GET /api/recipe/5
        public HttpResponseMessage Get(int id)
        {
            string result = recipeRepository.GetRecipe(id);
            if (!string.IsNullOrEmpty(result))
            {
					return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public void Update(string recipe)
        {
            try
            {
                if (!string.IsNullOrEmpty(recipe))
                {
                    recipeRepository.Save(recipe);
                }
            }
            catch (Exception error)
            {
                ErrorSignal.FromCurrentContext().Raise(error);
                throw;
            }
        }
    }
}