using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public partial class Context : DbContext
    {
        public virtual DbSet<Component> Components { get; set; }

        public virtual DbSet<Exam> Exams { get; set; }

        public virtual DbSet<ExamSession> ExamSessions { get; set; }

        public virtual DbSet<Faculty> Faculties { get; set; }

        public virtual DbSet<FacultySpeciality> FacultySpecialities { get; set; }

        public virtual DbSet<Grade> Grades { get; set; }

        public virtual DbSet<Holidai> Holidais { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Speciality> Specialities { get; set; }

        public virtual DbSet<Staff> Staff { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<SubjectComponent> SubjectComponents { get; set; }

        public virtual DbSet<SubjectSemester> SubjectSemesters { get; set; }

        public virtual DbSet<University> Universities { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}