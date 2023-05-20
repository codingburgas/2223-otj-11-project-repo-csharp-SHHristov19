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
                
                models.Add(newModel);
            }

            return models;
        }

        public void AddStudent(StudentModel student)
        {
            _studentRepository.AddData(student);
        }

        public void AddSpecialityId(string studentId, string specialityId)
        {
            _studentRepository.AddSpecialityId(studentId, specialityId);
        }

        public StudentModel? GetStudentByUserId(string userId)
        {
            List<StudentModel> students = TransferDataFromEntityToModel();

            return students.FirstOrDefault(x => x.UserId == userId);
        }

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

        public void AddFacultyNumber(string studentId, string facultyCode, string specialityCode)
        {
            int facultyNum = GetTheMaxNumOfTheFacultyNumbersOfAllStudents() + 1;

            string facultyNumber = $"{facultyCode}-{specialityCode.Substring(0, specialityCode.Length - 1)}-{specialityCode.Substring(specialityCode.Length - 1, 1)}-{facultyNum:D4}";

            _studentRepository.AddFacultyNumber(studentId, facultyNumber);
        }

        public StudentModel? GetStudentById(string studentId)
        {  
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == studentId);
        } 

        public string? GetUniversityNameByStudentId(string studentId)
        {
            return _studentRepository.GetUniversityNameByStudentId(studentId);
        }

        public void UpdaateStudent(EditStudentModel? newUser)
        {
            _studentRepository.UpdateStudent(newUser);
        }
    }
}
