namespace WeeklyThaiRecipe.ViewModels
{
    using WeeklyThaiRecipe.Models;

    public class RecipeViewModel
    {
        private readonly Recipe recipe;

        public RecipeViewModel(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public bool IsFirst { get; set; }

        public Recipe Model
        {
            get
            {
                return recipe;
            }
        }

        public string ImageSource
        {
            get
            {
                if (!recipe.Image.Contains("http://"))
                {
                    return string.Format("/Data/Images/{0}", recipe.Image);
                }
                
                return this.recipe.Image;
            }
        }
    }
}                                                                                              