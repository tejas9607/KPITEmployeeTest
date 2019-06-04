using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KPITEmployeeTest.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Range(0, 60)]
        public Nullable<int> Age { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [StringLength(500)]
        public string Location { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter correct value")]
        public decimal Salary { get; set; }
    }
}