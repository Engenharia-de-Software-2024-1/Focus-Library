#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;
using AppRestriction.Models;

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
        }

        public List<ApplicationInfo> GetInstalledApps() 
        {
            var response = new List<ApplicationInfo>();

            var instaledApps = bridge.CallStatic<AndroidJavaObject>("getInstalledApps");
            var size = instaledApps.Call<int>("size");

            for (int i = 0; i < size; i++)
            {
                var app = instaledApps.Call<AndroidJavaObject>("get", i);
                
                var applicationInfo = new ApplicationInfo
                (
                    bridge.CallStatic<string>("getAppName", app),
                    bridge.CallStatic<string>("getAppProcessName", app),
                    toTexture2D(bridge.CallStatic<byte[]>("getAppIcon", app))
                );

                response.Add(applicationInfo);
            }

            return response;
        }

        public List<string> GetRunningAppsNames() {
            var response = new List<string>();

            var runningApps = bridge.CallStatic<AndroidJavaObject>("getRunningAppsNames");
            var size = runningApps.Call<int>("size");

            for (int i = 0; i < size; i++)
            {
                var app = runningApps.Call<string>("get", i);
                
                response.Add(app);
            }

            return response;
        }

        private Texture2D toTexture2D(byte[] iconBytes)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);

            texture.LoadImage(iconBytes);

            return texture;
        }
    }
}
#endif