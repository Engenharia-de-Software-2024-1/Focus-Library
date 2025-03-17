using System.Collections.Generic;
using AppRestriction.Models;

namespace AppRestriction
{
    public interface IAppRestriction
    {
        List<ApplicationInfo> GetInstalledApps();
        List<string> GetRunningAppsNames();
    }
}