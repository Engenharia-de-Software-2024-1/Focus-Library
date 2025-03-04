using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppRestrictionScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI appsText;

    void Start()
    {
        var appRestriction = new AppRestriction.AppRestriction();
        var apps = appRestriction.GetInstalledApps();
        appsText.text = string.Join("\n", apps);
    }

    void Update()
    {
        
    }
}
