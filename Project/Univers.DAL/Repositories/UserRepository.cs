using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.DAL.Repositories
{
    public class UserRepository : IDisposable
    {
        private Context.Context _dbContext;

        public UserRepository()
        {
            _dbContext = new();
        }

        /// <summary>
        /// Read the data from the Users table
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> ReadAllData()
        { 
            return _dbContext.Users;
        } 

        /// <summary>
        /// Add user in Users table
        /// </summary>
        /// <param name="user"></param>
        public void AddData(SignUpUserModel user, string passwordSalt)
        {
            using Context.Context context = new();
            User additionalUser = new User();

            additionalUser.Id = Guid.NewGuid().ToString("D");
            additionalUser.Username = user.Username;
            additionalUser.Password = user.Password;
            additionalUser.PasswordSalt = passwordSalt;
            additionalUser.FirstName = user.FirstName;
            additionalUser.MiddleName = user.MiddleName;
            additionalUser.LastName = user.LastName;
            additionalUser.DateOfRegistration = DateTime.Now;
            additionalUser.PhoneNumber = user.PhoneNumber;
            additionalUser.Email = user.Email;
            additionalUser.Address = user.Address;
            additionalUser.Gender = user.Gender;
            additionalUser.Image = user.Image;
            additionalUser.IsActive = true;

            context.Users.Add(additionalUser);
            context.SaveChanges();
        }

        /// <summary>
        /// Update the password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <param name="salt"></param>
        public void UpdatePassword(string userId, string newPassword, string salt)
        {
            using Context.Context context = new();

            var user = context.Users.FirstOrDefault(x => x.Id == userId);

            user.Password = newPassword;
            user.PasswordSalt = salt;

            context.Update(user);
            context.SaveChanges();
        }

        public User? GetUserByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from user in context.Users
                   join student in context.Students on user.Id equals student.UserId
                   where student.Id == studentId
                   select user).FirstOrDefault();
                   
        }

        public void UpdateUsername(string? userId, string? newUsername)
        {
            using Context.Context context = new();

            var user = context.Users.FirstOrDefault(x => x.Id == userId);

            user.Username = newUsername;

            context.Update(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
