namespace WeeklyThaiRecipeManagement.Repositories
{
    public interface IRecipeRepository
    {
        int GetCurrentRecipeId();

        string GetRecipe(int id);

        void Save(string newRecipe);
    }
}