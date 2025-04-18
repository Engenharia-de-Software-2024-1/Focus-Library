using Notifications.Binding;

namespace Notifications
{
    public class SupressNotifications : ISupressNotifications
    {
        private ISupressNotificationsBinding binding;

        public SupressNotifications() => binding = getBinding();

        public void SupressAllNotifications() => binding.SupressAllNotifications();

        public void StartAllNotifications() => binding.StartAllNotifications();
        
        public void AskForNotificationPolicyAccess() => binding.AskForNotificationPolicyAccess();

        public bool IsNotificationPolicyAccessGranted() => binding.IsNotificationPolicyAccessGranted();

        private ISupressNotificationsBinding getBinding()
        {
            ISupressNotificationsBinding binding;
#if UNITY_ANDROID
            binding = new SupressNotificationsAndroidBinding();
#else
            binding = new SupressNotificationsEditorBinding();
#endif
            return binding;
        }
    }
}