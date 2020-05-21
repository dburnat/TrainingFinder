using System;
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;
using TrainingFinder.Dtos.Training;
using TrainingFinder.Models;

namespace TrainingFinder.Dtos.TrainingUser
{
    public class UserTrainingsDto
    {
        [MapTo("Training")]
        public TrainingDtoWithoutUsers Training { get; set; }
    }
}