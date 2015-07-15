namespace WeeklyThaiRecipe.Services
{
	using Microsoft.Phone.Notification;

    using WeeklyThaiRecipe.Repository;
    using WeeklyThaiRecipe.Utils;

    public class PushRegistrationService : IPushRegistrationService
    {
        private readonly IDeviceIdRepository deviceIdRepository;

        private readonly IPhoneRegistrationService registrationService;

        private readonly WeeklyThaiRecipeSettings weeklyThaiRecipeSettings;

        private HttpNotificationChannel channel;

        public PushRegistrationService(IDeviceIdRepository deviceIdRepository, IPhoneRegistrationService registrationService, WeeklyThaiRecipeSettings weeklyThaiRecipeSettings)
        {
            this.deviceIdRepository = deviceIdRepository;
            this.registrationService = registrationService;
            this.weeklyThaiRecipeSettings = weeklyThaiRecipeSettings;
        }

        public void RegisterService()
        {
            channel = HttpNotificationChannel.Find(Constants.Settings.WeeklyThaiRecipeChannelName);
            if (channel == null)
            {
                channel = new HttpNotificationChannel(Constants.Settings.WeeklyThaiRecipeChannelName);
                this.HookEventHandlers();
                channel.Open();
            }
            else
            {
                this.HookEventHandlers();
                string uniqueId = deviceIdRepository.RetrieveId();
                registrationService.RegisterPhone(uniqueId, channel.ChannelUri.ToString());
            }
        }

        public void DisableToastNotification()
        {
            channel = HttpNotificationChannel.Find(Constants.Settings.WeeklyThaiRecipeChannelName);
            if (channel != null)
            {
                if (channel.IsShellToastBound)
                {
                    channel.UnbindToShellToast();
                }
            }
        }

        public void EnableToadNotification()
        {
            channel = HttpNotificationChannel.Find(Constants.Settings.WeeklyThaiRecipeChannelName);
            if (channel != null)
            {
                if (channel.IsShellToastBound)
                {
                    channel.UnbindToShellToast();
                }
                channel.BindToShellToast();
            }
        }

        private void HookEventHandlers()
        {
            channel.ChannelUriUpdated += this.ChannelUriUpdated;
        }

        private void ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            if (!channel.IsShellTileBound)
            {
                channel.BindToShellTile();
            }

            if (weeklyThaiRecipeSettings.IsNotificationAllowed)
            {
                if (!channel.IsShellToastBound)
                {
                    channel.BindToShellToast();
                }
            }

            string uniqueId = deviceIdRepository.RetrieveId();
            registrationService.RegisterPhone(uniqueId, e.ChannelUri.ToString());
        }
    }
}
