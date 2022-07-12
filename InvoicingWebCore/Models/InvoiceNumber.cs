using System.Text;

namespace InvoicingWebCore.Models
{
    public class InvoiceNumber
    {
        private StringBuilder _invoiceNumber = new StringBuilder();

        public void SetNumber(int id)
        {
            _invoiceNumber.Append(id);
            _invoiceNumber.Append("/" + DateTime.Now.Month);
            _invoiceNumber.Append("/" + DateTime.Now.Year);
        }
        public string Build()
        {
            return _invoiceNumber.ToString();
        }
    }
}
