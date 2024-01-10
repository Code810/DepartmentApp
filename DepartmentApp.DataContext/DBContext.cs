using DepartmentApp.Domain.Models;
using DepartmentApp.Domain.Models.Helpers;
using System.Diagnostics.SymbolStore;
using System.Text;
namespace DepartmentApp.DataContext
{
    public static class DBContext
    {
        public static List<Department> Departments;
        public static List<Employee> Employees;
         static DBContext()
        {
            Departments = new();
            Employees = new();
            DepartmentsFromTextToList();
            EmployessFromTextToList();

        }
        public static void DepartmentsAddFile()
        {
            string fileName = "Departments.txt";
            string filepath = "C:\\Users\\seidb\\OneDrive\\Masaüstü\\DepartmentApp";
            string fullFilePath=System.IO.Path.Combine(filepath, fileName);
            if (!System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Create(fullFilePath);
            }
            List<string> lines = new();
            foreach (var item in Departments)
            {
                lines.Add($"{item.Id},{item.Name},{item.Capacity}");
            }
            File.WriteAllLines(filepath, lines);
        }

        public static List<Department> DepartmentsFromTextToList()
        {
            string filepath = "C:\\Users\\seidb\\OneDrive\\Masaüstü\\DepartmentApp\\Departments.txt";
            List<string> lines = File.ReadAllLines(filepath, Encoding.UTF8).ToList();

            foreach (var item in lines)
            {
                string[] exsistLine = item.Split(',');
                Department department = new ();
                department.Id = int.Parse(exsistLine[0]);
                department.Name = exsistLine[1];
                department.Capacity = int.Parse(exsistLine[2]);
                Departments.Add(department);
            }
            return Departments;
        }

        public static void EmployeesAddFile()
        {
            string fileName = "Employees.txt";
            string filepath = "C:\\Users\\seidb\\OneDrive\\Masaüstü\\DepartmentApp";
            string fullFilePath = System.IO.Path.Combine(filepath, fileName);
            if (!System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Create(fullFilePath);
            }
            List<string> lines = new();
            foreach (var item in Employees)
            {
                lines.Add($"{item.Id},{item.Name},{item.Surname},{item.Age},{item.Adress},{item.Email},{item.Password}," +
                    $"{item.department.Name},{item.Rol}");
            }
            File.WriteAllLines(filepath, lines);
        }

        public static List<Employee> EmployessFromTextToList()
        {
            string filepath = "C:\\Users\\seidb\\OneDrive\\Masaüstü\\DepartmentApp\\Employees.txt";
            List<string> lines = File.ReadAllLines(filepath, Encoding.UTF8).ToList();

            foreach (var item in lines)
            {
                string[] exsistLine = item.Split(',');
                Employee employee = new ();
                employee.Id= int.Parse(exsistLine[0]);
                employee.Name = exsistLine[1];
                employee.Surname = exsistLine[2];
                employee.Age = int.Parse(exsistLine[3]);
                employee.Adress = exsistLine[4];
                employee.Email = exsistLine[5];
                employee.Password = exsistLine[6];
                employee.department = Departments.Find(d => d.Name == exsistLine[7]);
                if (exsistLine[8]=="Admin")
                {
                    employee.Rol = Roles.Admin;
                }
                else
                {
                    employee.Rol = Roles.User;
                }
                Employees.Add(employee);
            }
            return Employees;
        }

        public static void SaveChange()
        {
            DepartmentsAddFile();
            EmployeesAddFile();
        }
    }
}
