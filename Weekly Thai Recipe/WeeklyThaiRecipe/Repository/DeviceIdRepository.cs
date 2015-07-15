namespace WeeklyThaiRecipe.Repository
{
    using System;

    using Microsoft.Phone.Info;

    public class DeviceIdRepository : IDeviceIdRepository
    {
        public string RetrieveId()
        {
            object uniqueID;
            if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out uniqueID))
            {
                var uniqueId = (byte[])uniqueID;
                return Convert.ToBase64String(uniqueId);
            }

            throw new InvalidOperationException("Could not get the unique id of the device");
        }
    }
}
