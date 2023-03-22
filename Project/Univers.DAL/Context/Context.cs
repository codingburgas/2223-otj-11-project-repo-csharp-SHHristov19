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
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC07C38577E9");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exams__3214EC079FF97746");

            entity.HasOne(d => d.ExamSession).WithMany(p => p.Exams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__ExamSessi__5165187F");

            entity.HasOne(d => d.Proctor).WithMany(p => p.Exams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__ProctorId__4F7CD00D");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__SubjectId__5070F446");
        });

        modelBuilder.Entity<ExamSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExamSess__3214EC0721CD366A");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facultie__3214EC07FBB7AC6F");

            entity.HasOne(d => d.Dean).WithMany(p => p.FacultyDeans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Faculties__DeanI__30F848ED");

            entity.HasOne(d => d.University).WithMany(p => p.Faculties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Faculties__Unive__300424B4");

            entity.HasOne(d => d.ViceDean).WithMany(p => p.FacultyViceDeans).HasConstraintName("FK__Faculties__ViceD__31EC6D26");
        });

        modelBuilder.Entity<FacultySpeciality>(entity =>
        {
            entity.HasOne(d => d.Faculty).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacultySp__Facul__33D4B598");

            entity.HasOne(d => d.Speciality).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacultySp__Speci__34C8D9D1");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasOne(d => d.Exam).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__ExamId__5441852A");

            entity.HasOne(d => d.Student).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__StudentI__534D60F1");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Semester__3214EC07A1DD55DF");

            entity.HasOne(d => d.University).WithMany(p => p.Semesters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Semesters__Unive__3B75D760");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC072A20C371");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Specialities).HasConstraintName("FK__Specialit__Tutor__2D27B809");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC07062B9008");

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__UserId__276EDEB3");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0742E84E15");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__Specia__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__UserId__440B1D61");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasOne(d => d.Semester).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Semes__4CA06362");

            entity.HasOne(d => d.Student).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Stude__4BAC3F29");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC074BE2039B");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Subjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__Specia__38996AB5");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Subjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__Teache__37A5467C");
        });

        modelBuilder.Entity<SubjectComponent>(entity =>
        {
            entity.HasOne(d => d.Component).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Compo__48CFD27E");

            entity.HasOne(d => d.Instructor).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Instr__47DBAE45");

            entity.HasOne(d => d.Subject).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Subje__49C3F6B7");
        });

        modelBuilder.Entity<SubjectSemester>(entity =>
        {
            entity.HasOne(d => d.Semester).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectSe__Semes__3D5E1FD2");

            entity.HasOne(d => d.Subject).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectSe__Subje__3E52440B");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3214EC077A9BD089");

            entity.HasOne(d => d.Rector).WithMany(p => p.Universities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Universit__Recto__2A4B4B5E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E7F14038");

            entity.Property(e => e.IsActive).HasDefaultValueSql("('TRUE')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
