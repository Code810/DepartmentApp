

using DepartmentApp.Business.Interface;
using DepartmentApp.DataContext;
using DepartmentApp.DataContext.Repository;
using DepartmentApp.Domain.Models;

namespace DepartmentApp.Business.Servicess
{
    public class EmployeeService : IEmployee
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;
        private static int count = DBContext.Employees.Last().Id + 1;
        public EmployeeService()
        {
            _employeeRepository = new();
            _departmentRepository = new();
        }
        public Employee Creat(Employee employee, string departmentname) 
        {
            var existDepartment=_departmentRepository.Get(d=>d.Name.ToLower()==departmentname.ToLower());
            if (existDepartment is null) return null;
            var existEmployesByDepartment=_employeeRepository.GetAll(e=>e.department.Name==departmentname);
            if (existEmployesByDepartment.Count >= existDepartment.Capacity) return null;
            employee.Id = count;
            employee.CreatedDate = DateTime.Now;
            employee.department=existDepartment;
            bool result = _employeeRepository.Creat(employee);
            if (!result) return null;
            DBContext.SaveChange();
            return employee;
        }

        public Employee Delete(int id) 
        {
            var existEmployee = _employeeRepository.Get(d=>d.Id==id);
            if (existEmployee is null) return null;
            if (_employeeRepository.Delete(existEmployee))
            {
                existEmployee.DeletedDate = DateTime.Now;
                DBContext.SaveChange();
                return existEmployee;
            }
            else return null;
        }

        public Employee Get(int id)
        {
            var existemploye = _employeeRepository.Get(e => e.Id == id);
            if (existemploye is null)
            {
                return null;
            }
            else return existemploye;
        }

        public List<Employee> GetAll(string Name)
        {
            var existemployees= _employeeRepository.GetAll(e => e.Name.ToLower() == Name.ToLower());
            if (existemployees.Count == 0)return null;
             return existemployees;
        }

        public List<Employee> GetAll()
        {
            var existemployees= _employeeRepository.GetAll();
            if (existemployees.Count == 0) return null;
            return existemployees;
        }

        public List<Employee> GetAll(int age)
        {
            var existemployees= _employeeRepository.GetAll(e=>e.Age==age);
            if (existemployees.Count == 0) return null;
            return existemployees;
        }

        public List<Employee> GetAll(DateTime date)
        {
            var existemployees = _employeeRepository.GetAll(e=>e.CreatedDate <= date);
            if (existemployees.Count == 0) return null;
            return existemployees;
        }

        public Employee Update(int id, Employee employee, string departmentName)
        {
            var existEmployeeById = _employeeRepository.Get(e=>e.Id==id);
            if (existEmployeeById is null) return null;
            var existDepartmentByName = _departmentRepository.Get(d=>d.Name.ToLower()==departmentName.ToLower());
            if (existDepartmentByName is null) return null;
            var existEmployesByDepartment = _employeeRepository.GetAll(e => e.department.Name.ToLower() == departmentName.ToLower());
            if (existEmployesByDepartment.Count >= existDepartmentByName.Capacity && existEmployeeById.department.Name != departmentName) return null;
           employee.department = existDepartmentByName;
            employee.Id = id;
            if (_employeeRepository.Update(employee))
            {
                DBContext.SaveChange();
                return employee;
            }
            else
            {
                return null;
            }
            

        }
    }
}
