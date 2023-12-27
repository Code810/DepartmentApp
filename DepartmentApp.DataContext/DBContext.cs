using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DepartmentApp.DataContext
{
    public static class DBContext
    {
        public static List<Department> Departments { get; set; }
        public static List<Employee> Employees { get; set; }
         static DBContext()
        {
            Departments = new();
            Employees = new();
            DepartmentsFromTextToList();
            EmployessFromTextToList();

        }
        public static void DepartmentsAddFile()
        {
            string filepath = "C:\\Users\\seidb\\OneDrive\\Masaüstü\\DepartmentApp\\Departments.txt";
            List<string> lines = new();
            foreach (var item in Departments)
            {
                lines.Add($"{item.Id},{item.Name},{item.Capacity}");
            }
            File.WriteAllLines(filepath, lines);
        }

        public static void DepartmentsFromTextToList()
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
        }

        public static void EmployeesAddFile()
        {
            string filepath = "C:\\Users\\seidb\\OneDrive\\Masaüstü\\DepartmentApp\\Employees.txt";
            List<string> lines = new();
            foreach (var item in Employees)
            {
                lines.Add($"{item.Id},{item.Name},{item.Surname},{item.Age},{item.Adress},{item.Email},{item.Password}," +
                    $"{item.department.Id},{item.department.Name},{item.department.Capacity}");
            }
            File.WriteAllLines(filepath, lines);
        }

        public static void EmployessFromTextToList()
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
                employee.department.Id = int.Parse(exsistLine[7]);
                employee.department.Name= exsistLine[8];
                employee.department.Capacity= int.Parse(exsistLine[9]);



                Employees.Add(employee);
            }
        }

    }
}
