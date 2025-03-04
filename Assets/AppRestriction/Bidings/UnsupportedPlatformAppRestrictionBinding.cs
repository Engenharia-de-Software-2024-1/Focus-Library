using System.Collections.Generic;
using System;

namespace AppRestriction.Bindings
{
    public class UnsupportedPlatformAppRestrictionBinding : IAppRestrictionBinding
    {
        public List<string> GetInstalledApps() => throw new NotImplementedException();
    }
}