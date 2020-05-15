using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserRepository _userRepository;

        public TrainingUserService(ApplicationDbContext ctx, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _ctx = ctx;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
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

        public ResultModel<IQueryable<GetUserTrainingsDto>> GetUserTrainings(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);
                if (user.Data == null)
                {
                    return new ResultModel<IQueryable<GetUserTrainingsDto>>(null, StatusCodes.Status404NotFound);
                }

                if (!user.isStatusCodeSuccess())
                {
                    return new ResultModel<IQueryable<GetUserTrainingsDto>>(null, StatusCodes.Status400BadRequest);
                }

                var trainings = _ctx.TrainingUsers.Where(c => c.UserId == id);
                var model = trainings.ProjectTo<GetUserTrainingsDto>(new MapperConfiguration(cfg =>
                    cfg.CreateMap<TrainingUser, GetUserTrainingsDto>()));
                if (!trainings.Any())
                {
                    return new ResultModel<IQueryable<GetUserTrainingsDto>>(null, StatusCodes.Status404NotFound);
                }

                return new ResultModel<IQueryable<GetUserTrainingsDto>>(model, StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return new ResultModel<IQueryable<GetUserTrainingsDto>>(null, StatusCodes.Status500InternalServerError);
            }
        }
    }
}