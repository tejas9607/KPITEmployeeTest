using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeDbManagement.DataEntity;
using ViewModels;

namespace EmployeeDbManagement
{
    public class EmployeeManager
    {

        private EmployeeManager()
        {

        }
        private static EmployeeManager _instance;
        public static EmployeeManager Instance
        {
            get
            {
                if (_instance == null)
                {

                    _instance = new EmployeeManager();
                }
                return _instance;
            }

        }

        public IEnumerable<EmployeeListViewModel> GetEmployeeList(CustomFilter customFilter,out int totalCount)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {

                var res = (from emp in _dbContext.Employees
                           select new EmployeeListViewModel
                           {
                               Age = emp.Age,
                               Id = emp.Id,
                               Name = emp.Name,
                               Salary = emp.Salary,
                               MaritalStatus = emp.MaritalStatu.Status,
                               Street = emp.Locations.FirstOrDefault().Street,
                               State = emp.Locations.FirstOrDefault().State,
                               City = emp.Locations.FirstOrDefault().City
                           }
                           );

                if (!string.IsNullOrEmpty(customFilter.salarySearchValue))
                {
                    res = res.Where(
                            p => p.Salary.ToString().Trim().Contains(customFilter.salarySearchValue));
                }
                if (!string.IsNullOrEmpty(customFilter.ageSearchValue))
                {
                    res = res.Where(p => p.Age.ToString().Contains(customFilter.ageSearchValue));
                }
                if (!string.IsNullOrEmpty(customFilter.streetSearchValue))
                {
                    res = res.Where(p => p.Street.Trim().ToLower().Contains(customFilter.streetSearchValue)
                    || p.City.Trim().ToLower().Contains(customFilter.streetSearchValue)
                    || p.State.Trim().ToLower().Contains(customFilter.streetSearchValue));
                }
                totalCount = res.Count();
                return res.OrderBy(p => p.Id)
                       .Skip(customFilter.skip).Take(customFilter.pageSize).ToList();
            }

        }
        public IEnumerable<EmployeeListViewModel> GetEmployeeList()
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {

                var res = (from emp in _dbContext.Employees
                           select new EmployeeListViewModel
                           {
                               Age = emp.Age,
                               Id = emp.Id,
                               Name = emp.Name,
                               Salary = emp.Salary,
                               MaritalStatus = emp.MaritalStatu.Status,
                               Street = emp.Locations.FirstOrDefault().Street,
                               State = emp.Locations.FirstOrDefault().State,
                               City = emp.Locations.FirstOrDefault().City
                           }
                           );

                return res.ToList();
            }

        }
        public void AddEmployee(Employee emp)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                _dbContext.Employees.Add(emp);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                Employee emp = _dbContext.Employees.Find(id);
                _dbContext.Employees.Remove(emp);
                _dbContext.SaveChanges();
            }
        }

        public bool IsEmployeeExist(string empName)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                return _dbContext.Employees.Any(m => m.Name.Equals(empName, StringComparison.OrdinalIgnoreCase));
            }
        }

        public Employee GetEmployee(string empName)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                return _dbContext.Employees.Where(m => m.Name.Equals(empName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
        }
    }
}
