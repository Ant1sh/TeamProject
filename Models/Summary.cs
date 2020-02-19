using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject.Models
{
    public class Summary
    {
        //public int SummaryId { get; set; }
        [Required]
        public int TourId { get; set; }
        
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual User User { get; set; }
    }
}
