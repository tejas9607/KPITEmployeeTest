using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeDbManagement.DataEntity;

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

        public List<Employee> GetEmployeeList()
        {
            using (EmployeeTestEntities _dbContext = new EmployeeTestEntities())
            {
                return _dbContext.Employees.ToList();
            }
            
        }

        public void AddEmployee(Employee emp)
        {
            using (EmployeeTestEntities _dbContext = new EmployeeTestEntities())
            {
                _dbContext.Employees.Add(emp);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (EmployeeTestEntities _dbContext = new EmployeeTestEntities())
            {
                Employee emp = _dbContext.Employees.Find(id);
                _dbContext.Employees.Remove(emp);
                _dbContext.SaveChanges();
            }
        }

        public bool IsEmployeeExist(string empName)
        {
            using (EmployeeTestEntities _dbContext = new EmployeeTestEntities())
            {
                return _dbContext.Employees.Any(m => m.Name.Equals(empName, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
