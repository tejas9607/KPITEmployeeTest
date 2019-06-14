using EmployeeDbManagement.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDbManagement
{
    public class MaritalStatusManager
    {
        private MaritalStatusManager()
        {

        }

        private static MaritalStatusManager _instance;

        public static MaritalStatusManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MaritalStatusManager();

                return _instance;
            }
        }

        public List<MaritalStatus> GetMaritalStatusList()
        {
            using (KPITEmployeeTestEntities _dbContext = new KPITEmployeeTestEntities())
            {
                return _dbContext.MaritalStatus1.ToList();
            }
        }
    }
}
