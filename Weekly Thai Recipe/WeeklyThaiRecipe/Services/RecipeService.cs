namespace WeeklyThaiRecipe.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Windows;
    using System.Windows.Resources;
    using System.Xml.Linq;

    using GalaSoft.MvvmLight.Messaging;

    using RestSharp;

    using WeeklyThaiRecipe.Models;
    using WeeklyThaiRecipe.Utils;

    public class RecipeService : IRecipeService
    {
        private readonly WeeklyThaiRecipeSettings settings;
        private readonly NetworkConnection networkConnection;

        private List<Recipe> recipeList;

        public RecipeService(WeeklyThaiRecipeSettings settings)
        {
            this.settings = settings;
            this.networkConnection = new NetworkConnection();
        }

        public void StartGetAllRecipes()
        {
            this.recipeList = this.ParseRecipes();
            if (networkConnection.IsAvailable())
            {
                this.GetAndStoreDynamicRecipes();
            }
            else
            {
                SortAndSendList();
            }
        }

        private void GetAndStoreDynamicRecipes()
        {
            this.StartGetLatestRecipeId();
        }

        private void StartGetLatestRecipeId()
        {
            var client = new RestClient(Constants.Settings.Recipe_Service_Api_Url);
            client.CookieContainer = new CookieContainer();

            var request = CreateAuthenticationRequest();
            client.ExecuteAsync(
                request, 
                response =>
                    {
                        var newRequest = new RestRequest("api/Recipe", Method.GET);
                        client.ExecuteAsync(newRequest, this.LatestRecipeIdReceived);
                    });
        }

        private static RestRequest CreateAuthenticationRequest()
        {
            var request = new RestRequest("account/JsonLogin", Method.POST);
            request.AddParameter("UserName", Constants.Settings.UserName);
            request.AddParameter("Password", Constants.Settings.Password);
            return request;
        }

        private void LatestRecipeIdReceived(IRestResponse response)
        {
            try
            {
                int parsedId = int.Parse(response.Content);

                string dynamicRecipes = this.settings.GetDynamicRecipes();
                Recipe recipeLookUp = null;
                List<Recipe> dynamicRecipeList = null;
                if (!string.IsNullOrEmpty(dynamicRecipes))
                {
                    XDocument dataDocument = XDocument.Parse(dynamicRecipes);
                    var recipes = from recipe in dataDocument.Descendants("recipe") select recipe;

                    dynamicRecipeList = recipes.Select(this.ParseRecipe).ToList();

                    recipeLookUp = dynamicRecipeList.SingleOrDefault(r => r.Id == parsedId);
                }

                if (recipeLookUp == null)
                {
                    this.StartGetLatestRecipe(parsedId);
                }
                else
                {
                    this.recipeList.AddRange(dynamicRecipeList);
                    this.SortAndSendList();
                }
            }
            catch (FormatException error)
            {
                this.SortAndSendList();
            }
        }

        private void SortAndSendList()
        {
            this.recipeList = this.recipeList.OrderByDescending(r => r.Id).ToList();
            Messenger.Default.Send(recipeList);
        }

        private void StartGetLatestRecipe(int id)
        {
            var client = new RestClient(Constants.Settings.Recipe_Service_Api_Url);
            client.CookieContainer = new CookieContainer();
            var request = CreateAuthenticationRequest();
            client.ExecuteAsync(
                request,
                response =>
                    {
                        var newRequest = new RestRequest("api/Recipe", Method.GET);
                        newRequest.AddParameter("Id", id);
                        client.ExecuteAsync(newRequest, this.LatestRecipeReceived);
                    });
        }

        private void LatestRecipeReceived(IRestResponse restResponse)
        {
            string response = restResponse.Content.Substring(1, restResponse.Content.Length - 2);

            response = System.Text.RegularExpressions.Regex.Unescape(response);

            string dynamicRecipes = this.settings.GetDynamicRecipes();
            XDocument newRecipeDocument = XDocument.Parse(response);
            IEnumerable<XElement> newRecipes = from recipe in newRecipeDocument.Descendants("recipe") select recipe;

            if (!string.IsNullOrEmpty(dynamicRecipes))
            {
                XDocument recipesFromStorage = XDocument.Parse(dynamicRecipes);
                IEnumerable<XElement> recipes = from recipe in recipesFromStorage.Descendants("recipe") select recipe;
                recipes.ToList().AddRange(newRecipes);
                XDocument newDocument = new XDocument();
                XElement recipesElement = new XElement("Recipes");
                recipesElement.Add(recipes);
                recipesElement.Add(newRecipes);
                newDocument.Add(recipesElement);
                var recipesString = new StringBuilder();
                TextWriter writer = new StringWriter(recipesString);
                newDocument.Save(writer);
                dynamicRecipes = recipesString.ToString();
            }
            else
            {
                var newDocument = new XDocument();
                var recipesElement = new XElement("Recipes");
                recipesElement.Add(newRecipes);
                newDocument.Add(recipesElement);
                var recipesString = new StringBuilder();
                TextWriter writer = new StringWriter(recipesString);
                newDocument.Save(writer);
                dynamicRecipes = recipesString.ToString();
            }

            this.settings.SaveDynamicRecipes(dynamicRecipes);

            Recipe newRecipe = this.ParseRecipe(response);
            this.recipeList.Add(newRecipe);
            this.SortAndSendList();
        }

        private List<Recipe> ParseRecipes()
        {
            StreamResourceInfo xml = Application.GetResourceStream(new Uri("/WeeklyThaiRecipe;component/Data/Recipes.xml", UriKind.Relative));
            XDocument dataDocument = XDocument.Load(xml.Stream);
            var recipes = from recipe in dataDocument.Descendants("recipe") select recipe;

            var localRecipeList = new List<Recipe>();
            foreach (var xElement in recipes)
            {
                Recipe recipe = this.ParseRecipe(xElement);
                localRecipeList.Add(recipe);
            }

            return localRecipeList;
        }

        private Recipe ParseRecipe(string recipe)
        {
            XDocument dataDocument = XDocument.Parse(recipe);
            XElement recipeElement = dataDocument.Descendants("recipe").SingleOrDefault();
            return this.ParseRecipe(recipeElement);
        }

        private Recipe ParseRecipe(XElement xElement)
        {
            var recipe = new Recipe
            {
                Id = int.Parse(xElement.Attribute("id").Value),
                Title = xElement.Attribute("title").Value,
                Image = xElement.Attribute("image").Value,
                PreparationTime = TimeSpan.FromMinutes(int.Parse(xElement.Attribute("preparationtime").Value)),
                CookTime = TimeSpan.FromMinutes(int.Parse(xElement.Attribute("cooktime").Value)),
                Language = xElement.Attribute("lang").Value,
                Spiciness = (Spicy)int.Parse(xElement.Attribute("spiciness").Value),
                Week = int.Parse(xElement.Attribute("week").Value),
                Year = int.Parse(xElement.Attribute("year").Value),
                ForNumberOfPersons = int.Parse(xElement.Attribute("fornumberofpersons").Value),
            };

            recipe.Ingredients = new List<string>();
            foreach (var ingredient in xElement.Descendants("item"))
            {
                recipe.Ingredients.Add(ingredient.Value);
            }

            foreach (var directionParagraph in xElement.Descendants("paragraph"))
            {
                recipe.Directions += directionParagraph.Value + "\n\n";
            }

            return recipe;
        }
    }

}
