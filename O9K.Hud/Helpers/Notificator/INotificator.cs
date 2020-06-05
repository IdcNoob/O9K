namespace O9K.Hud.Helpers.Notificator
{
    using Notifications;

    internal interface INotificator
    {
        void PushNotification(Notification notification);
    }
}