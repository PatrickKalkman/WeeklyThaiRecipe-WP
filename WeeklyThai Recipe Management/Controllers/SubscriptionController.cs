namespace WeeklyThaiRecipeManagement.Controllers
{
    using System;
    using System.Web.Http;

    using Elmah;

    using WeeklyThaiRecipeManagement.Models;
    using WeeklyThaiRecipeManagement.Repositories;

    [Authorize]
    public class SubscriptionController : ApiController
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
        }

        // POST /api/subscription
        public void Post(string phoneId, string channelUri)
        {
            try
            {
                var subscription = new Subscription();
                subscription.PhoneId = phoneId;
                subscription.Uri = channelUri;
                subscriptionRepository.Insert(subscription);
            }
            catch (Exception error)
            {
                ErrorSignal.FromCurrentContext().Raise(error);
                throw;
            }
        }
    }
}