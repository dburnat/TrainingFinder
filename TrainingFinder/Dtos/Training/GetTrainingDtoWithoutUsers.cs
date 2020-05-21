using System;
using AutoMapper.Configuration.Conventions;
using Newtonsoft.Json;
using TrainingFinder.Models.JSON;

namespace TrainingFinder.Dtos.Training
{
    public class GetTrainingDtoWithoutUsers
    {
        [MapTo("TrainingId")]
        public int TrainingId { get; set; }
        [MapTo("Description")]
        public string Description { get; set; }
        [MapTo("DateTime")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateTime { get; set; }
        [MapTo("GymId")]
        public int GymId { get; set; }
    }
}