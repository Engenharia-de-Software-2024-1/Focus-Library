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

        public List<ApplicationInfo> GetInstalledApps() 
        {
            var apps = binding.GetInstalledApps();
            Debug.Log("apps: " + apps.Count);
            return apps;
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