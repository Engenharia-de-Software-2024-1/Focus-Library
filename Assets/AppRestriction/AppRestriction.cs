using System.Collections.Generic;
using AppRestriction.Bindings;
using UnityEngine;
using AppRestriction.Models;
using System;

namespace AppRestriction
{
    public class AppRestriction : IAppRestriction
    {
        IAppRestrictionBinding binding;

        public Action<string> OnRestrictedAppRunning;

        public AppRestriction()
        {
            binding = getNativeBinding();
        }

        public List<ApplicationInfo> GetInstalledApps() => binding.GetInstalledApps();

        public List<string> GetRunningAppsNames() => binding.GetRunningAppsNames();

        public void VerifyRestrictedAppsRunning()
        {
            var runningApps = GetRunningAppsNames();
            var restrictedApps = RestrictedApps.GetRestrictedApps();

            foreach (var app in runningApps)
            {
                if (restrictedApps.ContainsKey(app) && restrictedApps[app])
                {
                    OnRestrictedAppRunning.Invoke(app);
                }
            }
        }

        private IAppRestrictionBinding getNativeBinding() 
        {
            IAppRestrictionBinding binding; 
#if UNITY_EDITOR || UNITY_IOS
            binding = new UnsupportedPlatformAppRestrictionBinding();
#else
            binding = new AndroidAppRestrictionBinding();
#endif
            return binding;
        }
    }
}