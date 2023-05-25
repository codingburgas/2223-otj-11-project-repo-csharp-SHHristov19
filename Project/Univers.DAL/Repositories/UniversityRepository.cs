using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.DAL.Repositories
{
    public class UniversityRepository
    {
        // Read the data from the Universities table
        public List<University> ReadAllData()
        {
            using Context.Context context = new();

            return context.Universities.ToList();
        }

        public string? GetRectorByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from s in context.Students
                    join sp in context.Specialities on s.SpecialityId equals sp.Id
                    join f in context.Faculties on sp.Faculties.First().Id equals f.Id
                    join un in context.Universities on f.UniversityId equals un.Id
                    join sf in context.Staff on un.RectorId equals sf.Id
                    join u in context.Users on sf.UserId equals u.Id
                    where s.Id == studentId
                    select new
                    {
                        Name = $"{u.FirstName} {u.MiddleName} {u.LastName}"
                    }).First().Name;

        }

        public string? GetUniversityNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from s in context.Students
                    join sp in context.Specialities on s.SpecialityId equals sp.Id
                    join f in context.Faculties on sp.Faculties.First().Id equals f.Id
                    join un in context.Universities on f.UniversityId equals un.Id
                    where s.Id == studentId
                    select un.Name).FirstOrDefault();
        }

        public string? GetUniversityAddressByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from s in context.Students
                    join sp in context.Specialities on s.SpecialityId equals sp.Id
                    join f in context.Faculties on sp.Faculties.First().Id equals f.Id
                    join un in context.Universities on f.UniversityId equals un.Id
                    where s.Id == studentId
                    select un.Address).FirstOrDefault();
        }

        public string? GetRectorNameByUniversityId(string universityId)
        {
            using Context.Context context = new();

            return (from u in context.Universities
                    join sf in context.Staff on u.RectorId equals sf.Id
                   join us in context.Users on sf.UserId equals us.Id
                   select new
                   {
                       Name = $"{us.FirstName} {us.MiddleName} {us.LastName}"
                   }).First().Name;
        }

        public void AddUniversity(UniversityModel addUniversity)
        {
            using Context.Context context = new();

            University university = new()
            {
                Id = addUniversity.Id,
                RectorId = addUniversity.RectorId,
                Capacity = addUniversity.Capacity,
                Name = addUniversity.Name,
                Address = addUniversity.Address,
            };

            context.Universities.Add(university);
            context.SaveChanges();
        }

        public void DeleteUniversity(string universityId)
        {
            using Context.Context context = new();

            var university = context.Universities
                           .Include(s => s.Semesters)
                           .Include(f => f.Faculties) 
                           .ThenInclude(s => s.Specialities) 
                           .FirstOrDefault(u => u.Id == universityId);


            foreach (var faculties in university.Faculties.ToList())
            {
                faculties.Specialities.Clear();
            }

            university.Faculties.Clear();

            university.Semesters.Clear();
             
            context.Universities.Remove(university);
            context.SaveChanges();
        }
    }
}
