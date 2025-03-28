namespace Notifications
{
    public interface ISupressNotifications
    {
        void SupressAllNotifications();
        void AskForNotificationPolicyAccess();
        bool IsNotificationPolicyAccessGranted();
    }
}