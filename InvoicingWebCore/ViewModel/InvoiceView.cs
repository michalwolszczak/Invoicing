using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoicingWebCore.ViewModel
{
    public class InvoiceView //: PageModel
    {
        
        public Invoice Invoice { get; set; }
        public IEnumerable<InvoiceType> InvoiceTypeList { get; set; }
        public IEnumerable<Product> ProductsList { get; set; }
        public IEnumerable<Contractor> ContractorsList { get; set; }
        public ProductInvoice ProductInvoice { get; set; }


        //public InvoiceView()
        //{

        //}
        //public void OnPost()
        //{
            
        //}
    }
}
