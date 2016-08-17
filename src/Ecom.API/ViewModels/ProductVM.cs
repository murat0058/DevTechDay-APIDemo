using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.Tools.Test;
using System.ComponentModel.DataAnnotations;

namespace Ecom.API.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Product Code must be less than 20 character.")]
        [Required(ErrorMessage = "Product Code is required.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Supplier is required.")]
        public string Supplier { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
