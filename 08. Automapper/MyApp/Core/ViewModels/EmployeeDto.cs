﻿using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.ViewModels
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
            
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }

        
    }
}
