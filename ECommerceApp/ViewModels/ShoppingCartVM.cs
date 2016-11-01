using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceApp.ViewModels
{
    public class ShoppingCartVM
    {
        [Key]
        public int RecordID { get; set; }

        [Required]
        [StringLength(50)]
        public string CartID { get; set; }

        public int Quantity { get; set; }

        public int BookID { get; set; }

        public DateTime DateCreated { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
    }
}