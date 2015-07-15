namespace WeeklyThaiRecipe.Services
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;

    using WeeklyThaiRecipe.DesignTime;
    using WeeklyThaiRecipe.Mappers;
    using WeeklyThaiRecipe.Repository;
    using WeeklyThaiRecipe.Utils;
    using WeeklyThaiRecipe.ViewModels;

    using DesignSettingsHelper = WeeklyThaiRecipe.Utils.DesignSettingsHelper;

    public class ContainerLocator
    {
        public ContainerLocator()
        {
            this.ConfigureContainer(); 
        }

        private void ConfigureContainer()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<ISettingsHelper, SettingsHelper>();
                SimpleIoc.Default.Register<IPushRegistrationService, PushRegistrationService>();
                SimpleIoc.Default.Register<IRecipeService, RecipeService>();
                SimpleIoc.Default.Register<ITileResetService, TileResetService>();
            }
            else
            {
                SimpleIoc.Default.Register<ISettingsHelper, DesignSettingsHelper>();
                SimpleIoc.Default.Register<IPushRegistrationService, DesignPushRegistrationService>();
                SimpleIoc.Default.Register<ITileResetService, DesignTileResetService>();
                SimpleIoc.Default.Register<IRecipeService, DesignRecipeService>();
            }

            SimpleIoc.Default.Register<IPhoneRegistrationService, PhoneRegistrationService>();
            
            SimpleIoc.Default.Register<IDeviceIdRepository, DeviceIdRepository>();

            SimpleIoc.Default.Register<WeeklyThaiRecipeSettings>();

            SimpleIoc.Default.GetInstance<IRecipeService>();

            SimpleIoc.Default.Register<NetworkConnection>();

            SimpleIoc.Default.Register<BackgroundImageRotator>();
            SimpleIoc.Default.GetInstance<BackgroundImageRotator>();

            SimpleIoc.Default.Register<RecipeMapper>();
            SimpleIoc.Default.GetInstance<RecipeMapper>();

            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.GetInstance<SettingsViewModel>();

            SimpleIoc.Default.Register<WeeklyRecipeViewModel>();
            SimpleIoc.Default.GetInstance<WeeklyRecipeViewModel>();

            SimpleIoc.Default.Register<RecipeDetailViewModel>();
            SimpleIoc.Default.GetInstance<RecipeDetailViewModel>();

            var service = SimpleIoc.Default.GetInstance<IPushRegistrationService>();
            service.RegisterService();

            var tileResetService = SimpleIoc.Default.GetInstance<ITileResetService>();
            tileResetService.ResetTile();
        } 
    }
}