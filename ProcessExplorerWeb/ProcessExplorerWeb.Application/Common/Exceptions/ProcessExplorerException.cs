using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Exceptions
{
    public class ProcessExplorerException : Exception, IException
    {
        public IEnumerable<string> Errors { get; set; }

        public ProcessExplorerException(string message) : base(message)
        {

        }

        public ProcessExplorerException(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public object Exception()
        {
            if (Errors != null)
                return Errors;
            return Message;
        }
    }
}
