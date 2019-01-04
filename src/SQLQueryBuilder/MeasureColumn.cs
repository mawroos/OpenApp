using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryBuilder
{
    public class MeasureColumn
    {
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public MeasureColumnAggType AggregationType { get; set; }

        public MeasureColumn()
        {
            
        }

        public MeasureColumn(string name, string columnName, MeasureColumnAggType aggregationType)
        {
            Name = name;
            ColumnName = columnName;
            AggregationType = aggregationType;
        }
        


    }
    public enum MeasureColumnAggType
    {
    Sum, Count
    }


}
