using System;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources;
using DataTable = SpecFlow.Contrib.JsonData.SpecFlowPlugin.DataSources.DataTable;

namespace SpecFlow.Contrib.JsonData.SpecFlowPlugin.Loaders
{
    public class ExcelLoader : FileBasedLoader
    {
        public ExcelLoader() : base("Excel", ".xlsx", ".xlsb", ".xls")
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        protected override DataSource LoadDataSourceFromFilePath(string filePath, string sourceFilePath, string dataSet)
        {
            using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            System.Data.DataTable resultTable = default;
            try
            {
                if (!result.Tables.Contains(dataSet))
                {
                    resultTable = result.Tables[0];
                }
                else
                {
                    resultTable = result.Tables[dataSet];
                }
                
            }
            catch (NullReferenceException)
            {
                throw new ExternalDataPluginException($"Unable to find worksheet {dataSet} in json file: {filePath}");
            }

            DataTable dataTable = new DataTable(resultTable.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray());

            foreach (DataRow resultTableRow in resultTable.Rows)
            {
                var dataRecord = new DataRecord();
                foreach (DataColumn column in resultTable.Columns)
                {
                    dataRecord.Fields[column.ColumnName] = new DataValue(resultTableRow[column]);
                }
                dataTable.Items.Add(dataRecord);
            }

            return new DataSource(dataTable);
        }
    }
}
