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
            List<User> models = new();

            var entities = UserRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new User();

                newModel.Id = entity.Id;
                newModel.Username = entity.Username;
                newModel.Password = entity.Password;
                newModel.PasswordSalt = entity.PasswordSalt;
                newModel.FirstName = entity.FirstName;
                newModel.MiddleName = entity.MiddleName;
                newModel.LastName = entity.LastName;
                newModel.DateOfRegistration = entity.DateOfRegistration;
                newModel.PhoneNumber = entity.PhoneNumber;
                newModel.Email = entity.Email;
                newModel.Address = entity.Address;
                newModel.Gender = entity.Gender;
                newModel.Image = entity.Image;
                newModel.IsActive = entity.IsActive;

                models.Add(newModel);
            }

            return models;
        }
    }
}
