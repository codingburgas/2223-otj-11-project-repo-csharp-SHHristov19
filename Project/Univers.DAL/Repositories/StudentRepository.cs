using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.DAL.Repositories
{
    public class StudentRepository
    {
        /// <summary>
        /// Read the data from the Student table
        /// </summary>
        /// <returns></returns>
        public List<Student> ReadAllData()
        {
            using Context.Context context = new();

            return (from student in context.Students
                   join user in context.Users on student.UserId equals user.Id
                   where user.IsActive == true
                   select student).ToList();
        }

        /// <summary>
        /// Add student in Students table
        /// </summary>
        /// <param name="student"></param>
        public void AddData(StudentModel student)
        {
            using Context.Context context = new();

            Student additionalStudent = new Student()
            {
                Id = student.Id,
                UserId = student.UserId,
                Identity = student.Identity,
                Citizenship = student.Citizenship,
                CountryOfBirth = student.CountryOfBirth,
                AreaOfBirth = student.AreaOfBirth,
                CityOfBirth = student.CityOfBirth,
                MunicipalityOfBirth = student.MunicipalityOfBirth,
                DateOfBirth = student.DateOfBirth,
                FormOfEducation = student.FormOfEducation
            };

            context.Students.Add(additionalStudent);
            context.SaveChanges();
        }

        public void AddSpecialityId(string studentId, string specialityId)
        {
            using Context.Context context = new();

            var student = context.Students.FirstOrDefault(s => s.Id == studentId);
            
            student.SpecialityId = specialityId;
            
            context.Update(student);

            context.SaveChanges();
        }

        public void AddFacultyNumber(string studentId, string facultyNumber)
        {
            using Context.Context context = new();

            var student = context.Students.FirstOrDefault(s => s.Id == studentId);

            student.FacultyNumber = facultyNumber;

            context.Update(student);

            context.SaveChanges();
        }

        public string? GetUniversityNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from s in context.Students
                   join sp in context.Specialities on s.SpecialityId equals sp.Id
                   join f in context.Faculties on sp.Faculties.First().Id equals f.Id
                   join u in context.Universities on f.UniversityId equals u.Id
                   where s.Id == studentId
                   select u.Name).FirstOrDefault();
        }

        public void UpdateStudent(EditStudentModel? newUser)
        {
            using Context.Context context = new();

            var student = context.Students.FirstOrDefault(u => u.Id == newUser.Id);

            student.User = context.Users.FirstOrDefault(u => u.Id == newUser.User.Id);

            student.Id = newUser.Id; 
            student.Identity = newUser.Identity;
            student.Citizenship = newUser.Citizenship;
            student.CountryOfBirth = newUser.CountryOfBirth;
            student.AreaOfBirth = newUser.AreaOfBirth;
            student.CityOfBirth = newUser.CityOfBirth;
            student.MunicipalityOfBirth = newUser.MunicipalityOfBirth;
            student.DateOfBirth = DateTime.ParseExact(newUser.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            student.FormOfEducation = newUser.FormOfEducation;
            student.User.Address = newUser.User.Address;
            student.User.Email = newUser.User.Email;
            student.User.FirstName = newUser.User.FirstName;
            student.User.MiddleName = newUser.User.MiddleName;
            student.User.LastName = newUser.User.LastName;
            student.User.Username = newUser.User.Username;
            student.User.PhoneNumber = newUser.User.PhoneNumber;

            context.Update(student);
            context.SaveChanges();
        } 
    }
}
