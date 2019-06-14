using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CustomFilter
    {
        public int pageSize { get; set; }
        public int skip { get; set; }

        public string streetSearchValue { get; set; }
        public string salarySearchValue { get; set; }
        public string ageSearchValue { get; set; }
    }
}
