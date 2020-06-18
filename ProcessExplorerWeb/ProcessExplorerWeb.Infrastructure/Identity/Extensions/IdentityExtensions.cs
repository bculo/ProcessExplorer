using Microsoft.AspNetCore.Identity;
using ProcessExplorerWeb.Application.Common.Models.Service;
using System.Linq;

namespace ProcessExplorerWeb.Infrastructure.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static Result GetApplicationResult(this IdentityResult identity)
        {
            if (identity.Succeeded)
                return Result.Success();
            else
                return Result.Failure(identity.Errors.Select(i => i.Description));
        }
    }
}
