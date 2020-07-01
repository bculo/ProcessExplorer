namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IProcessCollectorFactory
    {
        IProcessCollector GetProcessCollector();
    }
}
