﻿using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources;


namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders
{
    public class JsonLoader : FileBasedLoader
    {
        public JsonLoader() : base("Json", ".json")
        {
                
        }

        protected override DataSource LoadDataSourceFromFilePath(string filePath, string dataSet)
        {
            JArray jsonArray = SelectJsonArray(filePath, dataSet);

            List<string> header = jsonArray.First.ToObject<JObject>().Properties().Select(p => p.Name).ToList();

            DataTable dataTable = new DataTable(header.ToArray());
            foreach (JObject jsonObject in jsonArray.Select(i => i.ToObject<JObject>()))
            {
                DataRecord dataRecord = new DataRecord();
                foreach (JProperty property in jsonObject.Properties())
                {
                    dataRecord.Fields[property.Name] = new DataValue(property.Value);
                }
                dataTable.Items.Add(dataRecord);
            }
            return new DataSource(dataTable);
        }

        internal JArray SelectJsonArray(string jsonFilePath, string jsonArrayName)
        {
            JObject jsonObject = JObject.Parse(ReadTextFileContent(jsonFilePath));
            try
            {
                if (jsonArrayName is null)
                {
                    return jsonObject.SelectToken("$.*").ToObject<JArray>(); // Select 1st array
                }
                else
                {
                    return jsonObject[jsonArrayName].ToObject<JArray>();
                }
            }
            catch (System.NullReferenceException)
            {
                throw new ExternalDataPluginException($"Unable to find array {jsonArrayName} in json file: {jsonFilePath}");
            }
        }
    }
}
