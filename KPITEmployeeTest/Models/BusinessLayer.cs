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
        Logger logger = new Logger();
        public bool IsEmployeeExist(string empName)
        {
            return EmployeeManager.Instance.IsEmployeeExist(empName);
        }

        public void AddEmployee(EmployeeViewModel employeeViewModel)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogMessage(ex.ToString());
            }
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

        public IEnumerable<EmployeeListViewModel> GetEmployeeList(CustomFilter customFilter, out int totalCount)
        {
            try
            {
                int totalCount1 = 0;
                var result = EmployeeManager.Instance.GetEmployeeList(customFilter, out totalCount1).ToList();
                totalCount = totalCount1;

                var res = totalCount / 0;
                return result;
            }
            catch (Exception ex)
            {
                totalCount = 0;
                logger.LogMessage(ex.ToString());
                return Enumerable.Empty<EmployeeListViewModel>();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                LocationManager.Instance.Delete(id);
                EmployeeManager.Instance.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogMessage(ex.ToString());
                return false;
            }
        }
        #region Marital Status
        public List<MaritalStatus> GetMaritalStatusList()
        {
            try
            {
                return MaritalStatusManager.Instance.GetMaritalStatusList();
            }
            catch (Exception ex)
            {
                logger.LogMessage(ex.ToString());
                return new List<MaritalStatus>();
            }
        }
        #endregion
    }
}