namespace WeeklyThaiRecipeManagement.PushNotification
{
    using System;

    using Elmah;

    public class TileSender
    {
        private readonly MessageSender messageSender;

        public TileSender(MessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void Send(string frontTitle, int count, string frontImageLocation, string backTitle, string backImageLocation, string backContent, PhoneUriCollection phoneUriCollection)
        {
            var tileMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                   "<wp:Tile>" +
                      "<wp:BackgroundImage>{2}</wp:BackgroundImage>" +
                      "<wp:Count>{1}</wp:Count>" +
                      "<wp:Title>{0}</wp:Title>" +
                      "<wp:BackBackgroundImage>{4}</wp:BackBackgroundImage>" +
                      "<wp:BackContent>{5}</wp:BackContent>" +
                      "<wp:BackTitle>{3}</wp:BackTitle>" +
                   "</wp:Tile> " +
                "</wp:Notification>";

            tileMessage = string.Format(tileMessage, frontTitle, count, frontImageLocation, backTitle, backImageLocation, backContent);

            var messageBytes = System.Text.Encoding.UTF8.GetBytes(tileMessage);

            foreach (var uri in phoneUriCollection.Values)
            {
                try
                {
                    messageSender.Send(uri, messageBytes, NotificationType.Tile);
                }
                catch (Exception error)
                {
                    ErrorSignal.FromCurrentContext().Raise(error);
                }
            }
        }
    }
}