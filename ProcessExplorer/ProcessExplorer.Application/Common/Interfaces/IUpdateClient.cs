using ProcessExplorer.Application.Dtos.Requests.Update;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IUpdateClient
    {
        Task<bool> UpdateSession(UserSessionDto sessionDto);
    }
}
