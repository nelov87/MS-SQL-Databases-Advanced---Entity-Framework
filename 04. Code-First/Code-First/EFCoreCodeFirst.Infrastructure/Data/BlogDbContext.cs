using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreCodeFirst.Infrastructure.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasData(new User()
            //{
            //    Id = 1,
            //    FirstName = "Gosho",
            //    LastName = "Petrov",
            //    email = "gosho@petrov.com"
            //});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=BlogDb;Integrated Security=true", s => s.MigrationsAssembly("EFCoreCodeFirst.Infrastructure"));
            }
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Replay> Replays { get; set; }

    }
}
