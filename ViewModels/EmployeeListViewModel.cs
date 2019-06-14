using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class EmployeeListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Nullable<int> Age { get; set; }

        public decimal Salary { get; set; }

        public string MaritalStatus { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
