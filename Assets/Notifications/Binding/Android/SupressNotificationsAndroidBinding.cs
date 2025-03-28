#if UNITY_ANDROID || UNITY_EDITOR
using UnityEngine;

namespace Notifications.Binding
{
    public class SupressNotificationsAndroidBinding : ISupressNotificationsBinding
    {
        private AndroidJavaObject supressNotifications;
        private AndroidJavaObject context;

        public SupressNotificationsAndroidBinding()
        {
            var unityJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            context = unityJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            supressNotifications = new AndroidJavaObject("com.supressnotifications.SupressNotifications");
        }

        public void SupressAllNotifications() => supressNotifications.CallStatic("supressAllNotifications", context);

        public void AskForNotificationPolicyAccess() => supressNotifications.CallStatic("askForNotificationPolicyAccess", context);

        public bool IsNotificationPolicyAccessGranted() => supressNotifications.CallStatic<bool>("isNotificationPolicyAccessGranted", context);
    }
}
#endif