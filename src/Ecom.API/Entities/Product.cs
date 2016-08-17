using Ecom.API.Repository.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ecom.API.Entities
{
    public class Product: IEntityBase
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Supplier { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        
    }
}
