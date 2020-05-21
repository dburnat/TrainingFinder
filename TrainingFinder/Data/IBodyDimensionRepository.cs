using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public interface IBodyDimensionRepository
    {
        IQueryable<BodyDimension> BodyDimensions { get; }
        bool DeleteDimensions(int id);
        bool SaveDimensions(BodyDimension entity);
        ResultModel<BodyDimension> Create(BodyDimension entity);
        ResultModel<BodyDimension> Delete(int id);
        ResultModel<BodyDimension> Update(BodyDimension entity);
        ResultModel<IEnumerable<BodyDimension>> GetAllByUser(User user);

    }
}
