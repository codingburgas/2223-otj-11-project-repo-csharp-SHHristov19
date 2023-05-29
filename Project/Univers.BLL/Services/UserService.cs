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
        public IEnumerable<UserModel> TransferDataFromEntityToModel()
        {
            var entities = _userRepository.ReadAllData();

            foreach (var entity in entities)
            {
                UserModel newModel = MapUserEntity(entity);

                yield return newModel; 
            } 
        }

        /// <summary>
        /// Maps a User entity to a UserModel object.
        /// </summary>
        /// <param name="entity">The User entity to be mapped.</param>
        /// <returns>A UserModel object representing the mapped user.</returns>
        public UserModel MapUserEntity(User entity)
        {
            var newModel = new UserModel()
            {
                Id = entity.Id,
                Username = entity.Username,
                PasswordSalt = entity.PasswordSalt,
                Password = entity.Password,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                DateOfRegistration = entity.DateOfRegistration,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Address = entity.Address,
                Gender = entity.Gender,
                Image = entity.Image,
                FullName = entity.FirstName + " " + entity.MiddleName + " " + entity.LastName,
                IsConfirmed = entity.IsConfirmed,
            };

            return newModel;
        }

        /// <summary>
        /// Get user by username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserModel? GetUserByUsernameAndPassword(string username, string password)
        { 
            var user = TransferDataFromEntityToModel().FirstOrDefault(x => x.Username == username); 
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
        public ValidationResult ValidateUsername(string username)
        { 
            if (UsernameAlreadyExist(username))
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
        public ValidationResult ValidateEmail(string email)
        {
            if (EmailAlreadyExist(email))
            {
                return new ValidationResult("Email адреса вече съществува.");
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Retrieves a UserModel object by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A UserModel object representing the user, or null if not found.</returns>
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

        /// <summary>
        /// Retrieves the user ID associated with a given username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user ID associated with the username.</returns>
        public string GetUserIdByUsername(string username)
        {
            var users = TransferDataFromEntityToModel();
            return users.FirstOrDefault(x => x.Username == username).Id;
        }

        /// <summary>
        /// Changes the password of a user identified by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="newPassword">The new password to set for the user (optional).</param>
        public void ChangePassword(string userId, string? newPassword)
        {
            var passwordSalt = _utilities.GenerateSalt();
            newPassword = _utilities.HashPassword(newPassword, passwordSalt);
            _userRepository.UpdatePassword(userId, newPassword, passwordSalt);
        }

        /// <summary>
        /// Retrieves a UserModel object associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The student ID associated with the user.</param>
        /// <returns>A UserModel object representing the user, or null if not found.</returns>
        public UserModel? GetUserByStudentId(string studentId)
        {
            var user = _userRepository.GetUserByStudentId(studentId);
            return MapUserEntity(user); 
        }

        /// <summary>
        /// Compares the provided password with the password associated with a given user ID.
        /// </summary>
        /// <param name="studentId">The ID of the student or user.</param>
        /// <param name="password">The password to compare.</param>
        /// <returns>True if the provided password matches the user's password, false otherwise.</returns>
        public bool ComparePasswordsByUserId(string? studentId, string? password)
        {
            var user = GetUserByStudentId(studentId);
            password = _utilities.HashPassword(password, user.PasswordSalt);
            return user.Password == password;
        }

        /// <summary>
        /// Compares the provided username with the username associated with a given user ID.
        /// </summary>
        /// <param name="studentId">The ID of the student or user.</param>
        /// <param name="username">The username to compare.</param>
        /// <returns>True if the provided username matches the user's username, false otherwise.</returns>
        public bool CompareUsernames(string? studentId, string? username)
        {
            var user = GetUserByStudentId(studentId);
            return user.Username == username;
        }

        /// <summary>
        /// Changes the username of a user identified by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="newUsername">The new username to set for the user (optional).</param>
        public void ChangeUsername(string? userId, string? newUsername)
        {
            _userRepository.UpdateUsername(userId, newUsername);
        }

        /// <summary>
        /// Retrieves a UserModel object by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A UserModel object representing the user, or null if not found.</returns>
        public UserModel? GetUserByUserId(string userId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == userId);
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing student users.
        /// </summary>
        /// <returns>A list of UserModel objects representing student users.</returns>
        public List<UserModel> GetStaffUsers()
        {
            var entities = _userRepository.GetStaffUsers();

            List<UserModel> models = new();

            foreach (var entity in entities)
            {
                if(entity.IsActive == true)
                {
                    models.Add(MapUserEntity(entity));
                } 
            }

            return models;
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing student users.
        /// </summary>
        /// <returns>A list of UserModel objects representing student users.</returns>
        public List<UserModel> GetStudentUsers()
        {
            var entities = _userRepository.GetStudentUsers();

            List<UserModel> models = new();

            foreach (var entity in entities)
            {
                models.Add(MapUserEntity(entity));
            }

            return models;
        }

        /// <summary>
        /// Fills the roles of a user based on the provided UserModel.
        /// </summary>
        /// <param name="user">The UserModel object representing the user.</param>
        public void FillRolesOfTheUsers (UserModel user)
        {
            Dictionary<string, string> roles = _userRepository.GetRoleOfTheUsers();

            if (roles.ContainsKey(user.Id))
            {
                user.Role = roles[user.Id];
            }  
            else
            {
                user.Role = "Липсва";
            } 
        }

        /// <summary>
        /// Updates a user with the provided details in the EditUserModel.
        /// </summary>
        /// <param name="newUser">The EditUserModel object containing the updated user details.</param>
        public void UpdateUser(EditUserModel? newUser)
        {
            _userRepository.UpdateUser(newUser);
        }

        /// <summary>
        /// Deletes a user with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        public void DeleteUser(string userId)
        {
            _userRepository.DeleteUser(userId);
        }

        /// <summary>
        /// Adds a new user from the admin panel with the provided user details.
        /// </summary>
        /// <param name="user">The AddUserModel object containing the details of the new user.</param>
        public void AddNewUserFromAdminPanel(AddUserModel user)
        {
            var passwordSalt = _utilities.GenerateSalt();
            user.Password = _utilities.HashPassword(user.Password, passwordSalt);
            _userRepository.AddUser(user, passwordSalt);
        }

        /// <summary>
        /// Retrieves a UserModel object associated with a given staff ID.
        /// </summary>
        /// <param name="userId">The staff ID associated with the user.</param>
        /// <returns>A UserModel object representing the user, or null if not found.</returns>
        public UserModel? GetUserByStaffId(string? userId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == userId);
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing rectors who are not associated with any university.
        /// </summary>
        /// <returns>A list of UserModel objects representing rectors with no university.</returns>
        public List<UserModel> GetRectorsWithNoUniversity()
        {
            var rectors = _userRepository.GetFreeRectors(); 

            var result = new List<UserModel>();

            foreach (var rector in rectors)
            {
                UserModel newModel = MapUserEntity(rector);

                result.Add(newModel);
            } 

            return result;
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing deans who are not associated with any faculty.
        /// </summary>
        /// <returns>A list of UserModel objects representing deans with no faculty.</returns>
        public List<UserModel> GetDeansWithNoFaculty()
        {
            var deans = _userRepository.GetFreeDeans(); 

            var result = new List<UserModel>();

            foreach (var dean in deans)
            {
                UserModel newModel = MapUserEntity(dean);

                result.Add(newModel);
            } 

            return result;
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing tutors who are not associated with any speciality.
        /// </summary>
        /// <returns>A list of UserModel objects representing tutors with no speciality.</returns>
        public List<UserModel> GetTutorsWithNoSpeciality()
        {
            var tutors = _userRepository.GetFreeTutors();

            var result = new List<UserModel>();

            foreach (var tutor in tutors)
            {
                UserModel newModel = MapUserEntity(tutor);

                result.Add(newModel);
            }

            return result;
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing unconfirmed users.
        /// </summary>
        /// <returns>A list of UserModel objects representing unconfirmed users.</returns>
        public List<UserModel> GetUncomfirmedUsers()
        {
            var users = _userRepository.GetAllUsersThatAreNotConfirmed();

            var result = new List<UserModel>();

            foreach (var user in users)
            {
                UserModel newModel = MapUserEntity(user);

                result.Add(newModel);
            }

            return result;
        }

        /// <summary>
        /// Confirms a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be confirmed.</param>
        public void ConfirmUser(string id)
        {
            _userRepository.ConfirmUser(id);
        }

        /// <summary>
        /// Retrieves a list of UserModel objects representing all free teachers.
        /// </summary>
        /// <returns>A list of UserModel objects representing all free teachers.</returns>
        public List<UserModel> GetAllFreeTeachers()
        {
            var teachers = _userRepository.GetAllFreeTeachers();

            var result = new List<UserModel>();

            foreach (var teacher in teachers)
            {
                UserModel newModel = MapUserEntity(teacher);

                result.Add(newModel);
            }

            return result;
        }
    }
}