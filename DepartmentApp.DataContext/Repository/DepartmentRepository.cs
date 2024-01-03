using DepartmentApp.DataContext.Interface;
using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DepartmentApp.DataContext.Repository
{
    public class DepartmentRepository : IRepository<Department>
    {
        public bool Creat(Department entity)
        {
            try
            {
                DBContext.Departments.Add(entity);
               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Department entity)
        {
            try
            {
                DBContext.Departments.Remove(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Department Get(Predicate<Department> filter)
        {
            return DBContext.Departments.Find(filter);
        }

        public List<Department> GetAll(Predicate<Department> filter = null)
        {
            return filter is null ? DBContext.Departments : DBContext.Departments.FindAll(filter);
        }

        public bool Update(Department entity)
        {
            try
            {
                var existDepartment = Get(u => u.Id == entity.Id);
                existDepartment = entity;
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
