using System.Collections.Generic;
using AppRestriction.Bindings;
using UnityEngine;
using AppRestriction.Models;

namespace AppRestriction
{
    public class AppRestriction : IAppRestriction
    {
        IAppRestrictionBinding binding;

        public AppRestriction()
        {
            binding = getNativeBinding();
        }

        public List<ApplicationInfo> GetInstalledApps() => binding.GetInstalledApps();

        public List<ApplicationInfo> GetRunningApps() => binding.GetRunningApps();

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