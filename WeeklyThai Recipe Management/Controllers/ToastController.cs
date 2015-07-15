namespace WeeklyThaiRecipeManagement.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Elmah;

    using WeeklyThaiRecipeManagement.PushNotification;
    using WeeklyThaiRecipeManagement.Repositories;

    [Authorize]
    public class ToastController : ApiController
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        private readonly ToastSender toastSender;

        public ToastController(ISubscriptionRepository subscriptionRepository, ToastSender toastSender)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.toastSender = toastSender;
        }

        [HttpPost]
        public void Send(string toastTitle, string toastMessage)
        {
            try
            {
                PhoneUriCollection phonesCollection = subscriptionRepository.GetAll();
                toastSender.Send(toastTitle, toastMessage, phonesCollection);
            }
            catch (Exception error)
            {
                ErrorSignal.FromCurrentContext().Raise(error);
            	throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError, error.Message));
            }
        }
    }
}