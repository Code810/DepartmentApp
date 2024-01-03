using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.Business.Interface
{
    public interface IAccount
    {
        public bool PaswordandEmailChecker(string pasword, string email);
        public Employee Login(string password, string email);
    }
}
