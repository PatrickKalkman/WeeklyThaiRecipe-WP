namespace WeeklyThaiRecipe.Services
{
    public interface IPushRegistrationService
    {
        void RegisterService();

        void DisableToastNotification();

        void EnableToadNotification();
    }
}