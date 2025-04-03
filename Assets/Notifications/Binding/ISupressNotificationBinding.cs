namespace Notifications.Binding
{
    public interface ISupressNotificationsBinding
    {
        void SupressAllNotifications();

        void StartAllNotifications();

        void AskForNotificationPolicyAccess();

        bool IsNotificationPolicyAccessGranted();
    }
}