using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Common.Models.Service
{
    public class Result
    {
        private Result(bool status, IEnumerable<string> errors)
        {
            Succeeded = status;
            Errors = errors.ToList();
        }

        public List<string> Errors { get; set; }

        public bool Succeeded { get; private set; }


        public void AddNewError(string message)
        {
            if (Succeeded)
                Succeeded = false;
            Errors.Add(message);
        }

        public void AddNewError(Exception exception)
        {
            if (Succeeded)
                Succeeded = false;
            Errors.Add(exception.Message);
        }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, errors);
        }
    }
}
