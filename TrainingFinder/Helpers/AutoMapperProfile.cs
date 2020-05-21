using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Dtos.Training;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Dtos.User;
using TrainingFinder.Entities;
using TrainingFinder.Models;
using TrainingFinder.Models.Users;

namespace TrainingFinder.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<GymModel, Gym>();
            CreateMap<TrainingModel, Training>();
            CreateMap<User, GetUserDto>();
            CreateMap<Training, TrainingDtoWithoutUsers>();
            CreateMap<Training, GetTrainingDto>()
                .ForMember(dto => dto.Users, u => u.MapFrom(tu => tu.TrainingUsers.Select(x => x.User)));
        }
    }
}