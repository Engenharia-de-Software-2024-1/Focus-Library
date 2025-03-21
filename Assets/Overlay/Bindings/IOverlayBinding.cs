namespace Overlay.Bindings
{
    public interface IOverlayBinding
    {
        void LaunchOverlay();
        void AskForSystemAlertPermission();
        bool IsSystemAlertAllowed();
    }
}