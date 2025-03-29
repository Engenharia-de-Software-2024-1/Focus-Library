using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppRestriction.Models;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Permissions;

public class AppRestrictionScene : MonoBehaviour
{
    [SerializeField] GameObject appsContent;
    [SerializeField] ToggleButton appRestrictionPrefab;

    [SerializeField] float verifyInterval = 3f;

    private AppRestriction.AppRestriction appRestriction;

    void Start()
    {
        appRestriction = new AppRestriction.AppRestriction();
        var apps = appRestriction.GetInstalledApps();
        setupAppsContent(apps);
        appRestriction.OnRestrictedAppRunning += (app) => Debug.Log("Restricted app running: " + app); 
    }

    void setupAppsContent(List<ApplicationInfo> apps)
    {
        foreach (var app in apps)
        {
            var appObject = Instantiate(appRestrictionPrefab);
            appObject.transform.SetParent(appsContent.transform, false);
            
            appObject.GetComponentInChildren<Image>().sprite = Sprite.Create(app.Icon, new Rect(0, 0, app.Icon.width, app.Icon.height), new Vector2(0.5f, 0.5f));
            
            appObject.OnEnable += () => { AppRestriction.RestrictedApps.AddRestrictedApp(app); };
            appObject.OnDisable += () => { AppRestriction.RestrictedApps.RemoveRestrictedApp(app); };
        }
    }

    public void StartVerifying() => StartCoroutine(VerifyEverySeconds(verifyInterval));

    IEnumerator VerifyEverySeconds(float seconds)
    {
        Application.runInBackground = true;
        while (true)
        {
            yield return new WaitForSeconds(seconds);

            appRestriction.VerifyRestrictedAppsRunning();
        }
    }
}
