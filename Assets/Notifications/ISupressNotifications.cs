namespace Notifications
{
    public interface ISupressNotifications
    {
        void SupressAllNotifications();
        void StartAllNotifications();
        void AskForNotificationPolicyAccess();
        bool IsNotificationPolicyAccessGranted();
    }
}