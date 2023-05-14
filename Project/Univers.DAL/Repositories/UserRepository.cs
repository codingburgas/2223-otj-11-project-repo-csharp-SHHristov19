using Microsoft.EntityFrameworkCore;
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
    public class UserRepository 
    {   
        /// <summary>
        /// Read the data from the Users table
        /// </summary>
        /// <returns></returns>
        public List<User> ReadAllData()
        {
            using Context.Context context = new();

            return context.Users.ToList();
        }

        /// <summary>
        /// Add user in Users table
        /// </summary>
        /// <param name="user"></param>
        public void AddData(SignUpUserModel user, string passwordSalt)
        {
            using Context.Context context = new();

            User additionalUser = new User()
            {
                Id = Guid.NewGuid().ToString("D"),
                Username = user.Username,
                Password = user.Password,
                PasswordSalt = passwordSalt,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                DateOfRegistration = DateTime.Now,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                Gender = user.Gender,
                Image = user.Image,
                IsActive = true,
            };

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

        public IEnumerable<User> GetStaffUsers()
        {
            using Context.Context context = new();

            return (from u in context.Users
                    join s in context.Students on u.Id equals s.UserId into studentGroup
                    from sg in studentGroup.DefaultIfEmpty()
                    where sg == null
                    orderby u.FirstName, u.MiddleName, u.LastName
                    select u).ToList();
        }

        public IEnumerable<User> GetStudentUsers()
        {
            using Context.Context context = new();

            return (from u in context.Users
                    join s in context.Students on u.Id equals s.UserId into studentGroup
                    from sg in studentGroup.DefaultIfEmpty()
                    where sg != null
                    orderby u.FirstName, u.MiddleName, u.LastName
                    select u).ToList();
        }
    }
}
