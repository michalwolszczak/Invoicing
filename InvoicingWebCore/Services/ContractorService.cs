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

        public bool Create(Contractor model)
        {
            //model.Company = _loggedUser.Company;
            _db.Contractors.Add(model);
            _db.SaveChanges();

            return true;
        }

        public Contractor Get(int contractorId)
        {
            var contractor = _db.Contractors.Find(contractorId);
            if (contractor != null)
            {
                return contractor;
            }

            return null;
        }

        public IEnumerable<Contractor> GetAll()
        {
            return _db.Contractors.ToList();
        }

        public void Update(Contractor model)
        {
            _db.Contractors.Update(model);
            _db.SaveChanges();    
        }

        public bool Delete(int contractorId)
        {
            Contractor contractor = _db.Contractors.Find(contractorId);
            if(contractor != null)
            {
                _db.Contractors.Remove(contractor);
                _db.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
