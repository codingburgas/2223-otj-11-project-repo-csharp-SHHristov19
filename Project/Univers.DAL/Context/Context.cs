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

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectComponent> SubjectComponents { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.\\SQLEXPRESS; initial catalog=Univers; Encrypt=False; Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC07EC82379D");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exams__3214EC07BE3A9C13");

            entity.HasOne(d => d.ExamSession).WithMany(p => p.Exams).HasConstraintName("FK__Exams__ExamSessi__5629CD9C");

            entity.HasOne(d => d.Proctor).WithMany(p => p.Exams).HasConstraintName("FK__Exams__ProctorId__5441852A");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams).HasConstraintName("FK__Exams__SubjectId__5535A963");
        });

        modelBuilder.Entity<ExamSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExamSess__3214EC077C383EBC");

            entity.HasOne(d => d.Semester).WithMany(p => p.ExamSessions).HasConstraintName("FK__ExamSessi__Semes__4316F928");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facultie__3214EC0734260DED");

            entity.HasOne(d => d.Dean).WithMany(p => p.FacultyDeans).HasConstraintName("FK__Faculties__DeanI__30F848ED");

            entity.HasOne(d => d.University).WithMany(p => p.Faculties).HasConstraintName("FK__Faculties__Unive__300424B4");

            entity.HasOne(d => d.ViceDean).WithMany(p => p.FacultyViceDeans).HasConstraintName("FK__Faculties__ViceD__31EC6D26");

            entity.HasMany(d => d.Specialities).WithMany(p => p.Faculties)
                .UsingEntity<Dictionary<string, object>>(
                    "FacultySpeciality",
                    r => r.HasOne<Speciality>().WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FacultySp__Speci__35BCFE0A"),
                    l => l.HasOne<Faculty>().WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FacultySp__Facul__34C8D9D1"),
                    j =>
                    {
                        j.HasKey("FacultyId", "SpecialityId").HasName("PK__FacultyS__9611B5071290AEB2");
                        j.ToTable("FacultySpeciality");
                        j.IndexerProperty<string>("FacultyId")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("SpecialityId")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.ExamId }).HasName("PK__Grades__5052798522FA4BD0");

            entity.HasOne(d => d.Exam).WithMany(p => p.Grades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__ExamId__59FA5E80");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__StudentI__59063A47");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Semester__3214EC0741553A4C");

            entity.HasOne(d => d.University).WithMany(p => p.Semesters).HasConstraintName("FK__Semesters__Unive__3C69FB99");

            entity.HasMany(d => d.Subjects).WithMany(p => p.Semesters)
                .UsingEntity<Dictionary<string, object>>(
                    "SubjectSemester",
                    r => r.HasOne<Subject>().WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SubjectSe__Subje__403A8C7D"),
                    l => l.HasOne<Semester>().WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SubjectSe__Semes__3F466844"),
                    j =>
                    {
                        j.HasKey("SemesterId", "SubjectId").HasName("PK__SubjectS__9EF2BBE7FE95897F");
                        j.ToTable("SubjectSemester");
                        j.IndexerProperty<string>("SemesterId")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("SubjectId")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC07F10C4554");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Specialities).HasConstraintName("FK__Specialit__Tutor__2D27B809");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC07FC024550");

            entity.HasOne(d => d.User).WithMany(p => p.Staff).HasConstraintName("FK__Staff__UserId__276EDEB3");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07C80DEA66");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Students).HasConstraintName("FK__Students__Specia__45F365D3");

            entity.HasOne(d => d.User).WithMany(p => p.Students).HasConstraintName("FK__Students__UserId__46E78A0C");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.SemesterId }).HasName("PK__StudentC__F2861B8437FBB853");

            entity.HasOne(d => d.Semester).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Semes__5165187F");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Stude__5070F446");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC076571D5E6");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Subjects).HasConstraintName("FK__Subjects__Specia__398D8EEE");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Subjects).HasConstraintName("FK__Subjects__Teache__38996AB5");
        });

        modelBuilder.Entity<SubjectComponent>(entity =>
        {
            entity.HasKey(e => new { e.InstructorId, e.ComponentId, e.SubjectId }).HasName("PK__SubjectC__C9D4DE3C916FDE7E");

            entity.HasOne(d => d.Component).WithMany(p => p.SubjectComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Compo__4CA06362");

            entity.HasOne(d => d.Instructor).WithMany(p => p.SubjectComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Instr__4BAC3F29");

            entity.HasOne(d => d.Subject).WithMany(p => p.SubjectComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubjectCo__Subje__4D94879B");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3214EC07B3205FE1");

            entity.HasOne(d => d.Rector).WithMany(p => p.Universities).HasConstraintName("FK__Universit__Recto__2A4B4B5E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC077C4B298E");

            entity.Property(e => e.IsActive).HasDefaultValueSql("('TRUE')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
