using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UniversUtilities.Utilities _utilities;

        public UserService()
        {
            _userRepository = new UserRepository();
            _utilities = new UniversUtilities.Utilities();
        }

        /// <summary>
        /// Transfer data from User entity to User model
        /// </summary>
        /// <returns></returns>
        public List<UserModel> TransferDataFromEntityToModel()
        {
            List<UserModel> models = new();

            List<User> entities = _userRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new UserModel();

                newModel.Id = entity.Id;
                newModel.Username = entity.Username;
                newModel.PasswordSalt = entity.PasswordSalt;
                newModel.Password = entity.Password;
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

        /// <summary>
        /// Get user by username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserModel GetUserByUsernameAndPassword(string username, string password)
        {
            List<UserModel> users = TransferDataFromEntityToModel();

            var user = users.Where(x => x.Username == username).FirstOrDefault();

            if (user != null && user.Password == _utilities.HashPassword(password, user.PasswordSalt))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Compare the password and the repeated password 
        /// </summary>
        /// <param name="firstPass"></param>
        /// <param name="secondPass"></param>
        /// <returns></returns>
        public bool ComparePasswords(string firstPass, string secondPass)
        {
            return firstPass.Equals(secondPass);
        }

        /// <summary>
        /// Add a user
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(UserModel user)
        {
            user.PasswordSalt = _utilities.GenerateSalt();
            user.Password = _utilities.HashPassword(user.Password, user.PasswordSalt);
            _userRepository.AddData(user);
        }
    }
}