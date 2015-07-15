namespace WeeklyThaiRecipe.Utils
{
    public class RegiRideSettingsManager
    {
        private readonly ISettingsHelper settingsHelper;

        public RegiRideSettingsManager(ISettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
        }
    }
}
