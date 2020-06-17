using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ILoginBehaviour
    {
        public Task ValidateUser();
    }
}
