using FluentValidation.Results;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Common.Exceptions
{
    public class ValidationException : Exception, IException
    {
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures have occurred.")
        {
            var test = failures.GroupBy(e => e.PropertyName);

            foreach (var failureGroup in test)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.Select(i => i.ErrorMessage).ToArray();
                Errors.Add(propertyName, propertyFailures);
            }
        }

        public object Exception()
        {
            return Errors;
        }
    }
}
