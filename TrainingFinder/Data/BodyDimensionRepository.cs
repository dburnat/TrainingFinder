using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public class BodyDimensionRepository : IBodyDimensionRepository
    {
        private readonly ApplicationDbContext _ctx;
        public BodyDimensionRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<BodyDimension> BodyDimensions => _ctx.BodyDimensions;

        public bool SaveDimensions(BodyDimension bodyDimension)
        {
            if (bodyDimension.Id == 0)
            {
                var user = _ctx.Users.SingleOrDefault(x => x.Id == bodyDimension.User.Id);
                if (user != null)
                    user.BodyDimensions.Add(bodyDimension);

                _ctx.SaveChanges();
                return true;
            }
            else
            {
                var entityToUpdate = _ctx.BodyDimensions.SingleOrDefault(x => x.Id == bodyDimension.Id);

                if (entityToUpdate == null)
                    return false;

                entityToUpdate.Neck = bodyDimension.Neck;
                entityToUpdate.Shoulder = bodyDimension.Shoulder;
                entityToUpdate.Thigh = bodyDimension.Thigh;
                entityToUpdate.Bicep = bodyDimension.Bicep;
                entityToUpdate.Calf = bodyDimension.Calf;
                entityToUpdate.Chest = bodyDimension.Chest;
                entityToUpdate.Date = bodyDimension.Date;
                entityToUpdate.Wrist = bodyDimension.Wrist;

                _ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteDimensions(int id)
        {
            var entityToDelete = _ctx.BodyDimensions.SingleOrDefault(x => x.Id == id);

            if (entityToDelete != null)
            {
                _ctx.BodyDimensions.Remove(entityToDelete);
                _ctx.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public ResultModel<BodyDimension> Create(BodyDimension bodyDimension, int userId)
        {
            try
            {
                if (bodyDimension.Id == 0)
                {
                    var user = _ctx.Users.SingleOrDefault(x => x.Id == userId);

                    user.BodyDimensions.Add(bodyDimension);
                    _ctx.SaveChanges();

                    return new ResultModel<BodyDimension>(bodyDimension, 201);
                }
                else if (bodyDimension != null)
                {
                    return new ResultModel<BodyDimension>(bodyDimension, 409);
                }

                return new ResultModel<BodyDimension>(null, 409);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<BodyDimension>(null, 500);
            }

        }

        public ResultModel<BodyDimension> Delete(int id)
        {
            try
            {
                var entityToDelete = _ctx.BodyDimensions.SingleOrDefault(x => x.Id == id);

                if (entityToDelete != null)
                {
                    var user = _ctx.Users.SingleOrDefault(x => x.Id == entityToDelete.User.Id);

                    user.BodyDimensions.Remove(entityToDelete);
                    _ctx.SaveChanges();

                    return new ResultModel<BodyDimension>(null, 204);
                }
                else
                    return new ResultModel<BodyDimension>(null, 404);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<BodyDimension>(null, 500);
            }
        }

        public ResultModel<BodyDimension> Update(BodyDimension bodyDimension)
        {
            try
            {
                var getResponse = _ctx.BodyDimensions
                    .Where(t => t.Id == bodyDimension.Id)
                    .Include(t => t.User)
                    .SingleOrDefault();

                if (getResponse == null)
                    return new ResultModel<BodyDimension>(null, 404);

                _ctx.Entry(getResponse).State = EntityState.Detached;

                _ctx.Entry(getResponse).CurrentValues.SetValues(bodyDimension);

                _ctx.SaveChanges();

                return new ResultModel<BodyDimension>(bodyDimension, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<BodyDimension>(null, 500);
            }
        }

        public ResultModel<IEnumerable<BodyDimension>> GetAllByUser(int id)
        {
            try
            {
                var user = _ctx.Users.SingleOrDefault(x => x.Id == id);
                IEnumerable<BodyDimension> bodyDimensions;
                if (user != null)
                {
                    bodyDimensions = _ctx.BodyDimensions.Where(x => x.User.Id == user.Id).ToList();

                    return new ResultModel<IEnumerable<BodyDimension>>(bodyDimensions, 200);
                }
                else
                    return new ResultModel<IEnumerable<BodyDimension>>(null, 404);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<IEnumerable<BodyDimension>>(null, 500);
            }
        }     
    }
}
