using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    [Table("Products")]
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; } = "No description";

        public ICollection<Sale> Sales { get; set; }

        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }
    }
}
