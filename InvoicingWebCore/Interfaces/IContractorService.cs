using InvoicingWebCore.Models;

namespace InvoicingWebCore.Interfaces
{
    public interface IContractorService
    {
        bool Create(Contractor model);
        void Update(Contractor model);
        bool Delete(int contractorId);
        Contractor Get(int contractorId);
        IEnumerable<Contractor> GetAll();
    }
}
