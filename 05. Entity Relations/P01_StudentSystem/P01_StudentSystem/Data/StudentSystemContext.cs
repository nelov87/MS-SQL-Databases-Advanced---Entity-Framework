using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public StudentSystemContext()
        {

        }
        

        public StudentSystemContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbOpt)
        {
            if (!dbOpt.IsConfigured)
            {
                dbOpt.UseSqlServer("Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=SalesDb;Integrated Security=true");

            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateEntityStudent(modelBuilder);
            CreateEntityCourse(modelBuilder);
            CreateEntityResource(modelBuilder);
            CreateEntityHomework(modelBuilder);
            CreateEntityStudentCourse(modelBuilder);
        }

        private void CreateEntityStudentCourse(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder
                .Entity<StudentCourse>()
                .HasOne(s => s.Student)
                .WithMany(c => c.CourseEnrollments);

            modelBuilder
                .Entity<StudentCourse>()
                .HasOne(c => c.Course)
                .WithMany(s => s.StudentsEnrolled);
        }

        private void CreateEntityHomework(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Homework>()
                .HasKey(h => h.HomeworkId);

            modelBuilder
                .Entity<Homework>()
                .Property(h => h.Content)
                .IsUnicode(false);

            modelBuilder
                .Entity<Homework>()
                .HasOne(s => s.Student)
                .WithMany(h => h.HomeworkSubmissions);

            modelBuilder
                .Entity<Homework>()
                .HasOne(c => c.Course)
                .WithMany(h => h.HomeworkSubmissions);
        }

        private void CreateEntityResource(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Resource>()
                .HasKey(r => r.ResourceId);

            modelBuilder
                .Entity<Resource>()
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            modelBuilder
                .Entity<Resource>()
                .HasOne(c => c.Course)
                .WithMany(r => r.Resources);
        }

        private void CreateEntityCourse(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>()
                .HasKey(c => c.CourseId);

            modelBuilder
                .Entity<Course>()
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsRequired(true)
                .IsUnicode(true);

            modelBuilder
                .Entity<Course>()
                .Property(c => c.Description)
                .IsRequired(false)
                .IsUnicode(true);


        }

        private void CreateEntityStudent(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder
                .Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

            modelBuilder
                .Entity<Student>()
                .Property(s => s.PhoneNumber)
                .HasColumnType("CHAR(10)")
                .IsUnicode(false)
                .IsRequired(false);

            modelBuilder
                .Entity<Student>()
                .Property(s => s.RegisteredOn)
                .HasDefaultValueSql("GETDATE()");

            
        }
    }
}
