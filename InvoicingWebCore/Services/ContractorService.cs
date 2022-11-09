using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;

namespace InvoicingWebCore.Services
{
    public class ContractorService : IContractorService
    {
        private readonly ApplicationDbContext _db;
        public ContractorService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Contractor Create()
        {
            throw new NotImplementedException();
        }

        public Contractor Get(int contractorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contractor> GetAll(ApplicationUser loggedUser)
        {
            return _db.Contractors.Where(x => x.Company == loggedUser.Company).ToList();
        }

        public Contractor Update(int contractorId)
        {
            var contractor = _db.Contractors.Find(contractorId);
            if (contractor != null)
            {
                return contractor;
            }
            return null;
        }
    }
}
