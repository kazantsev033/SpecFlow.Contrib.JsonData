using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources;


namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders
{
    public class JsonLoader : FileBasedLoader
    {
        public JsonLoader() : base("Json", ".json")
        {
                
        }

        protected override DataSource LoadDataSourceFromFilePath(string filePath, string sourceFilePath)
        {
            JObject originalJson = JObject.Parse(ReadTextFileContent(filePath));
            JArray jsonArray = originalJson["users"].ToObject<JArray>();
            List<string> header = jsonArray[0].ToObject<JObject>().Properties().Select(p => p.Name).ToList();
            var dataTable = new DataTable(header.ToArray());

            foreach (JObject childJson in jsonArray.Select(i => i.ToObject<JObject>()))
            {
                var dataRecord = new DataRecord();
                foreach (JProperty property in childJson.Properties())
                {
                    dataRecord.Fields[property.Name] = new DataValue(property.Value);
                }
                dataTable.Items.Add(dataRecord);
            }
            return new DataSource(dataTable);
        }
    }
}
