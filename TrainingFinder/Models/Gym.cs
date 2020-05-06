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
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Number")]
        public string Number { get; set; }

        [Display(Name = "PostCode")]
        public string PostCode { get; set; }
        [Display(Name = "IsAddedByUser")]
        public bool IsAddedByUser { get; set; } = false;

        public ICollection<Training> Trainings { get; set; }
    }
}