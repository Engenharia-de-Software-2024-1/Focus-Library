using System.Collections.Generic;
using UnityEngine;
using AppRestriction.Models;
using UnityEngine.UI;
using Unity.VisualScripting;

public class AppRestrictionScene : MonoBehaviour
{
    [SerializeField] GameObject appsContent;
    [SerializeField] ToggleButton appRestrictionPrefab;

    void Start()
    {
        var appRestriction = new AppRestriction.AppRestriction();
        var apps = appRestriction.GetInstalledApps();
        setupAppRestriction(apps);
    }

    void setupAppRestriction(List<ApplicationInfo> apps)
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
}
