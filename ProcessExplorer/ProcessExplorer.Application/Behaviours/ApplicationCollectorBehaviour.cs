using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ApplicationCollectorBehaviour : IApplicationCollectorBehaviour
    {
        private readonly IApplicationCollectorFactory _appFactory;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInternet _internet;

        public ApplicationCollectorBehaviour(IApplicationCollectorFactory appFactory,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IInternet internet)
        {
            _appFactory = appFactory;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _internet = internet;
        }

        public async Task Collect()
        {
            //get app collector
            IApplicationCollector collector = _appFactory.GetApplicationCollector();

            //get running applications
            IList<ApplicationInformation> apps = collector.GetApplications();

            //check internet connection
            if(!await _internet.CheckForInternetConnectionAsync())
            {
                //store records to local database

            }

            await StoreRecords(apps);
        }

        /// <summary>
        /// Store records to local database
        /// </summary>
        /// <param name="apps"></param>
        /// <returns></returns>
        private async Task StoreRecords(IList<ApplicationInformation> apps)
        { 
            //map
            var entites = apps.Adapt<List<ApplicationEntity>>();

            //update db
            _unitOfWork.Application.BulkAdd(entites.ToList());
            await _unitOfWork.CommitAsync();
        }
    }
}
