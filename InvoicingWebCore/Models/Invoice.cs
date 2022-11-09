using InvoicingWebCore.ViewModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int InvoiceTypeId { get; set; }
        [JsonIgnore]
        public InvoiceType? InvoiceType { get; set; }
        public int? ContractorId { get; set; }
        [JsonIgnore]
        public Contractor? Contractor { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }
        [Required]
        public string Number { get; set; }
        //[Required]
        //public int Tax { get; set; }
        [Required(ErrorMessage = "Creation date is required")]
        [DisplayName("Issue date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Sale date")]
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }
        public List<InvoiceProduct>? Products { get; set; } = new List<InvoiceProduct>();
        [NotMapped]
        public static List<string> QuantityUnitList { get; set; } = new List<string> { " ", "szt", "godz", "dni" };

        public Invoice()
        {
        }

        public Invoice(InvoiceModel invoiceModel)
        {
            InvoiceTypeId = invoiceModel.InvoiceTypeId;
            ContractorId = invoiceModel.ContractorId;
            Number = invoiceModel.Number;
            CreationDate = invoiceModel.CreationDate;
            SaleDate = invoiceModel.SaleDate;
        }
    }
}
