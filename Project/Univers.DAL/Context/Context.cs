using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Univers.DAL.Entities;

namespace Univers.DAL.Context;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamSession> ExamSessions { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultySpeciality> FacultySpecialities { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.\\SQLEXPRESS; initial catalog=Univers; Encrypt=False; Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC07F319BB63");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exams__3214EC07E810D019");

            entity.HasOne(d => d.ExamSession).WithMany(p => p.Exams).HasConstraintName("FK__Exams__ExamSessi__52593CB8");

            entity.HasOne(d => d.Proctor).WithMany(p => p.Exams).HasConstraintName("FK__Exams__ProctorId__5070F446");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams).HasConstraintName("FK__Exams__SubjectId__5165187F");
        });

        modelBuilder.Entity<ExamSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExamSess__3214EC07E8583AD4");

            entity.HasOne(d => d.Semester).WithMany(p => p.ExamSessions).HasConstraintName("FK__ExamSessi__Semes__412EB0B6");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facultie__3214EC07C5DEE315");

            entity.HasOne(d => d.Dean).WithMany(p => p.FacultyDeans).HasConstraintName("FK__Faculties__DeanI__30F848ED");

            entity.HasOne(d => d.University).WithMany(p => p.Faculties).HasConstraintName("FK__Faculties__Unive__300424B4");

            entity.HasOne(d => d.ViceDean).WithMany(p => p.FacultyViceDeans).HasConstraintName("FK__Faculties__ViceD__31EC6D26");
        });

        modelBuilder.Entity<FacultySpeciality>(entity =>
        {
            entity.HasOne(d => d.Faculty).WithMany().HasConstraintName("FK__FacultySp__Facul__33D4B598");

            entity.HasOne(d => d.Speciality).WithMany().HasConstraintName("FK__FacultySp__Speci__34C8D9D1");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasOne(d => d.Exam).WithMany().HasConstraintName("FK__Grades__ExamId__5535A963");

            entity.HasOne(d => d.Student).WithMany().HasConstraintName("FK__Grades__StudentI__5441852A");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Semester__3214EC07D27CA51C");

            entity.HasOne(d => d.University).WithMany(p => p.Semesters).HasConstraintName("FK__Semesters__Unive__3B75D760");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC0783F5D558");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Specialities).HasConstraintName("FK__Specialit__Tutor__2D27B809");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC077910D2E6");

            entity.HasOne(d => d.User).WithMany(p => p.Staff).HasConstraintName("FK__Staff__UserId__276EDEB3");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07F116E3A7");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Students).HasConstraintName("FK__Students__Specia__440B1D61");

            entity.HasOne(d => d.User).WithMany(p => p.Students).HasConstraintName("FK__Students__UserId__44FF419A");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasOne(d => d.Semester).WithMany().HasConstraintName("FK__StudentCo__Semes__4D94879B");

            entity.HasOne(d => d.Student).WithMany().HasConstraintName("FK__StudentCo__Stude__4CA06362");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC076115C41E");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Subjects).HasConstraintName("FK__Subjects__Specia__38996AB5");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Subjects).HasConstraintName("FK__Subjects__Teache__37A5467C");
        });

        modelBuilder.Entity<SubjectComponent>(entity =>
        {
            entity.HasOne(d => d.Component).WithMany().HasConstraintName("FK__SubjectCo__Compo__49C3F6B7");

            entity.HasOne(d => d.Instructor).WithMany().HasConstraintName("FK__SubjectCo__Instr__48CFD27E");

            entity.HasOne(d => d.Subject).WithMany().HasConstraintName("FK__SubjectCo__Subje__4AB81AF0");
        });

        modelBuilder.Entity<SubjectSemester>(entity =>
        {
            entity.HasOne(d => d.Semester).WithMany().HasConstraintName("FK__SubjectSe__Semes__3D5E1FD2");

            entity.HasOne(d => d.Subject).WithMany().HasConstraintName("FK__SubjectSe__Subje__3E52440B");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3214EC070FBF4676");

            entity.HasOne(d => d.Rector).WithMany(p => p.Universities).HasConstraintName("FK__Universit__Recto__2A4B4B5E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07F51374E4");

            entity.Property(e => e.IsActive).HasDefaultValueSql("('TRUE')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
