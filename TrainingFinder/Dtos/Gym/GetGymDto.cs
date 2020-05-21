using System.Collections.Generic;
using TrainingFinder.Dtos.Training;

namespace TrainingFinder.Dtos.Gym
{
    public class GetGymDto
    {
        public int GymId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<GetTrainingDtoWithoutUsers> Trainings { get; set; }
    }
}