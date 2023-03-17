using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models.Data;

public partial class UniversContext : DbContext
{
    public UniversContext()
    {
    }

    public UniversContext(DbContextOptions<UniversContext> options)
        : base(options)
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.\\SQLEXPRESS; initial catalog=Univers; Encrypt=False; Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC07C38577E9");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(60);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exams__3214EC079FF97746");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExamSessionId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.ProctorId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ExamSession).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ExamSessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__ExamSessi__5165187F");

            entity.HasOne(d => d.Proctor).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ProctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__ProctorId__4F7CD00D");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__SubjectId__5070F446");
        });

        modelBuilder.Entity<ExamSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExamSess__3214EC0721CD366A");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfEnd).HasColumnType("date");
            entity.Property(e => e.DateOfStart).HasColumnType("date");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facultie__3214EC07FBB7AC6F");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.DeanId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UniversityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ViceDeanId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Dean).WithMany(p => p.FacultyDeans)
                .HasForeignKey(d => d.DeanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Faculties__DeanI__30F848ED");

            entity.HasOne(d => d.University).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Faculties__Unive__300424B4");

            entity.HasOne(d => d.ViceDean).WithMany(p => p.FacultyViceDeans)
                .HasForeignKey(d => d.ViceDeanId)
                .HasConstraintName("FK__Faculties__ViceD__31EC6D26");
        });

        modelBuilder.Entity<FacultySpeciality>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FacultySpeciality");

            entity.Property(e => e.FacultyId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpecialityId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany()
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacultySp__Facul__33D4B598");

            entity.HasOne(d => d.Speciality).WithMany()
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FacultySp__Speci__34C8D9D1");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ExamId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Grade1).HasColumnName("Grade");
            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Exam).WithMany()
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__ExamId__5441852A");

            entity.HasOne(d => d.Student).WithMany()
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__StudentI__534D60F1");
        });

        modelBuilder.Entity<Holidai>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DateOfEnd).HasColumnType("date");
            entity.Property(e => e.DateOfStart).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Semester__3214EC07A1DD55DF");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfEnd).HasColumnType("date");
            entity.Property(e => e.DateOfStart).HasColumnType("date");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UniversityId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.University).WithMany(p => p.Semesters)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Semesters__Unive__3B75D760");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC072A20C371");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Degree).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.TutorId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Tutor).WithMany(p => p.Specialities)
                .HasForeignKey(d => d.TutorId)
                .HasConstraintName("FK__Specialit__Tutor__2D27B809");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC07062B9008");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__UserId__276EDEB3");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0742E84E15");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AreaOfBirth).HasMaxLength(50);
            entity.Property(e => e.Citizenship).HasMaxLength(60);
            entity.Property(e => e.CityOfBirth).HasMaxLength(50);
            entity.Property(e => e.CountryOfBirth).HasMaxLength(50);
            entity.Property(e => e.DacultyNumber).HasMaxLength(20);
            entity.Property(e => e.FacultyNumber).HasMaxLength(50);
            entity.Property(e => e.FormOfEducation).HasMaxLength(50);
            entity.Property(e => e.Identity)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MunicipalityOfBirth).HasMaxLength(50);
            entity.Property(e => e.SpecialityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Speciality).WithMany(p => p.Students)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__Specia__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__UserId__440B1D61");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StudentCourse");

            entity.Property(e => e.SemesterId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Semester).WithMany()
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Semes__4CA06362");

            entity.HasOne(d => d.Student).WithMany()
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Stude__4BAC3F29");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC074BE2039B");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Credits).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.SpecialityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeacherId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.Speciality).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__Specia__38996AB5");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subjects__Teache__37A5467C");
        });

        modelBuilder.Entity<SubjectComponent>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ComponentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InstructorId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Component).WithMany()
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Compo__48CFD27E");

            entity.HasOne(d => d.Instructor).WithMany()
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Instr__47DBAE45");

            entity.HasOne(d => d.Subject).WithMany()
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Subje__49C3F6B7");
        });

        modelBuilder.Entity<SubjectSemester>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SubjectSemester");

            entity.Property(e => e.SemesterId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Semester).WithMany()
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectSe__Semes__3D5E1FD2");

            entity.HasOne(d => d.Subject).WithMany()
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectSe__Subje__3E52440B");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3214EC077A9BD089");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RectorId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Rector).WithMany(p => p.Universities)
                .HasForeignKey(d => d.RectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Universit__Recto__2A4B4B5E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E7F14038");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("('TRUE')");
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.MiddleName).HasMaxLength(30);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
