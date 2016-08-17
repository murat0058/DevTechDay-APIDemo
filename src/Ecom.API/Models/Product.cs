using System;

namespace Ecom.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string ProductCode { get; set; }
        
        public string ProductName { get; set; }
        
        public decimal Price { get; set; }
        
        public string Supplier { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}
