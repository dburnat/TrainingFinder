
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingFinder.Models
{
    public class TrainingAppUser
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}