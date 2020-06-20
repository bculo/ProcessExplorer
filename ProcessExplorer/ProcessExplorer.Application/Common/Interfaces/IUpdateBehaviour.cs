using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IUpdateBehaviour
    {
        Task CheckForUpdates();
    }
}
