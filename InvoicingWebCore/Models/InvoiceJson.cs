using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class InvoiceJson
    {
        [JsonPropertyName("invoiceTypeId")]
        public int InvoiceTypeId;

        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber;

        [JsonPropertyName("saleDate")]
        public DateTime SaleDate;

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate;

        [JsonPropertyName("contractorId")]
        public int ContractorId;

        [JsonPropertyName("products")]
        public List<Product> Products;

        public class Product
        {
            [JsonPropertyName("id")]
            public int Id;

            [JsonPropertyName("netPrice")]
            public decimal NetPrice;

            [JsonPropertyName("netPrice")]
            public decimal GrossPrice;

            [JsonPropertyName("quantity")]
            public int Quantity;
            public decimal TotalGross { get; set; }
            public decimal TotalNet { get; set; }

            [JsonPropertyName("quantityUnit")]
            public string QuantityUnit;

            [JsonPropertyName("tax")]
            public int Tax;
        }
    }
}
