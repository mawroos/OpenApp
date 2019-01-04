using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryBuilder
{
    public class DimensionColumn
    {
        public string Name { get; set; }

        public DimensionColumn()
        {
            
        }
        public DimensionColumn(string name)
        {
            Name = name;
            
        }
    }
}
