using DepartmentApp.Domain.Models.Common;
using DepartmentApp.Domain.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.Domain.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Department department { get; set; }

        public Roles Rol { get; set; } = Roles.User;
    }
}
