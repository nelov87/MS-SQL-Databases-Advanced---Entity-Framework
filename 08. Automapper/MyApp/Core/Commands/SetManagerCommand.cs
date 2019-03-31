using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;

        private readonly Mapper mapper;

        public SetManagerCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            var employee = context.Employees
                .FirstOrDefault(e => e.Id == employeeId);

            var manager = context.Employees
                .FirstOrDefault(m => m.Id == managerId);

            employee.Manager = manager;
            context.SaveChanges();

            var employeeDto = mapper.CreateMappedObject<EmployeeDto>(employee);
            var managerDto = mapper.CreateMappedObject<ManagerDto>(manager);

            string result = $"{employeeDto.FirstName} {employeeDto.LastName} has Manager {managerDto.FirstName} {managerDto.LastName}";

            return result;
        }
    }
}
