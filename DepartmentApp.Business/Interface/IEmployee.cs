using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.Business.Interface
{
    public interface IEmployee
    {
        Employee Creat(Employee employee, string departmentname);
        Employee Update(int id, Employee employee, string departmentName);
        Employee Delete(int id);
        Employee Get(int id);
        List<Employee> GetAll();
        public List<Employee> GetAll(string Name);
        List<Employee> GetAll(int age);
        List<Employee> GetAll(DateTime date);
    }
}
