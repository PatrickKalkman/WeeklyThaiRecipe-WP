namespace WeeklyThaiRecipeManagement.PushNotification
{
    using System;

    using Elmah;

    public class ToastSender
    {
        private readonly MessageSender messageSender;

        public ToastSender(MessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void Send(string title, string message, PhoneUriCollection phoneUriCollection)
        {
            var toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                   "<wp:Toast>" +
                      "<wp:Text1>{0}</wp:Text1>" +
                      "<wp:Text2>{1}</wp:Text2>" +
                   "</wp:Toast>" +
                "</wp:Notification>";

            toastMessage = string.Format(toastMessage, title, message);

            var messageBytes = System.Text.Encoding.UTF8.GetBytes(toastMessage);

            foreach (var uri in phoneUriCollection.Values)
            {
                try
                {
                    messageSender.Send(uri, messageBytes, NotificationType.Toast);
                }
                catch (Exception error)
                {
                    ErrorSignal.FromCurrentContext().Raise(error);
                }
            }
        }
    }
}