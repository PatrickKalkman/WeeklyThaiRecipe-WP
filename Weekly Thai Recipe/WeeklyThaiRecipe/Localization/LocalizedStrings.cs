namespace WeeklyThaiRecipe.Localization
{
    using WeeklyThaiRecipe.Resources;

    public class LocalizedStrings
    {
        private readonly AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources
        {
            get
            {
                return this.localizedResources;
            }
        }
    }
}
