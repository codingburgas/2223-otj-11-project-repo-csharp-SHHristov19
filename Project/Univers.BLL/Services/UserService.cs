using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public List<User> TransferDataFromEntityToModel()
        {
            List<User> models = new();

            var entities = _userRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new User();

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

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            List<User> users = TransferDataFromEntityToModel();

            var user = users.Where(x => x.Username == username).FirstOrDefault();

            if(user != null && user.Password == HashPassword(password, user.PasswordSalt))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool ComparePasswords(string firstPass, string secondPass)
        {
            return firstPass.Equals(secondPass);
        }

        public void AddUser(User user)
        {
            user.PasswordSalt = _utilities.GenerateSalt();
            user.Password = HashPassword(user.Password, user.PasswordSalt);
            _userRepository.AddData(user);
        }

        public string HashPassword(string password, string salt)
        {
            var hash = SHA512.Create().ComputeHash(_utilities.HashPasswordWithSalt(password, salt));

            return Convert.ToHexString(hash);
        }
    }
}
