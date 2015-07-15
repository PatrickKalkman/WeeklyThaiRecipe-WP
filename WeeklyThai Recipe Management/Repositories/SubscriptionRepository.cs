namespace WeeklyThaiRecipeManagement.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dapper;

    using WeeklyThaiRecipeManagement.Models;
    using WeeklyThaiRecipeManagement.PushNotification;

    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public void Insert(Subscription subscription)
        {
            using (IDbConnection connection = OpenConnection())
            {
                IEnumerable<Subscription> selectedSubscription = connection.Query<Subscription>(@"select * from subscription where PhoneId=@PhoneId", subscription);
                if (selectedSubscription == null || !selectedSubscription.Any())
                {
                    connection.Execute(@"insert into Subscription(PhoneId, Uri) values (@PhoneId, @Uri)", subscription);
                }
                else
                {
                    connection.Execute(@"update Subscription set Uri = @Uri where PhoneId = @PhoneId", subscription);
                }
            }
        }

        public PhoneUriCollection GetAll()
        {
            using (IDbConnection connection = OpenConnection())
            {
                var collection = new PhoneUriCollection();
                IEnumerable<Subscription> subscriptions = connection.Query<Subscription>(@"select PhoneId, Uri from subscription");
                foreach (var subscription in subscriptions)
                {
                    collection[subscription.PhoneId] = new Uri(subscription.Uri);
                }

                return collection;
            }
        }
    }
}