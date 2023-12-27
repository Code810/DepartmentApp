using DepartmentApp.DataContext.Interface;
using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.DataContext.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public bool Creat(Employee entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Employee Get(Predicate<Employee> filter)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll(Predicate<Employee> filter = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
