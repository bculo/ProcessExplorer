using Mapster;
using ProcessExplorerWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Dtos.Models
{
    /// <summary>
    /// Base model of every process DTO instance
    /// Every process needs to have a name
    /// </summary>
    public abstract class ProcessExplorerModel
    {
        public string Name { get; set; }
        public DateTime Detected { get; set; }
    }
}
