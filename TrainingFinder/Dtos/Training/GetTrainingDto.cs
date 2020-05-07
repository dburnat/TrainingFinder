using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Dtos.User;

namespace TrainingFinder.Dtos.Training
{
    public class GetTrainingDto
    {
        public int TrainingId { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int GymId { get; set; }
        public List<GetUserDto> Users { get; set; }
    }
}
