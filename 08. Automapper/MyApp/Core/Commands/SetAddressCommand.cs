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
    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetAddressCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int id = int.Parse(inputArgs[0]);

            var employee = context.Employees
                .Where(e => e.Id == id)
                .FirstOrDefault();

            employee.Address = inputArgs[1];
            context.SaveChanges();

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            string result = $"{employeeDto.FirstName} {employeeDto.LastName} has addres {inputArgs[1]}";
            return result;
        }
    }
}
