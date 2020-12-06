using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TrainingFinder.Entities;
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
                var gym = _ctx.Gyms.Find(entity.GymId);

                gym.Trainings.Add(entity);

                _ctx.SaveChanges();
                return true;
            }
            else if (entity.TrainingId != 0)
            {
                var entityToUpdate = _ctx.Trainings
                    .Where(t => t.TrainingId == entity.TrainingId)
                    .Include(t => t.Gym)
                    .SingleOrDefault();

                if (entityToUpdate == null)
                    return false;

                entityToUpdate.Description = entity.Description;
                entityToUpdate.DateTime = entity.DateTime;
                entityToUpdate.Gym = entity.Gym;
                entityToUpdate.TrainingUsers = entity.TrainingUsers;

                _ctx.SaveChanges();

                return true;
            }
            return false;
        }

        public ResultModel<Training> Create(Training entity)
        {
            try
            {
                if (entity.TrainingId == 0)
                {
                    var gym = _ctx.Gyms.Find(entity.GymId);

                    gym.Trainings.Add(entity);
                    _ctx.SaveChanges();
                    return new ResultModel<Training>(entity, 201);
                }
                else
                {
                    if (entity != null)
                        return new ResultModel<Training>(entity, 409);
                }
                return new ResultModel<Training>(entity, 409);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Training>(null, 500);
            }
        }

        public ResultModel<Training> Delete(int id)
        {
            try
            {
                var entityToDelete = _ctx.Trainings.FirstOrDefault(x => x.TrainingId == id);

                if (entityToDelete == null)
                    return new ResultModel<Training>(null, 404);

                _ctx.Trainings.Remove(entityToDelete);
                _ctx.SaveChanges();
                return new ResultModel<Training>(null, 204);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Training>(null, 500);
            }
        }       

        public ResultModel<Training> GetById(int id)
        {
            try
            {
                var entity = _ctx.Trainings.FirstOrDefault(x => x.TrainingId == id);

                if (entity == null)
                    return new ResultModel<Training>(null, 404);
                else
                    return new ResultModel<Training>(entity, 200);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Training>(null, 500);
            }
        }      

        public ResultModel<Training> Update(Training entity)
        {
            try
            {
                var getResponse = _ctx.Trainings
                    .Where(t => t.TrainingId == entity.TrainingId)
                    .Include(t => t.Gym)
                    .SingleOrDefault();

                if (getResponse == null)
                    return new ResultModel<Training>(null, 404);

                _ctx.Entry(getResponse).State = EntityState.Detached;

                _ctx.Entry(getResponse).CurrentValues.SetValues(entity);

                _ctx.SaveChanges();

                return new ResultModel<Training>(entity, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Training>(null, 500);
            }
        }
    }
}