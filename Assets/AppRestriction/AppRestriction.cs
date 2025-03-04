using System.Collections.Generic;
using AppRestriction.Bindings;
using UnityEngine;

namespace AppRestriction
{
    public class AppRestriction
    {
        IAppRestrictionBinding binding;

        public AppRestriction()
        {
            binding = getNativeBinding();
        }

        public List<string> GetInstalledApps() 
        {
            var apps = binding.GetInstalledApps();
            Debug.Log("apps: " + apps.Count);
            return apps;
        }

        private IAppRestrictionBinding getNativeBinding() 
        {
            IAppRestrictionBinding binding; 
#if UNITY_ANDROID
            binding = new AndroidAppRestrictionBinding();
#else
            binding = new UnsupportedPlatformAppRestrictionBinding();
#endif
            return binding;
        }
    }
}