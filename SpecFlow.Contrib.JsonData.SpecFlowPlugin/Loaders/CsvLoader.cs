using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources;

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders
{
    public class CsvLoader : FileBasedLoader
    {
        public CsvLoader() : base("CSV", ".csv")
        {
        }
        
        protected override DataSource LoadDataSourceFromFilePath(string filePath, string dataSet)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            string fileContent = ReadTextFileContent(filePath);
            DataTable records = LoadCsvDataTable(fileContent, culture);
            return new DataSource(records);
        }

        internal DataTable LoadCsvDataTable(string fileContent, CultureInfo culture)
        {
            using StringReader reader = new StringReader(fileContent);
            using CsvReader csv = new CsvReader(reader, new CsvConfiguration(culture)
            {
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null,
                BadDataFound = args => throw new ExternalDataPluginException($"Invalid data found in CSV, in row {args.Context.Parser.RawRow}, near '{args.RawRecord}'")
            });
            
            csv.Read();
            csv.ReadHeader();
            DataTable dataTable = new DataTable(csv.HeaderRecord);
            while (csv.Read())
            {
                DataRecord dataRecord = new DataRecord();
                foreach (string header in csv.HeaderRecord)
                {
                    dataRecord.Fields[header] = new DataValue(csv.GetField(header));
                }
                dataTable.Items.Add(dataRecord);
            }

            return dataTable;
        }
    }
}
