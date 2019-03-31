using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        [MinLength(6), MaxLength(20)]
        public string Password { get; set; }
        public ICollection<PaymentMethod> PaymentMethod { get; set; }

        public User()
        {
            this.PaymentMethod = new HashSet<PaymentMethod>();
        }

    }
}
