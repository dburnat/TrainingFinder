using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingFinder.Models
{
    public class Gym
    {
        [Required]
        public int GymId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        [Required]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
