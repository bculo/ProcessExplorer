using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog.Config;
using ProcessExplorer.Api.Soap.Interfaces;
using ProcessExplorer.Api.Soap.Models;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Sync.Commands.SyncApplication;
using ProcessExplorerWeb.Application.Sync.Commands.SyncProcess;
using ProcessExplorerWeb.Application.Sync.Commands.SyncSession;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Soap.Endpoints
{
    public class SoapSyncService : ISyncService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAuthenticationService _service;
        private readonly IMediator _mediator;

        public SoapSyncService(IHttpContextAccessor httpContext,
            IAuthenticationService service,
            IMediator mediator)
        {
            _httpContext = httpContext;
            _service = service;
            _mediator = mediator;
        }

        #region HELPER METHODS 

        /// <summary>
        /// Given token Valid ?
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> IsTokenValid(string token)
        {
            if(token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return await _service.IsTokenValid(token);
        }

        /// <summary>
        /// Guard against null and validate input token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task CheckInputModel(SecurityModel model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if(!await IsTokenValid(model.Jwt))
            {
                throw new ArgumentException("JWT token must be valid");
            }
        }

        public Guid GetUserIdFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(jwtToken);

            var claim = jsonToken.Claims.FirstOrDefault(i => i.Type == "nameid");
            if (claim is null)
                throw new ArgumentNullException(nameof(claim));

            var stringIdentifier = claim.Value;
            return Guid.Parse(stringIdentifier);
        }

        #endregion

        public async Task<bool> SyncApplications(SyncApplicationModel model)
        {
            await CheckInputModel(model);

            var syncCommand = model.Adapt<SyncApplicationCommand>();
            syncCommand.UserId = GetUserIdFromToken(model.Jwt);

            await _mediator.Send(syncCommand);

            return true;
        }


        public async Task<bool> SyncProcesses(SyncProcessModel model)
        {
            await CheckInputModel(model);

            var syncCommand = model.Adapt<SyncProcessCommand>();
            syncCommand.UserId = GetUserIdFromToken(model.Jwt);

            await _mediator.Send(syncCommand);

            return true;
        }

        public async Task<bool> SyncSession(SyncSessionModel model)
        {
            await CheckInputModel(model);

            var syncCommand = model.Adapt<SyncSessionCommand>();
            syncCommand.UserId = GetUserIdFromToken(model.Jwt);

            await _mediator.Send(syncCommand);

            return true;
        }

        public string Test()
        {
            return nameof(SoapSyncService);
        }
    }
}
