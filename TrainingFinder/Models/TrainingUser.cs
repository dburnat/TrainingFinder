using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;

namespace TrainingFinder.Models
{
    public class TrainingUser
    {
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
