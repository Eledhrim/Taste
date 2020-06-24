using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Taste.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string ApplicationUserId { get; set; }

        [Range(1,100, ErrorMessage = "Please select a count 1 and 100")]
        public int Count { get; set; }

        [NotMapped]
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public ShoppingCart()
        {
            Count = 1;
        }
    }
}
