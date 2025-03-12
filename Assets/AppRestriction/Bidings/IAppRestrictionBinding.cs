using System.Collections.Generic;
using AppRestriction.Models;

namespace AppRestriction.Bindings
{
    public interface IAppRestrictionBinding
    {
        List<ApplicationInfo> GetInstalledApps();
    }
}