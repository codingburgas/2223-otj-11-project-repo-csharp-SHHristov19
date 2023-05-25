using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

            return context.Users.Where(x => x.IsActive == true).ToList();
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
                   select user)
                   .FirstOrDefault(); 
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
                    select u)
                    .ToList();
        }

        public IEnumerable<User> GetStudentUsers()
        {
            using Context.Context context = new();

            return (from u in context.Users
                    join s in context.Students on u.Id equals s.UserId into studentGroup
                    from sg in studentGroup.DefaultIfEmpty()
                    where sg != null && u.IsActive == true
                    orderby u.FirstName, u.MiddleName, u.LastName
                    select u)
                    .ToList();
        }

        public Dictionary<string, string> GetRoleOfTheUsers ()
        {
            using Context.Context context = new();

            return (from u in context.Users
                   join sf in context.Staff on u.Id equals sf.UserId
                   select new { Id = u.Id, Role = sf.Role })
                   .ToDictionary(item => item.Id, item => item.Role); 
        }

        public void UpdateUser(EditUserModel newUser)
        {
            using Context.Context context = new();

            var user = context.Users.FirstOrDefault(u => u.Id == newUser.Id);

            user.Address = newUser.Address;
            user.Email = newUser.Email;
            user.FirstName = newUser.FirstName;
            user.MiddleName = newUser.MiddleName;
            user.LastName = newUser.LastName;
            user.Username = newUser.Username;
            user.PhoneNumber = newUser.PhoneNumber;

            context.Update(user); 
            context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            using Context.Context context = new();

            var user = context.Users.FirstOrDefault(u => u.Id == id);

            user.IsActive = false;

            context.Update(user);
            context.SaveChanges();
        }

        public void AddUser(AddUserModel user, string passwordSalt)
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
                Image = null,
                IsActive = true,
            };

            context.Users.Add(additionalUser);
            context.SaveChanges();
        }


        public List<User> GetFreeRectors()
        {
            using Context.Context context = new();

            return (from sf in context.Staff
                   join s in context.Users on sf.UserId equals s.Id
                   join u in context.Universities on sf.Id equals u.RectorId into universityGroup
                   from u in universityGroup.DefaultIfEmpty()
                   where sf.Role == "Ректор" && u.RectorId == null && s.IsActive == true
                   select s)
                   .ToList();
        }

        public List<User> GetFreeDeans()
        {
            using Context.Context context = new();

            return (from sf in context.Staff
                   join user in context.Users on sf.UserId equals user.Id
                   join f in context.Faculties on sf.Id equals f.DeanId into facultyGroup
                   from f in facultyGroup.DefaultIfEmpty()
                   where sf.Role == "Декан" && f.DeanId == null && user.IsActive == true
                   select user)
                   .ToList();
        }

        public List<User> GetFreeTutors()
        {
            using Context.Context context = new();

            return (from sf in context.Staff
                    join u in context.Users on sf.UserId equals u.Id
                    join sp in context.Specialities on sf.Id equals sp.TutorId into specialityGroup
                    from sp in specialityGroup.DefaultIfEmpty()
                    where sf.Role == "Тютор" && sp.TutorId == null && u.IsActive == true
                    select u)
                    .ToList();
        }
    }
}
