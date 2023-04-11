using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
    }
}
