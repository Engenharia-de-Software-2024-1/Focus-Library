namespace Overlay
{
    public interface IOverlay
    {
        void AskForSystemAlertPermission();
        bool IsSystemAlertAllowed();
        void LaunchOverlay();
    }
}