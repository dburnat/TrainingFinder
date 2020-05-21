using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;

namespace TrainingFinder.Models
{
    public class BodyDimension
    {
        public int Id { get; set; }
        public string Neck { get; set; }
        public string Shoulder { get; set; }
        public string Chest { get; set; }
        public string Wrist { get; set; }
        public string Bicep { get; set; }
        public string Thigh { get; set; }
        public string Calf { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
    }
}
