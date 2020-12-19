using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingFinder.Entities
{
    public class UserProfile
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
        public float Weight { get; set; }
        }
}