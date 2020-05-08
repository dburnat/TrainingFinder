using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lw.SeniorLoans.Compliance.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TrainingFinder.Data;
using TrainingFinder.Dtos.Training;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Entities;
using TrainingFinder.Models;

namespace TrainingFinder.Services.TrainingUserService
{
    public class TrainingUserService : ITrainingUserService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TrainingUserService(ApplicationDbContext ctx, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _ctx = ctx;
        }
        public ResultModel<GetTrainingDto> AddTrainingUser(AddTrainingUserDto newTrainingUser)
        {
            /*ServiceResponse<GetTrainingDto> response = new ServiceResponse<GetTrainingDto>();*/

            try
            {
                Training training = _ctx.Trainings.FirstOrDefault(t => t.TrainingId == newTrainingUser.TrainingId);                    
                if (training == null)
                {
                    return new ResultModel<GetTrainingDto>(null, 404);
                }

                User user = _ctx.Users.FirstOrDefault(u => u.Id == newTrainingUser.UserId);
                if (user == null)
                {
                    return new ResultModel<GetTrainingDto>(null, 404);
                }

                TrainingUser trainingUser = new TrainingUser
                {
                    Training = training,
                    User = user
                };

                _ctx.TrainingUsers.Add(trainingUser);
                _ctx.SaveChanges();

                return new ResultModel<GetTrainingDto>(null, 201);
            }
            catch (Exception)
            {
                return new ResultModel<GetTrainingDto>(null, 404);
            }           
            

        }
    }
}
