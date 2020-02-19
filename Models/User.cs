using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject.Models
{
    public class User : IdentityUser
    {
        //public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [PersonalData]
        public string LastName { get; set; }

        public virtual ICollection<Summary> Summaries { get; set; }

        

    }
}
