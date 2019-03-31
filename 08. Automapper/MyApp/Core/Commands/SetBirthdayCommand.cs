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
    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetBirthdayCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int id = int.Parse(inputArgs[0]);
            DateTime date;

            if (!inputArgs.Any(x => x.Contains("date:")))
            {
                date = DateTime.ParseExact(inputArgs[1], "dd-MM-yyyy", null);
            }
            else
            {
                //string dateString = inputArgs[1].Skip()
                //DateTime date = DateTime.ParseExact(, "dd-MM-yyyy", null);
                date = DateTime.ParseExact(inputArgs[2], "dd-MM-yyyy", null);
            }

            var employee = context.Employees
                .Where(e => e.Id == id)
                .FirstOrDefault();

            employee.BirthDay = date;
            context.SaveChanges();

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            string result = $"{employeeDto.FirstName} {employeeDto.LastName} has burthday {date}";

            return result;
        }
    }
}
