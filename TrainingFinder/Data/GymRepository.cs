using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public class GymRepository : IGymRepository
    {
        private readonly ApplicationDbContext _ctx;
        public GymRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Gym> Gyms => _ctx.Gyms;
        public bool DeleteGym(int id)
        {
            var entityToDelete = _ctx.Gyms.FirstOrDefault(g => g.GymId == id);

            if (entityToDelete != null)
            {
                _ctx.Gyms.Remove(entityToDelete);
                _ctx.SaveChanges();
                return true;
            }
            else
                return false;

        }
        public bool SaveGym(Gym entity)
        {
            if (entity.GymId == 0)
            {
                _ctx.Gyms.Add(entity);
                _ctx.SaveChanges();
                return true;
            }
            else if (entity.GymId != 0)
            {
                var entityToUpdate = _ctx.Gyms.FirstOrDefault(g => g.GymId == entity.GymId);

                entityToUpdate.Name = entity.Name;
                entityToUpdate.City = entity.City;
                entityToUpdate.Street = entity.Street;
                entityToUpdate.Number = entity.Number;
                entityToUpdate.PostCode = entity.PostCode;
                entityToUpdate.Trainings = entity.Trainings;

                if (entityToUpdate == null)
                    return false;

                _ctx.SaveChanges();
                return true;
            }
            return false;
        }


        public ResultModel<Gym> Create(Gym entity)
        {
            try
            {
                if (entity.GymId == 0)
                {
                    _ctx.Gyms.Add(entity);
                    _ctx.SaveChanges();
                    return new ResultModel<Gym>(entity, 201);
                }
                else
                {
                    if (entity != null)
                        return new ResultModel<Gym>(entity, 409);
                }
                return new ResultModel<Gym>(entity, 409);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Gym>(null, 500);
            }
        }

        public ResultModel<Gym> Delete(int id)
        {
            try
            {
                var entityToDelete = _ctx.Gyms.FirstOrDefault(x => x.GymId == id);

                if (entityToDelete == null)
                    return new ResultModel<Gym>(null, 404);

                _ctx.Gyms.Remove(entityToDelete);
                _ctx.SaveChanges();
                return new ResultModel<Gym>(null, 204);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Gym>(null, 500);
            }
        }       
        

        public ResultModel<IEnumerable<Gym>> GetAllInCity(string city)
        {
            try
            {
                IEnumerable<Gym> gyms;
                if (string.IsNullOrWhiteSpace(city))
                    gyms = _ctx.Gyms.ToList();
                else
                    gyms = _ctx.Gyms.Where(x => x.City == city).ToList();

                return new ResultModel<IEnumerable<Gym>>(gyms, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<IEnumerable<Gym>>(null, 500);
            }
        }

        public ResultModel<Gym> GetById(int id)
        {
            try
            {
                var entity = _ctx.Gyms.FirstOrDefault(x => x.GymId == id);

                if (entity == null)
                    return new ResultModel<Gym>(null, 404);
                else
                    return new ResultModel<Gym>(entity, 200);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Gym>(null, 500);
            }
        }       

        public ResultModel<Gym> Update(Gym entity)
        {
            try
            {
                var getResponse = _ctx.Gyms.FirstOrDefault(x => x.GymId == entity.GymId);

                if (getResponse == null)
                    return new ResultModel<Gym>(null, 404);

                _ctx.Entry(getResponse).State = EntityState.Detached;

                var updateResponse = _ctx.Update(entity);
                _ctx.SaveChanges();

                return new ResultModel<Gym>(entity, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Gym>(null, 500);
            }
        }
        public void Dispose()
        {
            _ctx?.Dispose();
        }
    }
}