using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingFinder.Entities;

namespace TrainingFinder.Data
{
    public class TrainingUserRepository : ITrainingUserRepository
    {
        private readonly ApplicationDbContext _ctx;
        public TrainingUserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<User> Users => _ctx.Users;
    }
}
