﻿using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.ViewModels
{
    public class ManagerDto
    {
        public ManagerDto()
        {
            this.ManagedEmployees = new HashSet<EmployeeDto>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }



        public HashSet<EmployeeDto> ManagedEmployees { get; set; }
    }
}
