namespace WeeklyThaiRecipe.ViewModels
{
    using GalaSoft.MvvmLight.Ioc;

    using WeeklyThaiRecipe.Services;

    public class ViewModelLocator
    {
        private readonly ContainerLocator containerLocator;

        public ViewModelLocator()
        {
            this.containerLocator = new ContainerLocator();
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<SettingsViewModel>();
            }
        }

        public WeeklyRecipeViewModel WeeklyRecipeViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<WeeklyRecipeViewModel>();
            }
        }

        public RecipeDetailViewModel RecipeDetailViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RecipeDetailViewModel>();
            }
        }
    }
}