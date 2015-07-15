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
    public class TileController : ApiController
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        private readonly TileSender tileSender;

        public TileController(ISubscriptionRepository subscriptionRepository, TileSender tileSender)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.tileSender = tileSender;
        }

        [HttpPost]
        public void Send(
            string frontTitle, 
            int numberToShow, 
            string frontImageLocation, 
            string backTitle,
            string backImageLocation,
            string backContent)
        {
            try
            {
                PhoneUriCollection collection = subscriptionRepository.GetAll();
                tileSender.Send(frontTitle, numberToShow, frontImageLocation, backTitle, backImageLocation, backContent, collection);
            }
            catch (Exception error)
            {
                ErrorSignal.FromCurrentContext().Raise(error);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError, error.Message));
            }
        }
    }
}