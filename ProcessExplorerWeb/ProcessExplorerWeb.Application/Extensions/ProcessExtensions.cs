using Mapster;
using ProcessExplorerWeb.Application.Dtos.Models;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessExplorerWeb.Application.Extensions
{
    public static class ProcessExtensions
    {
        public static List<ProcessEntity> GetNewProcesses(this ICollection<ProcessEntity> storedProcesses, IEnumerable<ProcessExplorerModel> fetchedProcesses)
        {
            HashSet<string> storedProcessesNames = new HashSet<string>(storedProcesses.Select(i => i.ProcessName));
            var notStoredProcesses = fetchedProcesses.Where(i => !storedProcessesNames.Contains(i.Name));
            return notStoredProcesses.Adapt<List<ProcessEntity>>();
        } 
    }
}
