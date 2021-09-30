using SpecFlow.Contrib.JsonData.SpecFlowPlugin;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders;
using TechTalk.SpecFlow.Generator.Interfaces;
using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:GeneratorPlugin(typeof(ExternalDataGeneratorPlugin))]

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin
{
    public class ExternalDataGeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.RegisterDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterTypeAs<ExternalDataTestGenerator, ITestGenerator>();
                
                args.ObjectContainer.RegisterTypeAs<SpecificationProvider, ISpecificationProvider>();
                args.ObjectContainer.RegisterTypeAs<DataSourceLoaderFactory, IDataSourceLoaderFactory>();
                args.ObjectContainer.RegisterTypeAs<CsvLoader, IDataSourceLoader>("CSV");
                args.ObjectContainer.RegisterTypeAs<ExcelLoader, IDataSourceLoader>("Excel");
                args.ObjectContainer.RegisterTypeAs<JsonLoader, IDataSourceLoader>("Json");
            };
        }
    }
}
