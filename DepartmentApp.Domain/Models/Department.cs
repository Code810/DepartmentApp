using DepartmentApp.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.Domain.Models
{
    public class Department: BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string CapacityStatus { get; set; }
    }
}
