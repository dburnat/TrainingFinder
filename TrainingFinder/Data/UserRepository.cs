using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TrainingFinder.Entities;
using TrainingFinder.Helpers;
using TrainingFinder.Models;

namespace TrainingFinder.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultModel<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return new ResultModel<User>(null, StatusCodes.Status400BadRequest);

            var user = _dbContext.Users.SingleOrDefault(c => c.Username == username);
            if (user == null)
                return new ResultModel<User>(null, StatusCodes.Status404NotFound);

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            //On successful authentication
            return new ResultModel<User>(user, StatusCodes.Status200OK);
        }

        public ResultModel<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (_dbContext.Users.Any(c => c.Username == user.Username))
            {
                throw new AppException($"{user.Username} is already taken");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return new ResultModel<User>(user, StatusCodes.Status200OK);
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public ResultModel<User> GetById(int id)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(c => c.Id == id);

                if (user == null)
                    return new ResultModel<User>(null, StatusCodes.Status404NotFound);

                return new ResultModel<User>(user, StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<User>(null, StatusCodes.Status500InternalServerError);
            }
        }

        public ResultModel<User> Update(User userToUpdate, string password = null)
        {
            try
            {
                var user = _dbContext.Users.Find(userToUpdate.Id);

                if (user == null)
                    throw new AppException("User not found");

                // Update username if it has changed
                if (!string.IsNullOrWhiteSpace(userToUpdate.Username) && userToUpdate.Username != user.Username)
                {
                    // throw error if the new username is already taken
                    if (_dbContext.Users.Any(x => x.Username == userToUpdate.Username))
                        throw new AppException("Username " + userToUpdate.Username + " is already taken");

                    user.Username = userToUpdate.Username;
                }

                // Update user properties if provided
                if (!string.IsNullOrWhiteSpace(userToUpdate.FirstName))
                    user.FirstName = userToUpdate.FirstName;

                if (!string.IsNullOrWhiteSpace(userToUpdate.LastName))
                    user.LastName = userToUpdate.LastName;

                // pdate password if provided
                if (!string.IsNullOrWhiteSpace(password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return new ResultModel<User>(user, StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<User>(null, StatusCodes.Status500InternalServerError);
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}