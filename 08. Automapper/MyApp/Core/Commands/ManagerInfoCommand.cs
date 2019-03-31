using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext context;

        public ManagerInfoCommand(MyAppContext context)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            int managerId = int.Parse(inputArgs[0]);

            var manager = this.context.Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(e => e.Id == managerId);

            string result = $"{manager.FirstName} {manager.LastName} | Employees: {manager.ManagedEmployees.Count}" 
                + Environment.NewLine
                + string.Join(Environment.NewLine, manager.ManagedEmployees.Select(e => $"    - {e.FirstName} {e.LastName} - ${e.Salary:F2}"));
            return result;
        }
    }
}
