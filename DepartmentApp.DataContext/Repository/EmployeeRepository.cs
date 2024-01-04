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
            try
            {
                DBContext.Employees.Add(entity); 
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(Employee entity)
        {
            try
            {
                DBContext.Employees.Remove(entity); 
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Employee Get(Predicate<Employee> filter)
        {
            return DBContext.Employees.Find(filter);
        }

        public List<Employee> GetAll(Predicate<Employee> filter = null)
        {
            return filter is null ? DBContext.Employees : DBContext.Employees.FindAll(filter);
        }

        public bool Update(Employee entity)
        {
            try
            {

                //var existEmployee = Get();
                var existingEmployeeIndex=DBContext.Employees.FindIndex(u => u.Id == entity.Id);
                if (existingEmployeeIndex!=-1)
                {
                    DBContext.Employees[existingEmployeeIndex] = entity;
                  return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
