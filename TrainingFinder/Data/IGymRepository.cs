using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public interface IGymRepository : IDisposable
    {
        IQueryable<Gym> Gyms { get; }
        bool DeleteGym(int id);
        bool SaveGym(Gym entity);
        ResultModel<Gym> GetById(int id);
        ResultModel<Gym> Create(Gym entiti);
        ResultModel<Gym> Update(Gym entity);
        ResultModel<Gym> Delete(int id);
        ResultModel<IEnumerable<Gym>> GetAllInCity(string city);
    }
}
