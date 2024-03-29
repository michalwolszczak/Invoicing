﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Product name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Gross price")]
        public decimal GrossPrice { get; set; }
        [Required]
        [DisplayName("Net price")]
        public decimal NetPrice { get; set; }
        public int Tax { get; set; }
        [DisplayName("Quantity unit")]
        public string? QuantityUnit { get; set; }
        [JsonIgnore]
        public List<InvoiceProduct>? Invoices { get; set; } = new List<InvoiceProduct>();
        //[JsonIgnore]
        //public Company Company { get; set; }
    }
}
