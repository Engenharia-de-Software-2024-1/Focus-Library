using System.Collections.Generic;
using AppRestriction.Models;

namespace AppRestriction
{
    public class RestrictedApps
    {
        private static Dictionary<string, bool> restrictedApps = new();

        public static void AddRestrictedApp(ApplicationInfo app) => restrictedApps[app.ProcessName] = true;

        public static void AddRestrictedApp(string appProcessName) => restrictedApps[appProcessName] = true;

        public static void AddRestrictedApps(List<ApplicationInfo> apps)
        {
            foreach (ApplicationInfo app in apps)
            {
                restrictedApps[app.ProcessName] = true;
            }
        }

        public static void AddRestrictedApps(List<string> apps)
        {
            foreach (string appProcessName in apps)
            {
                restrictedApps[appProcessName] = true;
            }
        }

        public static void RemoveRestrictedApp(ApplicationInfo app) => restrictedApps[app.ProcessName] = false;

        public static void RemoveRestrictedApp(string appProcessName) => restrictedApps[appProcessName] = false;
        
        public static Dictionary<string, bool> GetRestrictedApps() => restrictedApps;
    }
}