namespace ProcessExplorer.Service.Options
{
    public class AuthenticationClientOptions
    {
        public string BaseUri { get; set; }
        public string LoginMethod { get; set; }
        public string ValidateTokenMethod { get; set; }
        public int TimeOut { get; set; }
    }
}
