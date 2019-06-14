using EmployeeDbManagement.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDbManagement
{
    public class LocationManager
    {
        private LocationManager()
        {

        }

        private static LocationManager _instance;

        public static LocationManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LocationManager();
                return _instance;
            }
        }

        public void AddLocation(Location location)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                _dbContext.Locations.Add(location);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int empId)
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                Location location = _dbContext.Locations.Where(p=>p.EmployeeId == empId).FirstOrDefault();
                _dbContext.Locations.Remove(location);
                _dbContext.SaveChanges();
            }
        }
    }
}
