using System.Collections.Generic;

namespace AppRestriction
{
    public interface IAppRestriction
    {
        List<string> GetInstalledApps();
    }
}