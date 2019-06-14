using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ViewModels;

namespace ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20,MinimumLength =2,ErrorMessage ="Please enter correct name")]
        public string Name { get; set; }

        [Range(0, 60)]
        [Required]
        public Nullable<int> Age { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter correct value")]
        public decimal Salary { get; set; }

        [Display(Name ="Marital Status")]
        public int MaritalStatusId { get; set; }

        
        public int LocationId { get; set; }

        public LocationViewModel Location { get; set; }
        public MaritalStatusViewModel MaritalStatu { get; set; }

    }
}