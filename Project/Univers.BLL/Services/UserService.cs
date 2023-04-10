using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly Univers.Utilities.Utilities _utilities;

        public UserService()
        {
            _userRepository = new UserRepository();
            _utilities = new Univers.Utilities.Utilities();
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
            var users = TransferDataFromEntityToModel();
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
        public void AddUser(SignUpUserModel user)
        {
            var passwordSalt = _utilities.GenerateSalt();
            user.Password = _utilities.HashPassword(user.Password, passwordSalt);
            _userRepository.AddData(user, passwordSalt);
        }

        /// <summary>
        /// Check if the username is already using 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UsernameAlreadyExist(string username)
        {
            var users = TransferDataFromEntityToModel();
            return users.Where(x => x.Username == username).Any();
        }

        /// <summary>
        /// Check if the email is already using 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailAlreadyExist(string email)
        {
            var users = TransferDataFromEntityToModel();
            return users.Where(x => x.Email == email).Any();
        }

        /// <summary>
        /// Validates a SignUpUserModel object to ensure that the username is not already in use.
        /// </summary>
        /// <param name="user">The SignUpUserModel object to validate.</param>
        /// <returns>A ValidationResult object that contains any validation error.</returns>
        public ValidationResult ValidateUsername(SignUpUserModel user)
        { 
            if (UsernameAlreadyExist(user.Username))
            {
                return new ValidationResult("Потребителското име вече съществува.");
            }  
            return ValidationResult.Success;
        }

        /// <summary>
        /// Validates a SignUpUserModel object to ensure that the email is not already in use.
        /// </summary>
        /// <param name="user">The SignUpUserModel object to validate.</param>
        /// <returns>A ValidationResult object that contains any validation error.</returns>
        public ValidationResult ValidateEmail(SignUpUserModel user)
        {
            if (EmailAlreadyExist(user.Email))
            {
                return new ValidationResult("Email адреса вече съществува.");
            }
            return ValidationResult.Success;
        }

        public UserModel? GetUserByEmail(string email)
        {
            var users = TransferDataFromEntityToModel();
            var user = users.Where(x => x.Email == email).FirstOrDefault();

            if (user != null)
            {
                return user;
            } 
            return null; 
        }

        public string GetUserIdByUsername(string username)
        {
            var users = TransferDataFromEntityToModel();
            return users.FirstOrDefault(x => x.Username == username).Id;
        }

        public void ChangePassword(string userId, string? newPassword)
        {
            var passwordSalt = _utilities.GenerateSalt();
            newPassword = _utilities.HashPassword(newPassword, passwordSalt);
            _userRepository.UpdatePassword(userId, newPassword, passwordSalt);
        }
    }
}