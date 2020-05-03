using System.Collections.Generic;
using TrainingFinder.Entities;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public interface IUserRepository
    {
        ResultModel<User> Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        ResultModel<User>  GetById(int id);
        ResultModel<User> Create(User user, string password);
        ResultModel<User> Update(User user);
        ResultModel<User> Update(User user, string password = null);
        ResultModel<User> Delete(int id);
    }
}