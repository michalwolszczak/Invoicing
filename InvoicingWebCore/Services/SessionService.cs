using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace InvoicingWebCore.Services
{
    public class SessionService //: ISessionService
    {
        private readonly IInvoiceService _invoiceService;
        //private readonly IUserService _userService;
        //private readonly IDatabaseService _databaseService;
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;
        public SessionService(IInvoiceService invoiceService,
            //IUserService userService,
            //IDatabaseService databaseService,
            ApplicationDbContext db)
            //ILogger logger)
        {
            _invoiceService = invoiceService;
            //_userService = userService;
            //_databaseService = databaseService;
            _db = db;
            //_logger = logger;
        }

        //public bool CreateInvoice(string model, ApplicationUser user)
        //{
        //    try
        //    {
        //        var invoiceModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceModel>(model);
        //        if(invoiceModel != null)
        //        {
        //            var invoice = _invoiceService.Create(invoiceModel, user);
                    
        //            _db.Invoices.Add(invoice);
        //            _db.SaveChanges();

        //            var updatedInvoice = _db.Invoices.FirstOrDefault(x => x.Number == invoice.Number);

        //            _invoiceService.CreateInvoiceProducts(invoiceModel.Products, updatedInvoice.Id);

        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }            
        //}        

        //public IEnumerable<Contractor> GetAllContractors(ApplicationUser loggedUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Invoice> GetAllInvoices(ApplicationUser loggedUser)
        //{            
        //    return _db.Invoices.Where(x => x.Company == loggedUser.Company).Include(it => it.InvoiceType).ToList();
        //}

        //public IEnumerable<Product> GetAllProducts(ApplicationUser loggedUser)
        //{
        //    throw new NotImplementedException();
        //}

        //public ApplicationUser GetLoggedUser(string userId)
        //{
        //    try
        //    {
        //        return _db.Users.Include(c => c.Company).FirstOrDefault(x => x.Id == userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Cannot get user from database");
        //        throw;
        //    }
        //}

        //public Invoice UpdateInvoice(InvoiceModel model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
