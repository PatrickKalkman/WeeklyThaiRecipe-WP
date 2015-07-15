namespace WeeklyThaiRecipeManagement.Repositories
{
    using WeeklyThaiRecipeManagement.Models;
    using WeeklyThaiRecipeManagement.PushNotification;

    public interface ISubscriptionRepository
    {
        void Insert(Subscription subscription);

        PhoneUriCollection GetAll();
    }
}
