using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Persistence;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer
{
    public class Startup
    {
        public async Task StartApplication(IServiceProvider provider)
        {
            #region VALIDATE USER

            ILoginBehaviour loginBehaviour = provider.GetRequiredService<ILoginBehaviour>();
            await loginBehaviour.ValidateUser();

            #endregion


        }
    }
}
 