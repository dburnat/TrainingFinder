using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingFinder.Models
{
    public class AppUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        public virtual ICollection<TrainingAppUser> TrainingAppUsers { get; set; }

    }
}