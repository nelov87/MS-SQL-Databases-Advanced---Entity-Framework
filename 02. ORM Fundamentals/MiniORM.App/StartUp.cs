using MiniORM.App.Data;
using MiniORM.App.Data.Entities;
using System;
using System.Linq;

namespace MiniORM.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connectionSrting = @"Server=DESKTOP-533LOVH\SQLEXPRESS;Database=MiniORM;Integrated Security=true";

            var context = new SoftUniDbContext(connectionSrting);

            context.Employees.Add(new Employee
            {
                FirstName = "Goshko",
                LastName = "Goshkov",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            var employee = context.Employees.Last();
            employee.FirstName = "Modifaied";

            context.SaveChanges();
        }
    }
}
