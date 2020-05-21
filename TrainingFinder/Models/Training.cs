using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingFinder.Models
{
    public class Training
    {
        [Required]
        public int TrainingId { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date and time")]
        public DateTime DateTime { get; set; }

        [Display(Name ="GymId")]
        public int GymId { get; set; }


        public virtual  Gym Gym { get; set; }

        public virtual ICollection<TrainingUser> TrainingUsers { get; set; }

    }
}