using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.Models.Attributes
{
    class ExpirationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var curentDate = DateTime.Now;
            var expirationDate = Convert.ToDateTime(value);


            if (curentDate < expirationDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Card is expired!");
        }
    }
}
