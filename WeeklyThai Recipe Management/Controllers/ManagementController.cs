using System.Web.Mvc;

namespace WeeklyThaiRecipeManagement.Controllers
{
    using WeeklyThaiRecipeManagement.PushNotification;
    using WeeklyThaiRecipeManagement.Repositories;
    using WeeklyThaiRecipeManagement.ViewModel;

    [Authorize]
    public class ManagementController : Controller
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        public ManagementController(ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
        }

        public ActionResult Index()
        {
            PhoneUriCollection subscribedPhones = subscriptionRepository.GetAll();
            var viewModel = new ManagementViewModel();
            viewModel.NumberOfPhonesRegistered = subscribedPhones.Count;
            viewModel.ToastTitle = "Weekly Thai Recipe";
            viewModel.ToastMessage = "New recipe available";

            viewModel.FrontTitle = "New recipe";
            viewModel.FrontImageLocation = "Images/NewRecipeTileFront.jpg";
            viewModel.BackImageLocation = "Images/NewRecipeTileBack.jpg";
            viewModel.BackTitle = "Tom Kah Kai";
            viewModel.BackContent = "Original Thai Soup";
            viewModel.NumberToShow = 1;

            return View(viewModel);
        }
   
    }
}
