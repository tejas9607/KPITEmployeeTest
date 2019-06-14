using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using EmployeeDbManagement;
using EmployeeDbManagement.DataEntity;
using ViewModels;

namespace KPITEmployeeTest.Models
{
    public class BusinessLayer
    {
        public bool IsEmployeeExist(string empName)
        {
            return EmployeeManager.Instance.IsEmployeeExist(empName);
        }

        public void AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var dbEmployee = new Employee()
            {
                Id = employeeViewModel.Id,
                Age = employeeViewModel.Age ?? 0,
                MaritalStatusId = employeeViewModel.MaritalStatusId,
                Name = employeeViewModel.Name,
                Salary = employeeViewModel.Salary,

            };
            EmployeeManager.Instance.AddEmployee(dbEmployee);
            AddLocation(employeeViewModel);
        }
        private void AddLocation(EmployeeViewModel employeeViewModel)
        {
            Employee emp = EmployeeManager.Instance.GetEmployee(employeeViewModel.Name);
            var dbLocation = new Location
            {
                City = employeeViewModel.Location.City,
                State = employeeViewModel.Location.State,
                Street = employeeViewModel.Location.Street,
                EmployeeId = emp.Id
            };

            LocationManager.Instance.AddLocation(dbLocation);
        }

        public IEnumerable<EmployeeListViewModel> Search(string keyword)
        {
            var result = EmployeeManager.Instance.GetEmployeeList().Where(
                            p => p.Salary.ToString().Contains(keyword)
                           || p.Age.ToString().Contains(keyword)
                           || p.Street.ToLower().Contains(keyword.ToLower())
                           || p.City.ToLower().Contains(keyword.ToLower())
                           || p.State.ToLower().Contains(keyword.ToLower())).ToList();

            return result;
        }

        public IEnumerable<EmployeeListViewModel> GetEmployeeList(CustomFilter customFilter,out int totalCount)
        {
            int totalCount1 = 0;
            var result = EmployeeManager.Instance.GetEmployeeList(customFilter, out totalCount1).ToList();
            totalCount = totalCount1;
            return result;
        }

        public void Delete(int id)
        {
            LocationManager.Instance.Delete(id);
            EmployeeManager.Instance.Delete(id);
        }
        #region Marital Status
        public List<MaritalStatus> GetMaritalStatusList()
        {
            return MaritalStatusManager.Instance.GetMaritalStatusList();
        }
        #endregion
    }
}