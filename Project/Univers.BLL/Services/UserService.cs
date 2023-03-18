using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class UserService
    {
        // Transfer data from User entity to User model
        public static List<User> TransferDataFromEntityToModel()
        {
            List<User> users = new();

            var dataUsers = UserRepository.ReadAllDataFromUser();

            foreach (var userEntity in dataUsers)
            {
                var userModel = new User();

                userModel.Id = userEntity.Id;
                userModel.Username = userEntity.Username;
                userModel.Password = userEntity.Password;
                userModel.PasswordSalt = userEntity.PasswordSalt;
                userModel.FirstName = userEntity.FirstName;
                userModel.MiddleName = userEntity.MiddleName;
                userModel.LastName = userEntity.LastName;
                userModel.DateOfRegistration = userEntity.DateOfRegistration;
                userModel.PhoneNumber = userEntity.PhoneNumber;
                userModel.Email = userEntity.Email;
                userModel.Address = userEntity.Address;
                userModel.Gender = userEntity.Gender;
                userModel.Image = userEntity.Image;
                userModel.IsActive = userEntity.IsActive;

                users.Add(userModel);
            }

            return users;
        }
    }
}
