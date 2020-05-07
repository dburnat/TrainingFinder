using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;

namespace TrainingFinder.Data
{
    public interface ITrainingUserRepository
    {
        IQueryable<User> Users { get; }
    }
}
