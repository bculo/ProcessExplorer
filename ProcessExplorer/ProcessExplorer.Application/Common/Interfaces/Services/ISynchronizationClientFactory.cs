namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ISynchronizationClientFactory
    {
        ISyncClient GetClient();
    }
}
