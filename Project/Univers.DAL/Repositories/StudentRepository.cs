using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return context.Students.ToList();
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

            return context.Students
                .Include(s => s.Speciality)
                .ThenInclude(sp => sp.Faculties)
                .ThenInclude(f => f.University)
                .Where(s => s.Id == studentId)
                .Select(s => s.Speciality.Faculties.FirstOrDefault().University.Name)
                .FirstOrDefault();
        }
    }
}
