

using DepartmentApp.Business.Interface;
using DepartmentApp.DataContext;
using DepartmentApp.DataContext.Repository;
using DepartmentApp.Domain.Models;

namespace DepartmentApp.Business.Servicess
{
    public class DepartmentService : IDepartment
    {
        private readonly DepartmentRepository _departmentRepository;
        private static int count = DBContext.Departments.Last().Id + 1;
        public DepartmentService()
        {
            _departmentRepository = new ();
        }
        public Department get(string departmentName)
        {
            return _departmentRepository.Get(n => n.Name == departmentName);
        }
        public Department Get(int id)
        {
            return _departmentRepository.Get(n => n.Id == id);
        }
        public Department Creat(Department department)
        {
            department.Id = count;
            var existDepartment = _departmentRepository.Get(n=>n.Name == department.Name);
            if (existDepartment is null) return null;
            var result = _departmentRepository.Creat(department);
            if (result)
            {
                
                return department;
            }
            else
            {
                return null;
            }
        }

        public Department Delete(int id)
        {
           var existDepartment= _departmentRepository.Get(d=>d.Id == id);
            if (existDepartment is null) return null;
            if (existDepartment.Capacity == 0)
            {
                if (_departmentRepository.Delete(existDepartment))
                {
                    return existDepartment;
                }
                else return null;
            }
            else 
            {
                
            }

        }

      

       

        public List<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Department> GetAll(int capacity)
        {
            throw new NotImplementedException();
        }

        public List<Department> GetAll(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Department Update(int id, Department department, string departmentName)
        {
            throw new NotImplementedException();
        }
    }
}
