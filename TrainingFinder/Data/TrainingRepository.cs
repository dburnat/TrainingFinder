using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public class TrainingRepository : ITrainingRepository
    {
        private ApplicationDbContext _ctx;
        public TrainingRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Training> Trainings => _ctx.Trainings;

        public ResultModel<Training> Create(Training entity)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Training> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTraining(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Training> GetBtId(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveTraining(Training entity)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Training> Update(Training entity)
        {
            throw new NotImplementedException();
        }
    }
}
