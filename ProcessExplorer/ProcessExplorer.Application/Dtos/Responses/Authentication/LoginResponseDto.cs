using System;

namespace Dtos.Responses.Authentication
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string JwtToken { get; set; }
    }
}
