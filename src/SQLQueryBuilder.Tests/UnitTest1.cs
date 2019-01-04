using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SQLQueryBuilder.Tests
{
    public class UnitTest1
    {
        public static string GetDBConnectionString()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var dbFilePath = Path.Combine(currentDirectory, @"Resources\Tests_Database.mdf");
            var connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbFilePath};Integrated Security=True";
            return connString;
        }
        
        [Fact]
        public void Test1()
        {



            string connectionString = GetDBConnectionString();
            QueryGenerator queryBuilder = new QueryGenerator(connectionString);
            queryBuilder.TableName = "Products";
            List<DimensionColumn> dims = new List<DimensionColumn>();
            dims.Add(new DimensionColumn("CategoryID"));
            queryBuilder.Dimensions = dims;
            List<MeasureColumn> measures = new List<MeasureColumn>();
            measures.Add(new MeasureColumn("Price", "UnitPrice", MeasureColumnAggType.Sum));
            measures.Add(new MeasureColumn("StockUnits", "UnitsInStock", MeasureColumnAggType.Sum));
            queryBuilder.Measures = measures;
            var results = queryBuilder.GetGroupedData();
            Assert.True(results.Count() == 8);
            var res1 = results.First();
            
            var firstCategory = results.First(x => (int) x["CategoryID"] == 1);
            Assert.True(firstCategory["Price"].ToString() == "455.7500");


        }
        [Fact]
        public void Test2()
        {
            string connectionString = GetDBConnectionString();
            QueryGenerator queryBuilder = new QueryGenerator(connectionString);
            queryBuilder.TableName = "Products2";
            List<DimensionColumn> dims = new List<DimensionColumn>();
            dims.Add(new DimensionColumn("CompanyName"));
            dims.Add(new DimensionColumn("CategoryName"));
            queryBuilder.Dimensions = dims;
            List<MeasureColumn> measures = new List<MeasureColumn>();
            measures.Add(new MeasureColumn("StockUnits", "UnitsInStock", MeasureColumnAggType.Sum));
            queryBuilder.Measures = measures;
            var results = queryBuilder.GetGroupedData();
            Assert.True(results.Count() == 8);
            


        }
    }
}
