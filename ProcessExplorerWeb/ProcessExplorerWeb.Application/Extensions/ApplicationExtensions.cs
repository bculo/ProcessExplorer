using Mapster;
using ProcessExplorerWeb.Application.Dtos.Shared;
using ProcessExplorerWeb.Core.Entities;
using System.Collections.Generic;

namespace ProcessExplorerWeb.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static List<ApplicationEntity> ModifyExistingAndGetNewApps(this ICollection<ApplicationEntity> storedApps, IEnumerable<ApplicationExplorerModel> fetchedApps)
        {
            List<ApplicationExplorerModel> newApps = new List<ApplicationExplorerModel>();

            foreach (var fetchedApp in fetchedApps)
            {
                bool newApp = true;
                foreach (var storedApp in storedApps)
                {
                    //we are talking about same app if it has same applicaiton name and same starttime
                    if (storedApp.Name == fetchedApp.Name && storedApp.Started == fetchedApp.Started)
                    {
                        //change last active time of app
                        storedApp.Closed = fetchedApp.LastUse;
                        newApp = false;
                        break;
                    }
                }

                // new app
                if (newApp)
                    newApps.Add(fetchedApp);
            }

            //map
            return newApps.Adapt<List<ApplicationEntity>>();
        }
    }
}
