namespace ProcessExplorerWeb.Infrastructure.Options
{
    public class IdentityUserOptions
    {
        public bool RequireUniqueEmail { get; set; }
        public int MinimumUsernameLength { get; set; }
        public bool RequireConfirmedEmail { get; set; }
    }
}
