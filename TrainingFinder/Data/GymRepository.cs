using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ResultModel<Gym> Create(Gym entiti)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Gym> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGym(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ResultModel<IEnumerable<Gym>> GetAllInCity(string city)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Gym> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveGym(Gym entity)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Gym> Update(Gym entity)
        {
            throw new NotImplementedException();
        }
    }
}
