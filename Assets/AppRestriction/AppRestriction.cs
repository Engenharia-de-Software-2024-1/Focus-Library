using System.Collections.Generic;
using AppRestriction.Bindings;
using UnityEngine;

namespace AppRestriction
{
    public class AppRestriction : IAppRestriction
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
#if UNITY_EDITOR || UNITY_IOS
            binding = new UnsupportedPlatformAppRestrictionBinding();
#else
            binding = new AndroidAppRestrictionBinding();
#endif
            return binding;
        }
    }
}