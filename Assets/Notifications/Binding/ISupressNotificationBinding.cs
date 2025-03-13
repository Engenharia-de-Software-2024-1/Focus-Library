namespace Notifications.Binding
{
    public interface ISupressNotificationsBinding
    {
        void SupressAllNotifications();

        void AskForNotificationPolicyAccess();

        bool IsNotificationPolicyAccessGranted();
    }
}