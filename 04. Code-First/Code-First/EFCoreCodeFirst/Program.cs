using EFCoreCodeFirst.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCoreCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<BlogDbContext> optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();

            string conectionString = "Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=SoftUni;Integrated Security=true";

            optionsBuilder
                .UseSqlServer(conectionString, s => s.MigrationsAssembly("EFCoreCodeFirst.Infrastructure"));



            using (BlogDbContext context = new BlogDbContext())
            {
                var user = context.Users.FirstOrDefault();
            }

        }
    }
}
