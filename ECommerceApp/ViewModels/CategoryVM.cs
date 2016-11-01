using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.ViewModels
{
    public class CategoryVM
    {


        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        //[DisplayName("ID")]
        //[Required]
        //public int CategoryID { get; set; }

        //[DisplayName("Nama Kategori")]
        //[Required]
        //public int CategoryName { get; set; }
    }
}