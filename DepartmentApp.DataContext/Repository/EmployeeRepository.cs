﻿using DepartmentApp.DataContext.Interface;
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
                var existEmployee = Get(u => u.Id == entity.Id);
                existEmployee = entity;
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
