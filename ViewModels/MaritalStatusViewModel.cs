using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MaritalStatusViewModel
    {
        public int MaritalStatusId { get; set; }
        [Required]
        [Range(0, 10)]
        public string Status { get; set; }
    }
}
