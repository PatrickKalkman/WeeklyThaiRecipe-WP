namespace WeeklyThaiRecipe.Services
{
    using WeeklyThaiRecipe.Utils;

    public class WeeklyThaiRecipeSettings 
    {
        private const string RecipeXmlKey = "RecipeXml";

        private const string IsNotificationAllowedKey = "NotificationAllowed";

        private readonly ISettingsHelper settingsHelper;

        public WeeklyThaiRecipeSettings(ISettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
        }

        public bool IsNotificationAllowed
        {
            get
            {
                return this.settingsHelper.GetSetting(IsNotificationAllowedKey, false);
            }

            set
            {
                this.settingsHelper.UpdateSetting(IsNotificationAllowedKey, value);
            }
        }

        public void SaveDynamicRecipes(string dynamicRecipesXml)
        {
            this.settingsHelper.UpdateSetting(RecipeXmlKey, dynamicRecipesXml);
        }

        public string GetDynamicRecipes()
        {
            return this.settingsHelper.GetSetting(RecipeXmlKey, string.Empty);
        }
    }
}