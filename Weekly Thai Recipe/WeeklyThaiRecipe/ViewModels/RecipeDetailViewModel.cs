namespace WeeklyThaiRecipe.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;

    public class RecipeDetailViewModel : ViewModelBase
    {
        private RecipeViewModel recipeViewModel;
        
        /// <summary>
        /// Initializes a new instance of the RecipeDetailViewModel class.
        /// </summary>
        public RecipeDetailViewModel()
        {
            Messenger.Default.Register<PropertyChangedMessage<RecipeViewModel>>(
                this,
                message =>
                {
                    RecipeViewModel = null;
                    RecipeViewModel = message.NewValue;
                });
        }

        public RecipeViewModel RecipeViewModel
        {
            get
            {
                return recipeViewModel;
            }

            set
            {
                recipeViewModel = value;
                this.RaisePropertyChanged(() => RecipeViewModel);
            }
        }
    }
}