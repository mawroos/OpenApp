using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SQLQueryBuilder;

namespace Satrabel.Starter.Web.Application.Services.Dto
{
    public class DashboardDataInput
    {
        [Required]
        public List<string> Measures { get; set; }

        [Required]
        public List<string> Dimensions { get; set; }
    }

}
