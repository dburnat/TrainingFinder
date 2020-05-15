using AutoMapper.Configuration.Conventions;
using TrainingFinder.Models;

namespace TrainingFinder.Dtos.TrainingUser
{
    public class GetUserTrainingsDto
    {
        [MapTo("UserId")]
        public int UserId { get; set; }
        [MapTo("TrainingId")]
        public int TrainingId { get; set; }
    }
}