using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Please enter correct street")]
        public string Street { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Please enter correct city")]
        public string City { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Please enter correct state")]
        public string State { get; set; }

    }
}
