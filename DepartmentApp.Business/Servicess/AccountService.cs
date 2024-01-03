using DepartmentApp.Business.Interface;
using DepartmentApp.DataContext.Repository;
using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DepartmentApp.Business.Servicess
{
    public class AccountService : IAccount
    {
        private readonly EmployeeRepository _employeeRepository;
        public AccountService()
        {
            _employeeRepository = new ();
        }
        public bool PaswordandEmailChecker(string pasword, string email)
        {
            Regex RegEmail = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Regex RegPassword = new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

            return (RegPassword.IsMatch(pasword) && RegEmail.IsMatch(email));
        }

        public Employee Login(string password, string email) 
        {
            Employee employee = _employeeRepository.Get(e => e.Email.ToLower() == email.ToLower() && e.Password == password);

            if (employee is null) return null;

            return employee;
        }
    }
}
