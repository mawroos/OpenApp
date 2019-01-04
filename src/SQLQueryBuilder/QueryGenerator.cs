using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;


namespace SQLQueryBuilder
{
    public class QueryGenerator
    {
        public IEnumerable<DimensionColumn> Dimensions { get; set; }

        public IEnumerable<MeasureColumn> Measures { get; set; }

        public string TableName { get; set; }

        public IEnumerable<FilterCriteria> Filters { get; set; }
        private string connectionString { get; set; }

        public QueryGenerator(string connectionString)
        {
            this.Dimensions = new List<DimensionColumn>();
            this.Measures = new List<MeasureColumn>();
            this.Filters = new List<FilterCriteria>();
            this.connectionString = connectionString;
        }

        public IEnumerable<IDictionary<string, object>> GetGroupedData()
        {
            PreQueryCompileChecks();
            var connection = new SqlConnection(connectionString);
            
            var db = new QueryFactory(connection, new SqlServerCompiler());
            db.Logger = compiled => { Debugger.Log(1, "logged", compiled.ToString()); };

            var query = db.Query(TableName);
            
            if (Dimensions.Any())
            {
                var dimensionColumns = GetDimensionColumns(Dimensions);
                query.Select(dimensionColumns.ToArray());
                query.GroupBy(dimensionColumns.ToArray());
            }
            var measureColumns = GenerateSelectColumnsForMeasures(Measures);
            foreach (var filter in Filters)
            {
                if (filter.Operator.ToLower() == "in")
                    query.WhereIn(filter.ColumnName, filter.value.Split(','));
                else
                    query.Where(filter.ColumnName,filter.Operator,filter.value);
            }

            foreach (var measureColumn in measureColumns)
            {
                query.SelectRaw(measureColumn);
            }

            var results= query.Get();
            List<IDictionary<string,object>> convertedResults = new List<IDictionary<string, object>>();
            foreach (var result in results)
            {
                convertedResults.Add((IDictionary<string, object>)result);
            }
            return convertedResults;

        }

        private IEnumerable<string> GenerateSelectColumnsForMeasures(IEnumerable<MeasureColumn> measures)
        {
            List<string> columns = new List<string>();
            foreach (var measureColumn in measures)
            {
                var operation = GetOperationStringfromAggregationType(measureColumn.AggregationType);
                var columnstring = $"{operation}({measureColumn.ColumnName}) as {measureColumn.Name}";
                columns.Add(columnstring);
            }

            return columns;
        }

        private IEnumerable<string> GetDimensionColumns(IEnumerable<DimensionColumn> dimensionColumns)
        {
            List<string> columns = new List<string>();
            foreach (var dimensionColumn in dimensionColumns)
            {
                columns.Add(dimensionColumn.Name);
            }

            return columns;
        }

        private string GetOperationStringfromAggregationType(MeasureColumnAggType measureColumnAggregationType)
        {
            string result = string.Empty;
            switch (measureColumnAggregationType)
            {
                case MeasureColumnAggType.Count:
                    result = "count";
                    break;
                case MeasureColumnAggType.Sum:
                    result = "sum";
                    break;
            }

            return result;
        }

        private void PreQueryCompileChecks()
        {
            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentNullException("Empty Table Name");
            if (Dimensions == null || Dimensions.Count() == 0)
                throw new ArgumentNullException("Empty Dimensions");
            
        }
    }

    public class FilterCriteria
    {
        public string ColumnName { get; set; }
        public string Operator { get; set; }

        public string value { get; set; }
    }
}
