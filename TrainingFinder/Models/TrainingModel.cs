using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrainingFinder.Models.JSON;

namespace TrainingFinder.Models
{
    public class TrainingModel
    {
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Date and time")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateTime { get; set; }
        public int GymId { get; set; }
    }
}
