using System;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources;

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders
{
    public interface IDataSourceLoader
    {
        bool AcceptsSourceFilePath(string sourceFilePath);
        DataSource LoadDataSource(string path, string sourceFilePath);
    }
}
