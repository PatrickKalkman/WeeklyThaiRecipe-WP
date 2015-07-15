namespace WeeklyThaiRecipe.Views
{
    using Microsoft.Phone.Controls;

    using WeeklyThaiRecipe.ViewModels;

    public partial class WeeklyRecipeView : PhoneApplicationPage
    {
        public WeeklyRecipeView()
        {
            InitializeComponent();
        }

        private void MoreAppsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = this.DataContext as WeeklyRecipeViewModel;
            viewModel.MoreAppsCommand.Execute(null);
        }

        private void SubmitReviewButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = this.DataContext as WeeklyRecipeViewModel;
            viewModel.SubmitReviewCommand.Execute(null);
        }
    }
}