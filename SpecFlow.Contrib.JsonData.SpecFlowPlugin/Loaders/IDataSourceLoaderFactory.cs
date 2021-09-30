namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders
{
    public interface IDataSourceLoaderFactory
    {
        IDataSourceLoader CreateLoader(string format, string dataSourcePath);
    }
}
