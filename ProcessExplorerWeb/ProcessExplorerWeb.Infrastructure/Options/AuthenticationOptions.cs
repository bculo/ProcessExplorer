namespace ProcessExplorerWeb.Infrastructure.Options
{
    public class AuthenticationOptions
    {
        public JwtTokenOptions JwtTokenOptions { get; set; }
        public IdentityPasswordOptions IdentityPasswordOptions { get; set; }
        public IdentityUserOptions IdentityUserOptions { get; set; }
        public string DefaultRole { get; set; }
    }
}
