using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public interface ITrainingRepository
    {
        IQueryable<Training> trainings { get; }
        bool DeleteTraining(int id);
        bool SaveTraining(Training entity);
        ResultModel<Training> GetBtId(int id);
        ResultModel<Training> Update(Training entity);
        ResultModel<Training> Create(Training entity);
        ResultModel<Training> Delete(int id);
    }
}
