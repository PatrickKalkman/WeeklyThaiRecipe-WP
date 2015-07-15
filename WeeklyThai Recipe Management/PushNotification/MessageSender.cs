namespace WeeklyThaiRecipeManagement.PushNotification
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;

    public class MessageSender
    {
        public void Send(Uri uri, byte[] message, NotificationType notificationType)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "text/xml";
            request.ContentLength = message.Length;

            request.Headers.Add("X-MessageID", Guid.NewGuid().ToString());

            switch (notificationType)
            {
                case NotificationType.Toast:
                    request.Headers["X-WindowsPhone-Target"] = "toast";
                    request.Headers.Add("X-NotificationClass", ((int)BatchingInterval.ToastImmediately).ToString(CultureInfo.InvariantCulture));
                    break;
                case NotificationType.Tile:
                    request.Headers["X-WindowsPhone-Target"] = "token";
                    request.Headers.Add("X-NotificationClass", ((int)BatchingInterval.TileImmediately).ToString(CultureInfo.InvariantCulture));
                    break;
                default:
                    request.Headers.Add("X-NotificationClass", ((int)BatchingInterval.RawImmediately).ToString(CultureInfo.InvariantCulture));
                    break;
            }

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(message, 0, message.Length);
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Debug.WriteLine(string.Format("ERROR: {0}", ex.Message));
                throw ex;
            }
        }
    }
}