﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {

        private string targetProperty { get; set; }

        public XorAttribute(string targetProperty)
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetPropertyValue = validationContext
                .ObjectType
                .GetProperty(targetProperty)
                .GetValue(validationContext.ObjectInstance);

            if ((value == null) ^ (targetPropertyValue == null))
            {
                return ValidationResult.Success;
                
            }

            return new ValidationResult("The two properties must have opposite values!");
        }
    }
}
