using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Dtos.Training;
using TrainingFinder.Dtos.TrainingUser;
using TrainingFinder.Entities;
using Lw.SeniorLoans.Compliance.BusinessLogic;
using TrainingFinder.Models;

namespace TrainingFinder.Services.TrainingUserService
{
    public interface ITrainingUserService
    {
        ResultModel<GetTrainingDto> AddTrainingUser(AddTrainingUserDto newTrainingUser);
        ResultModel<List<UserTrainingsDto>> GetUserTrainings(int id);
    }
}