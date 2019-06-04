using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using EmployeeDbManagement;
using EmployeeDbManagement.DataEntity;

namespace KPITEmployeeTest.Models
{
    public class BusinessLayer
    {
        public bool IsEmployeeExist(string empName)
        {
            return EmployeeManager.Instance.IsEmployeeExist(empName);
        }
        public List<EmployeeViewModel> GetEmployeeList()
        {
            var empList = (from emp in EmployeeManager.Instance.GetEmployeeList()
                           select new EmployeeViewModel()
                           {
                               Age = emp.Age,
                               Id = emp.Id,
                               MaritalStatus = emp.MarritalStatus,
                               Name = emp.Name,
                               Location = emp.Location,
                               Salary = emp.Salary ?? 0
                           }).ToList();
            return empList;
        }

        public void AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var dbEmployee = new Employee()
            {
                Id = employeeViewModel.Id,
                Age = employeeViewModel.Age,
                Location = employeeViewModel.Location,
                MarritalStatus = employeeViewModel.MaritalStatus,
                Name = employeeViewModel.Name,
                Salary = employeeViewModel.Salary
            };
            EmployeeManager.Instance.AddEmployee(dbEmployee);
        }

        public IEnumerable<EmployeeViewModel> Search(string keyword)
        {
            var result = EmployeeManager.Instance.GetEmployeeList().Where(
                            p => p.Salary.ToString().Contains(keyword)
                           || p.Age.ToString().Contains(keyword)
                           || p.Location.ToLower().Contains(keyword.ToLower())).ToList();

            var empList = (from emp in result
                           select new EmployeeViewModel()
                           {
                               Age = emp.Age,
                               Id = emp.Id,
                               MaritalStatus = emp.MarritalStatus,
                               Name = emp.Name,
                               Location = emp.Location,
                               Salary = emp.Salary ?? 0
                           }).ToList();
            return empList;
        }

        public void Delete(int id)
        {
            EmployeeManager.Instance.Delete(id);
        }
    }
}