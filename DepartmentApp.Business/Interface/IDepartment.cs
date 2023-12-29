using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DepartmentApp.Business.Interface
{
    public interface IDepartment
    {
        Department Creat(Department department);
        Department Update(int id, Department department, string departmentName);
        Department Delete(int id);
        Department Get(int id);
        List<Department> GetAll();
        Department get(string departmentName);
        List<Department> GetAll(int capacity);
        List<Department> GetAll(DateTime date);
        
    }
}
