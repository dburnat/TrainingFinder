using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;
using TrainingFinder.Models;
using TrainingFinder.Models.Users;

namespace TrainingFinder.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AppUser>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<GymModel, Gym>();
            CreateMap<TrainingModel, Training>();
        }
    }
}
