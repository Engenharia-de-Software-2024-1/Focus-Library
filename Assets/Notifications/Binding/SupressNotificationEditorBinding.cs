using System;

namespace Notifications.Binding
{
    public class SupressNotificationsEditorBinding : ISupressNotificationsBinding
    {
        public void SupressAllNotifications() => throw new NotImplementedException();

        public void AskForNotificationPolicyAccess() => throw new NotImplementedException();

        public bool IsNotificationPolicyAccessGranted() => throw new NotImplementedException();
    }
}