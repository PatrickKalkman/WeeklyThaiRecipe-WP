namespace WeeklyThaiRecipe.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using System.Windows.Threading;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Phone.Tasks;

    using WeeklyThaiRecipe.Mappers;
    using WeeklyThaiRecipe.Models;
    using WeeklyThaiRecipe.Navigation;
    using WeeklyThaiRecipe.Services;
    using WeeklyThaiRecipe.Utils;

    public class WeeklyRecipeViewModel : ViewModelBase, INavigable
    {
        private readonly BackgroundImageRotator backgroundImageRotator;

        private readonly WeeklyThaiRecipeSettings weeklyThaiRecipeSettings;

        private readonly IPushRegistrationService pushRegistrationService;

        private readonly DispatcherTimer backgroundChangeTimer = new DispatcherTimer();

        private ImageBrush panoramaBackground;

        private ObservableCollection<RecipeViewModel> recipes;

        private RecipeViewModel currentRecipe;

        private RecipeViewModel selectedRecipe;

        private bool busyIndicatorIsVisible;

        public WeeklyRecipeViewModel(
            BackgroundImageRotator backgroundImageRotator, 
            IRecipeService recipeService, 
            RecipeMapper recipeMapper, 
            WeeklyThaiRecipeSettings weeklyThaiRecipeSettings, 
            IPushRegistrationService pushRegistrationService)
        {
            this.backgroundImageRotator = backgroundImageRotator;
            this.weeklyThaiRecipeSettings = weeklyThaiRecipeSettings;
            this.pushRegistrationService = pushRegistrationService;
            BusyIndicatorIsVisible = true;

            Messenger.Default.Register<List<Recipe>>(
                this,
                message =>
                    {
                        Recipes = new ObservableCollection<RecipeViewModel>(recipeMapper.Map(message));
                        Recipes[0].IsFirst = true;
                        this.CurrentRecipe = Recipes[0];
                        BusyIndicatorIsVisible = false;
                    });

            RotatePanoramaBackground();
            this.InitializeAndStartTimer();
            recipeService.StartGetAllRecipes();

            this.SubmitReviewCommand = new RelayCommand(SubmitReview);
            this.MoreAppsCommand = new RelayCommand(this.MoreApps);
        }

        public RelayCommand MoreAppsCommand { get; set; }

        public RelayCommand SubmitReviewCommand { get; set; }

        public ObservableCollection<RecipeViewModel> Recipes
        {
            get
            {
                return recipes;
            }

            set
            {
                recipes = value;
                this.RaisePropertyChanged(() => Recipes);
            }
        }

        public RecipeViewModel CurrentRecipe
        {
            get
            {
                return currentRecipe;
            }

            set
            {
                currentRecipe = value;
                this.RaisePropertyChanged(() => CurrentRecipe);
            }
        }

        public bool BusyIndicatorIsVisible
        {
            get
            {
                return busyIndicatorIsVisible;
            }

            set
            {
                busyIndicatorIsVisible = value;
                this.RaisePropertyChanged(() => BusyIndicatorIsVisible);
            }
        }


        public INavigationService NavigationService { get; set; }

        public RecipeViewModel SelectedRecipe
        {
            get
            {
                return selectedRecipe;
            }

            set
            {
                var oldValue = selectedRecipe;
                selectedRecipe = value;
                this.RaisePropertyChanged(() => SelectedRecipe, oldValue, selectedRecipe, true);
                NavigationService.Navigate("/Views/RecipeDetailView.xaml");
            }
        }

        public ImageBrush PanoramaBackground
        {
            get
            {
                return panoramaBackground;
            }

            set
            {
                panoramaBackground = value;
                this.RaisePropertyChanged(() => PanoramaBackground);
            }
        }

        public bool NotificationsAllowed
        {
            get
            {
                return this.weeklyThaiRecipeSettings.IsNotificationAllowed;
            }

            set
            {
                if (value)
                {
                    pushRegistrationService.EnableToadNotification();
                }
                else
                {
                    pushRegistrationService.DisableToastNotification();
                }
                this.weeklyThaiRecipeSettings.IsNotificationAllowed = value;
                this.RaisePropertyChanged(() => NotificationsAllowed);
            }
        }

        private void InitializeAndStartTimer()
        {
            this.backgroundChangeTimer.Interval = TimeSpan.FromSeconds(10);
            this.backgroundChangeTimer.Tick += this.BackgroundChangeTimer_Tick;
            this.backgroundChangeTimer.Start();
         }

        private void BackgroundChangeTimer_Tick(object sender, EventArgs e)
        {
            RotatePanoramaBackground();
        }

        private void SubmitReview()
        {
          new MarketplaceReviewTask().Show();
        }

        public void MoreApps()
        {
          var searchTask = new MarketplaceSearchTask();
          searchTask.SearchTerms = "Patrick Kalkman";
          searchTask.Show();
        }

        private void RotatePanoramaBackground()
        {
            PanoramaBackground = backgroundImageRotator.Rotate();
        }
    }
}
