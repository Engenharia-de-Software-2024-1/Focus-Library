using System;

namespace Overlay.Bindings
{
    public class UnsupportedPlatformOverlayBinding : IOverlayBinding
    {
        public void LaunchOverlay() => throw new NotImplementedException();
        public void AskForSystemAlertPermission() => throw new NotImplementedException();
        public bool IsSystemAlertAllowed() => throw new NotImplementedException();
    }
}