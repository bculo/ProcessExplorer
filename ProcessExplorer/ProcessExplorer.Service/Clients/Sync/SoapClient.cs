using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using SyncSoapReference;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients.Sync
{
    public class SoapClient : ISyncClient
    {
        private readonly SyncServiceClient syncServiceClient;
        private readonly ITokenService _tokenService;

        public SoapClient(ITokenService tokenService)
        {
            syncServiceClient = new SyncServiceClient();
            _tokenService = tokenService;
        }

        public async Task<bool> Sync(UserSessionDto sessionDto)
        {
            return await ExecuteSoapMethod<SyncSessionModel>(syncServiceClient.SyncSessionAsync, sessionDto);
        }

        public async Task<bool> SyncApplications(UserSessionDto sessionDto)
        {
            return await ExecuteSoapMethod<SyncApplicationModel>(syncServiceClient.SyncApplicationsAsync, sessionDto);
        }

        public async Task<bool> SyncProcesses(UserSessionDto sessionDto)
        {
            return await ExecuteSoapMethod<SyncProcessModel>(syncServiceClient.SyncProcessesAsync, sessionDto);
        }

        public async Task<bool> ExecuteSoapMethod<T>(Func<T, Task<bool>> soapFunctionAsync, UserSessionDto dto) where T : SecurityModel
        {
            try
            {
                await syncServiceClient.OpenAsync();

                T soapModel = dto.Adapt<T>();

                soapModel.Jwt = _tokenService.GetValidToken();

                await soapFunctionAsync(soapModel);

                await syncServiceClient.CloseAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class SoapClientMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<UserSessionDto, SyncSessionModel>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.SessionId, src => src.SessionId.ToString())
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Applications, src => src.Applications)
                .Map(dst => dst.Processes, src => src.Processes);

            config.ForType<UserSessionDto, SyncProcessModel>()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.SessionId, src => src.SessionId.ToString())
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Processes, src => src.Processes);

            config.ForType<ProcessDto, ProcessInstanceModel>()
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.SessionId, src => src.SessionId.ToString())
                .Map(dst => dst.Detected, src => src.Detected);

            config.ForType<ApplicationDto, ApplicationInstanceModel > ()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.LastUse, src => src.LastUse)
                .Map(dst => dst.Name, src => src.Name)
                .Map(dst => dst.SessionId, src => src.SessionId.ToString());

            config.ForType<UserSessionDto, SyncApplicationModel> ()
                .Map(dst => dst.Started, src => src.Started)
                .Map(dst => dst.SessionId, src => src.SessionId.ToString())
                .Map(dst => dst.UserName, src => src.UserName)
                .Map(dst => dst.OS, src => src.OS)
                .Map(dst => dst.Applications, src => src.Applications);
        }
    }
}
