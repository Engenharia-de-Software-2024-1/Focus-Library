#if UNITY_EDITOR || UNITY_ANDROID
using UnityEngine;

namespace Overlay.Bindings
{
    public class AndroidOverlayBinding : IOverlayBinding
    {
        private AndroidJavaObject overlay;
        private AndroidJavaObject context;

        public AndroidOverlayBinding()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            overlay = new AndroidJavaObject("com.overlay.Overlay");
        }

        public void LaunchOverlay()
        {
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
            intent.Call<AndroidJavaObject>("setClass", context, new AndroidJavaClass("com.overlay.OverlayService"));
            context.Call<AndroidJavaObject>("startService", intent);
        }

        public void AskForSystemAlertPermission()
        {
            overlay.CallStatic("askForSystemAlertPermission", context);
        }

        public bool IsSystemAlertAllowed()
        {
            return overlay.CallStatic<bool>("isSystemAlertAllowed", context);
        }
    }
}
#endif