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

        protected override DataSource LoadDataSourceFromFilePath(string filePath, string sourceFilePath, string jsonArray)
        {
            if (jsonArray is null)
            {
                throw new ExternalDataPluginException($"Tag @JsonArray is not specified");
            }
            
            JObject originalJson = JObject.Parse(ReadTextFileContent(filePath));            
            JArray jArray;
            
            try
            {
                jArray = originalJson[jsonArray].ToObject<JArray>();
            }
            catch (System.NullReferenceException)
            {
                throw new ExternalDataPluginException($"Unable to find array {jsonArray} in json file: {filePath}");
            }
            
            List<string> header = jArray[0].ToObject<JObject>().Properties().Select(p => p.Name).ToList();
            
            var dataTable = new DataTable(header.ToArray());
            foreach (JObject childJson in jArray.Select(i => i.ToObject<JObject>()))
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
