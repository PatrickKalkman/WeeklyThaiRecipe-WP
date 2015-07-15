namespace WeeklyThaiRecipeManagement.PushNotification
{
    public enum BatchingInterval
    {
        None = 0, 
        TileImmediately = 1,
        ToastImmediately = 2,
        RawImmediately = 3,
        TileWait450 = 11,
        ToastWait450 = 12,
        RawWait450 = 13,
        TileWait900 = 21,
        ToastWait900 = 22,
        RawWait900 = 23
    }
}