namespace WeeklyThaiRecipeManagement.Repositories
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;

    public class RecipeRepository : IRecipeRepository
    {
        public int GetCurrentRecipeId()
        {
            string file = GetRecipeFileName();
            XDocument recipeDocument = XDocument.Load(file);
            XElement recipeElement = recipeDocument.Descendants("recipe").SingleOrDefault();
            if (recipeElement != null)
            {
                return int.Parse(recipeElement.Attribute("id").Value);
            }

            return 0;
        }

        public string GetRecipe(int id)
        {
            string file = GetRecipeFileName();
            string fileContent = File.ReadAllText(file);
            XDocument recipeDocument = XDocument.Load(file);
            XElement recipeElement = recipeDocument.Descendants("recipe").SingleOrDefault();
            if (recipeElement != null)
            {
                int idFromRecipe = int.Parse(recipeElement.Attribute("id").Value);
                if (id == idFromRecipe)
                {
                    return fileContent;
                }
            }

            return null;
        }

        public void Save(string recipe)
        {
            string fileName = GetRecipeFileName();
            File.WriteAllText(fileName, recipe);
        }

        private static string GetRecipeFileName()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("..\\App_data"), "CurrentRecipe.xml").Replace("api\\", string.Empty);
        }
    }
}