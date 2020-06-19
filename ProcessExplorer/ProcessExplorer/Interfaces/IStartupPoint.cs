using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Interfaces
{
    /// <summary>
    /// Start application interface
    /// </summary>
    public interface IStartupPoint
    {
        /// <summary>
        /// Start method of console application
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        Task Start();
    }
}
