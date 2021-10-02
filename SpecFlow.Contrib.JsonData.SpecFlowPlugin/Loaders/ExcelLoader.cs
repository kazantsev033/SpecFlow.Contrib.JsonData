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

        protected override DataSource LoadDataSourceFromFilePath(string filePath, string dataSet)
        {

            System.Data.DataTable resultTable = SelectDataTable(filePath, dataSet);
            DataTable dataTable = new DataTable(resultTable.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray());

            foreach (DataRow resultTableRow in resultTable.Rows)
            {
                DataRecord dataRecord = new DataRecord();
                foreach (DataColumn column in resultTable.Columns)
                {
                    dataRecord.Fields[column.ColumnName] = new DataValue(resultTableRow[column]);
                }
                dataTable.Items.Add(dataRecord);
            }

            return new DataSource(dataTable);
        }

        internal System.Data.DataTable SelectDataTable(string excelFilePath, string table)
        {
            using FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read);
            using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            try
            {
                if (!result.Tables.Contains(table))
                {
                    return result.Tables[0];
                }
                else
                {
                    return result.Tables[table];
                }

            }
            catch (NullReferenceException)
            {
                throw new ExternalDataPluginException($"Unable to find worksheet {table} in json file: {excelFilePath}");
            }
        }
    }
}
