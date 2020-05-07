
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingFinder.Models
{
    public class TrainingAppUser
    {
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}