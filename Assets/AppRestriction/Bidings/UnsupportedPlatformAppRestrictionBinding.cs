using System.Collections.Generic;
using System;
using AppRestriction.Models;

namespace AppRestriction.Bindings
{
    public class UnsupportedPlatformAppRestrictionBinding : IAppRestrictionBinding
    {
        public List<ApplicationInfo> GetInstalledApps() => throw new NotImplementedException();
        public List<string> GetRunningAppsNames() => throw new NotImplementedException();
    }
}