namespace WeeklyThaiRecipe.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using WeeklyThaiRecipe.Models;
    using WeeklyThaiRecipe.ViewModels;

    public class RecipeMapper
    {
        public List<RecipeViewModel> Map(List<Recipe> allRecipes)
        {
            return allRecipes.Select(recipe => new RecipeViewModel(recipe)).ToList();
        }
    }
}