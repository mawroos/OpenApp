using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Satrabel.Starter.Web.Application.Services.Dto;
using SQLQueryBuilder;

namespace Satrabel.Starter.Web.Application
{
    public interface IDemo1Service : IApplicationService
    {
        string GetMyGreeting();
        string GetMyGreeting2(DimensionColumn col);
        Task<IEnumerable<IDictionary<string, object>>> GetData(string id);
        Task<IEnumerable<IDictionary<string, object>>> GetData2(DashboardDataInput input);
    }

    public class Demo1Service : MyApplicationServiceBase, IDemo1Service
    {
        public static string connectionString =
          @"data source=localhost;initial catalog=SGDash_Data;User Id=admin;Password=123$%^Qwe;multipleactiveresultsets=True;application name=EntityFramework";

        public string GetMyGreeting()
        {
            return "Demo1Service salutes you!";
        }
        public string GetMyGreeting2(DimensionColumn col)
        {
            return "Demo1Service salutes you!";
        }
        public async Task<IEnumerable<IDictionary<string, object>>> GetData(string id)
        {
            if (id == "0")
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
                QueryGenerator queryBuilder = new QueryGenerator(connectionString);
                queryBuilder.TableName = "Vehicles";
                List<DimensionColumn> dims = new List<DimensionColumn>();
                dims.Add(new DimensionColumn("Co"));
                queryBuilder.Dimensions = dims;
                List<MeasureColumn> measures = new List<MeasureColumn>();
                measures.Add(new MeasureColumn("Sales", "NetSales", MeasureColumnAggType.Sum));
                measures.Add(new MeasureColumn("Profit", "Profit", MeasureColumnAggType.Sum));
                queryBuilder.Measures = measures;
                var results = queryBuilder.GetGroupedData();

                return results;

            }

            if (id == "1")
            {
          //      string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
                QueryGenerator queryBuilder = new QueryGenerator(connectionString);
                queryBuilder.TableName = "Vehicles";
                List<DimensionColumn> dims = new List<DimensionColumn>();
                dims.Add(new DimensionColumn("CategoryID"));
                queryBuilder.Dimensions = dims;
                List<MeasureColumn> measures = new List<MeasureColumn>();
                measures.Add(new MeasureColumn("Price", "UnitPrice", MeasureColumnAggType.Sum));
                measures.Add(new MeasureColumn("StockUnits", "UnitsInStock", MeasureColumnAggType.Sum));
                queryBuilder.Measures = measures;
                var results = queryBuilder.GetGroupedData();
                return results;

            }

            return null;

        }

        public async Task<IEnumerable<IDictionary<string, object>>> GetData2(DashboardDataInput input)
        {


            QueryGenerator queryBuilder = new QueryGenerator(connectionString);
            queryBuilder.TableName = "Vehicles";
            var dashboardDataParameters = new DahsboardDataParameters(input);

            List<DimensionColumn> dims = new List<DimensionColumn>();
            dims.AddRange(dashboardDataParameters.DimensionColumns);
            queryBuilder.Dimensions = dims;
            List<MeasureColumn> measures = new List<MeasureColumn>();
            measures.AddRange(dashboardDataParameters.MeasureColumns);

            queryBuilder.Measures = measures;
            var results = queryBuilder.GetGroupedData();

            return results;


        }

        [HttpPost]
        public async Task<IEnumerable<IDictionary<string, object>>> RequestData2(DahsboardDataParameters input)
        {


            QueryGenerator queryBuilder = new QueryGenerator(connectionString);
            queryBuilder.TableName = "Vehicles";
            

            List<DimensionColumn> dims = new List<DimensionColumn>();
            dims.AddRange(input.DimensionColumns);
            queryBuilder.Dimensions = dims;
            List<MeasureColumn> measures = new List<MeasureColumn>();
            measures.AddRange(input.MeasureColumns);

            queryBuilder.Measures = measures;
            var results = queryBuilder.GetGroupedData();

            return results;


        }
    }

    public class DahsboardDataParameters
    {
        public List<DimensionColumn> DimensionColumns { get; set; }
        public List<MeasureColumn> MeasureColumns { get; set; }

        public DahsboardDataParameters()
        {
            
        }
        public DahsboardDataParameters(DashboardDataInput input)
        {
            DimensionColumns = new List<DimensionColumn>();
            MeasureColumns = new List<MeasureColumn>();
            foreach (var inputDimension in input.Dimensions)
            {
                DimensionColumn dim = JsonConvert.DeserializeObject<DimensionColumn>(inputDimension);
                DimensionColumns.Add(dim);
            }
            foreach (var inputMeasure in input.Measures)
            {
                MeasureColumn mis = JsonConvert.DeserializeObject<MeasureColumn>(inputMeasure);
                MeasureColumns.Add(mis);
            }
        }
    }
}