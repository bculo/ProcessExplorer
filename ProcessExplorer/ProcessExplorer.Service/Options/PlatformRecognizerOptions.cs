namespace ProcessExplorer.Service.Options
{
    public sealed class PlatformRecognizerOptions
    {
        public bool UseRegex { get; set; }
        public string WindowsRegex { get; set; }
        public string LinuxRegex { get; set; }
    }
}
