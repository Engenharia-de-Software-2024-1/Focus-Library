using System.Collections.Generic;
using AppRestriction.Models;

namespace AppRestriction
{
    public class RestrictedApps
    {
        private static List<ApplicationInfo> restrictedApps = new();
        public static void AddRestrictedApp(ApplicationInfo app) => restrictedApps.Add(app);
        public static void AddRestrictedApps(List<ApplicationInfo> apps) => restrictedApps.AddRange(apps);
        public static void RemoveRestrictedApp(ApplicationInfo app) => restrictedApps.Remove(app);
        public static List<ApplicationInfo> GetRestrictedApps() => restrictedApps;
    }
}