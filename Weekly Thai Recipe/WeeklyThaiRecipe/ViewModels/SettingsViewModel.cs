namespace WeeklyThaiRecipe.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using WeeklyThaiRecipe.Navigation;
    using WeeklyThaiRecipe.Utils;

    public class SettingsViewModel : ViewModelBase, INavigable
    {
        private readonly ISettingsHelper settingsHelper;

        private readonly RelayCommand saveCommand;

        public SettingsViewModel(ISettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
            this.saveCommand = new RelayCommand(this.Save);
        }

        public INavigationService NavigationService { get; set; }

        public RelayCommand SaveCommand
        {
            get
            {
                return this.saveCommand;
            }
        }

        private void Save()
        {
            this.NavigationService.Back();
        }
    }
}
