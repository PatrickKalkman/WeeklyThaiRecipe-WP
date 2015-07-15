namespace WeeklyThaiRecipe.Services
{
    using System.Net;

    using RestSharp;

    using WeeklyThaiRecipe.Utils;

    public class PhoneRegistrationService : IPhoneRegistrationService
    {
        public void RegisterPhone(string id, string uri)
        {
            string responseResult;

            var client = new RestClient(Constants.Settings.Recipe_Service_Api_Url);
            client.CookieContainer = new CookieContainer();
            var request = new RestRequest("account/JsonLogin", Method.POST);
            request.AddParameter("UserName", Constants.Settings.UserName);
            request.AddParameter("Password", Constants.Settings.Password);

            client.ExecuteAsync(
                request,
                response =>
                {
                    responseResult = response.Content;

                    var newrequest = new RestRequest("api/Subscription", Method.POST);
                    newrequest.AddParameter("phoneId", id);
                    newrequest.AddParameter("channelUri", uri);
                    client.ExecuteAsync(
                        newrequest ,
                        newresponse =>
                        {
                            responseResult = newresponse.Content;
                        });
                });
        }
    }
}
