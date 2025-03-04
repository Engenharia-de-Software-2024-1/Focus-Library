using System.Collections.Generic;

namespace AppRestriction.Bindings
{
    public interface IAppRestrictionBinding
    {
        List<string> GetInstalledApps();
    }
}