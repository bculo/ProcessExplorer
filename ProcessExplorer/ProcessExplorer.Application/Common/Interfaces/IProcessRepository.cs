﻿using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IProcessRepository : IRepository<ProcessEntity>
    {
    }
}