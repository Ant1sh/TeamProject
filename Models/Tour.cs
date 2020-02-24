using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject.Models
{
    public class Tour
    {
        public int TourId { get; set; }
        [Required]
        [StringLength(256)]
        public string Destination { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [StringLength(300)]
        public string Discription { get; set; }
        [Required]
        public DateTime Departure { get; set; }
        [Required]
        public DateTime Arrival { get; set; }

        public virtual ICollection<Summary> Summaries { get; set; }

        public string ShortDiscription
        {
            get
            {
                return Discription.Substring(0,12) + "...";
            }
        }

    }
}
