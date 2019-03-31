using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ListEmployeesOlderThanCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int age = int.Parse(inputArgs[0]);

            var employees = context.Employees
                .Where(e => (int)(DateTime.Now.Year - e.BirthDay.Value.Year) > age)
                .OrderByDescending(s => s.Salary)
                .ToList();

            var manegerDto = mapper.CreateMappedObject<ManagerDto>(employees);

            StringBuilder result = new StringBuilder();

            foreach (var emp in employees)
            {
                string maneger = emp.Manager == null ? "[no manager]" : emp.Manager.LastName;
                result.AppendLine($"{emp.FirstName} {emp.LastName} - ${emp.Salary:F2} - Manager: {maneger}");

            }



            return result.ToString().TrimEnd(); ;
        }
    }
}
