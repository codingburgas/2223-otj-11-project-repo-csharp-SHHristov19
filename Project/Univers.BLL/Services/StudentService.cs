using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
using Univers.Models.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Univers.BLL.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;
        private readonly Univers.Utilities.Utilities _utilities;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
            _utilities = new Univers.Utilities.Utilities();
        }

        /// <summary>
        /// Transfer data from Student entity to Student model
        /// </summary>
        /// <returns></returns>
        public List<StudentModel> TransferDataFromEntityToModel()
        {
            List<StudentModel> models = new();

            List<Student> entities = _studentRepository.ReadAllData();

            foreach (var entity in entities)
            {
                StudentModel newModel = MapStudentEntity(entity);

                models.Add(newModel);
            }

            return models;
        }


        /// <summary>
        /// Maps a Student entity to a StudentModel.
        /// </summary>
        /// <param name="entity">The Student entity to be mapped.</param>
        /// <returns>A StudentModel representing the mapped entity.</returns>
        public StudentModel MapStudentEntity(Student entity)
        {
            var newModel = new StudentModel();

            newModel.Id = entity.Id;
            newModel.UserId = entity.UserId;
            newModel.SpecialityId = entity.SpecialityId;
            newModel.Citizenship = entity.Citizenship;
            newModel.Identity = entity.Identity;
            newModel.DateOfStarting = entity.DateOfStarting;
            newModel.DateOfGraduate = entity.DateOfGraduate;
            newModel.MunicipalityOfBirth = entity.MunicipalityOfBirth;
            newModel.AreaOfBirth = entity.AreaOfBirth;
            newModel.DateOfBirth = entity.DateOfBirth;
            newModel.CountryOfBirth = entity.CountryOfBirth;
            newModel.CityOfBirth = entity.CityOfBirth;
            newModel.FacultyNumber = entity.FacultyNumber;
            newModel.FormOfEducation = entity.FormOfEducation;
            return newModel;
        }

        /// <summary>
        /// Adds a new student using the provided StudentModel.
        /// </summary>
        /// <param name="student">The StudentModel containing the details of the student to be added.</param>
        public void AddStudent(StudentModel student)
        {
            _studentRepository.AddData(student);
        }

        /// <summary>
        /// Adds a new student using the provided AddStudentModel.
        /// </summary>
        /// <param name="student">The AddStudentModel containing the details of the student to be added.</param>
        public void AddStudent(AddStudentModel student)
        {
            _studentRepository.AddData(student);
        }

        /// <summary>
        /// Adds a speciality ID to a student's record.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="specialityId">The ID of the speciality to add.</param>
        public void AddSpecialityId(string studentId, string specialityId)
        {
            _studentRepository.AddSpecialityId(studentId, specialityId);
        }

        /// <summary>
        /// Retrieves a student model by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve the student for.</param>
        /// <returns>A <see cref="StudentModel"/> representing the student, or null if not found.</returns>
        public StudentModel? GetStudentByUserId(string userId)
        {
            List<StudentModel> students = TransferDataFromEntityToModel();

            return students.FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>
        /// Retrieves the maximum number of faculty numbers among all students.
        /// </summary>
        /// <returns>The maximum number of faculty numbers.</returns>
        public int GetTheMaxNumOfTheFacultyNumbersOfAllStudents()
        {
            var facultyNums = TransferDataFromEntityToModel().Select(x => x.FacultyNumber);

            List<int> numbers = new();

            foreach (var num in facultyNums)
            {
                if(num != null)
                {
                    numbers.Add(int.Parse(num.Split("-")[3]));
                } 
            }

            return numbers.Max();
        }

        /// <summary>
        /// Adds a faculty number to a student's record based on the provided faculty code and speciality code.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="facultyCode">The code representing the faculty.</param>
        /// <param name="specialityCode">The code representing the speciality.</param>
        public void AddFacultyNumber(string studentId, string facultyCode, string specialityCode)
        {
            int facultyNum = GetTheMaxNumOfTheFacultyNumbersOfAllStudents() + 1;

            string facultyNumber = $"{facultyCode}-{specialityCode.Substring(0, specialityCode.Length - 1)}-{specialityCode[specialityCode.Length - 1]}-{facultyNum:D4}";

            _studentRepository.AddFacultyNumber(studentId, facultyNumber);
        }

        /// <summary>
        /// Retrieves a student model by their ID.
        /// </summary>
        /// <param name="studentId">The ID of the student to retrieve.</param>
        /// <returns>A <see cref="StudentModel"/> representing the student, or null if not found.</returns>
        public StudentModel? GetStudentById(string studentId)
        {  
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == studentId);
        }

        /// <summary>
        /// Retrieves the university name associated with the given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The university name associated with the student ID, or null if not found.</returns>
        public string? GetUniversityNameByStudentId(string studentId)
        {
            return _studentRepository.GetUniversityNameByStudentId(studentId);
        }

        /// <summary>
        /// Updates a student's information based on the provided new user details.
        /// </summary>
        /// <param name="newUser">The <see cref="EditStudentModel"/> object containing the updated student details.</param>
        public void UpdaateStudent(EditStudentModel? newUser)
        {
            _studentRepository.UpdateStudent(newUser);
        }
    }
}
