﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Taste.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name = "Pick Up Time")]
        public DateTime PickUpTime { get; set; }

        [Required]
        [NotMapped] //Notmapped because on DB PickUpTime will consist PickUpDate. We use this for display purposes
        public DateTime PickUpDate { get; set; }

        public string Status { get; set; }

        public string PaymentStatus { get; set; }

        public string Comments { get; set; }

        [Display(Name = "Pick Up Name")]
        public string PickUpName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string TransactionId { get; set; }
    }
}