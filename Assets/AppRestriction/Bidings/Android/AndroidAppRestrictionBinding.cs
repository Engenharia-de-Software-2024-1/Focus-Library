#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;

namespace AppRestriction.Bindings
{
    public class AndroidAppRestrictionBinding : IAppRestrictionBinding
    {
        readonly AndroidJavaObject bridge;
        
        public AndroidAppRestrictionBinding()
        {
            bridge = new AndroidJavaObject("com.apprestriction.AppRestrictionBridge");
            var unityJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = unityJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            bridge.CallStatic("Init", context);
            Debug.Log("AppRestriction initialized");
        }

        public List<string> GetInstalledApps() => bridge.CallStatic<List<string>>("getInstalledApps");
    }
}
#endif