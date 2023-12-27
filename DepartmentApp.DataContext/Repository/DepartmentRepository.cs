using DepartmentApp.DataContext.Interface;
using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.DataContext.Repository
{
    public class DepartmentRepository : IRepository<Department>
    {
        public bool Creat(Department entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Department entity)
        {
            throw new NotImplementedException();
        }

        public Department Get(Predicate<Department> filter)
        {
            throw new NotImplementedException();
        }

        public List<Department> GetAll(Predicate<Department> filter = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
