using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Notifications;

public class EstanteScene : MonoBehaviour
{
    public ISupressNotifications notifications;

    public void Start()
    {
        notifications = new SupressNotifications();

        if (!notifications.IsNotificationPolicyAccessGranted())
        {
            notifications.AskForNotificationPolicyAccess();
        }
    }

    public void OnTimerlicked() => SceneManager.LoadScene("ConfigTimerScene");
    public void OnSettingsClicked() => SceneManager.LoadScene("Configuracoes Scene");
}
