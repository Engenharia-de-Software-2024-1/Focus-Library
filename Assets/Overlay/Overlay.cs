using Overlay.Bindings;
using UnityEngine.Android;

namespace Overlay
{
    public class Overlay : IOverlay
    {
        IOverlayBinding binding;

        public Overlay()
        {
            binding = GetOverlayBinding();
        }

        public IOverlayBinding GetOverlayBinding()
        {
            IOverlayBinding binding;
#if UNITY_EDITOR || UNITY_IOS
            binding = new UnsupportedPlatformOverlayBinding();
#else
            binding = new AndroidOverlayBinding();
#endif
            return binding;
        }

        public void AskForSystemAlertPermission() => binding.AskForSystemAlertPermission();

        public bool IsSystemAlertAllowed() => binding.IsSystemAlertAllowed();

        public void LaunchOverlay() => binding.LaunchOverlay();
    }
}