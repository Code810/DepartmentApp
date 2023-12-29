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
        Employee Creat(Employee department);
        Employee Update(int id, Employee employee, string departmentName);
        Employee Delete(int id);
        Employee Get(int id);
        List<Employee> GetAll();
        Employee get(string Name);
        List<Employee> GetAll(int age);
        List<Department> GetAll(DateTime date);
    }
}
