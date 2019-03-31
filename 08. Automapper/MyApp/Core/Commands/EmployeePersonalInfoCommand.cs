using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int id = int.Parse(inputArgs[0]);

            var employee = context.Employees
                .FirstOrDefault(e => e.Id == id);

            string result = $"{employee.Id} {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}"
                + Environment.NewLine
                + $"Birthday: {employee.BirthDay.Value.ToString("dd-MM-yyyy")}"
                + Environment.NewLine
                + $"Address: {employee.Address}";
            return result;
        }
    }
}
