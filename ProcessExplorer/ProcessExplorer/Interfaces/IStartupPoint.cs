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
        /// <returns></returns>
        Task Start();
    }
}
