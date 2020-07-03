using ProcessExplorer.Api.Soap.Models;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Soap.Interfaces
{
    [ServiceContract, XmlSerializerFormat]
    public interface ISyncService
    {
        [OperationContract]
        string Test();

        [OperationContract]
        Task<bool> SyncSession(SyncSessionModel model);

        [OperationContract]
        Task<bool> SyncApplications(SyncApplicationModel model);

        [OperationContract]
        Task<bool> SyncProcesses(SyncProcessModel model);
    }
}
