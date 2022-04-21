using InvoicingWebCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingWebCore.Controllers
{
    public class InvoiceDetailsController : Controller
    {
        public IActionResult Index(Invoice obj)
        {
            return View(obj);
        }        
    }
}
