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
        public bool DeleteTraining(int id)
        {
            var entityToDelete = _ctx.Trainings.FirstOrDefault(t => t.TrainingId == id);
            if (entityToDelete == null)
                return false;
            else if (entityToDelete != null)
            {
                _ctx.Trainings.Remove(entityToDelete);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }
        public bool SaveTraining(Training entity)
        {
            if (entity.TrainingId == 0)
            {
                var res = _ctx.Trainings.Add(entity);
                _ctx.SaveChanges();
                return true;
            }
            else if (entity.TrainingId != 0)
            {
                var entityToUpdate = _ctx.Trainings.FirstOrDefault(t => t.TrainingId == entity.TrainingId);

                if (entityToUpdate == null)
                    return false;

                entityToUpdate.Description = entity.Description;
                entityToUpdate.DateTime = entity.DateTime;

                _ctx.SaveChanges();

                return true;
            }
            return false;
        }

        public ResultModel<Training> Create(Training entity)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Training> Delete(int id)
        {
            throw new NotImplementedException();
        }       

        public ResultModel<Training> GetBtId(int id)
        {
            throw new NotImplementedException();
        }      

        public ResultModel<Training> Update(Training entity)
        {
            throw new NotImplementedException();
        }
    }
}
