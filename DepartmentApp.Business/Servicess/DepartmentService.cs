

using DepartmentApp.Business.Interface;
using DepartmentApp.DataContext;
using DepartmentApp.DataContext.Repository;
using DepartmentApp.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace DepartmentApp.Business.Servicess
{
    public class DepartmentService : IDepartment
    {
        private readonly DepartmentRepository _departmentRepository;
        private readonly EmployeeRepository _employeeRepository;
        private static int count = DBContext.Departments.Last().Id+ 1;
        public DepartmentService()
        {
            _departmentRepository = new ();
            _employeeRepository = new ();
        }
        public Department Get(string departmentName)
        {
            return _departmentRepository.Get(n => n.Name.ToLower() == departmentName.ToLower());
        }
        public Department Get(int id)
        {
            return _departmentRepository.Get(n => n.Id == id);
        }
        public Department Creat(Department department)
        {
           
            var existDepartment = _departmentRepository.Get(n=>n.Name.ToLower() == department.Name.ToLower());
            if (existDepartment is not null) return null;
            department.Id = count;
            department.CreatedDate= DateTime.Now;
            var result = _departmentRepository.Creat(department);
            if (!result)  return null;
            DBContext.SaveChange();
             return department;
        }

        public Department Delete(int id)
        {
           var existDepartment= _departmentRepository.Get(d=>d.Id == id);
            if (existDepartment is null) return null;
            if (existDepartment.Capacity == 0)
            {
                if (_departmentRepository.Delete(existDepartment))
                {
                    DBContext.SaveChange();
                    return existDepartment;
                }
                else return null;
            }
            else 
            {
                var existEmployes = _employeeRepository.GetAll(e => e.department.Id == id);
                foreach (var employee in existEmployes)
                {
                    _employeeRepository.Delete(employee);
                }
                if (_departmentRepository.Delete(existDepartment))
                {
                    DBContext.SaveChange();
                    return existDepartment;
                }
                else return null;
            }

        } 
        public List<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public List<Department> GetAll(int capacity)
        {
           return _departmentRepository.GetAll(d=>d.Capacity == capacity);
        }

        public List<Department> GetAll(DateTime date)
        {
            return _departmentRepository.GetAll(d => d.CreatedDate < date);
        }

        public Department Update(int id, Department department)
        {
            var existDepartmentById=_departmentRepository.Get(d=>d.Id == id);
            if (existDepartmentById == null) return null;
            var existDepartmentByName = _departmentRepository.Get(d=>d.Name==department.Name && d.Id!=id);
            if (existDepartmentByName is not null) return null;
            else
            {
                department.UpdatedDate = DateTime.Now;
                if (_departmentRepository.Update(department))
                {

                    DBContext.SaveChange();
                    return department;
                }
                else return null;
            }
        }
    }
}
