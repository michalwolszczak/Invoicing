using InvoicingWebCore.Models;

namespace InvoicingWebCore.Interfaces
{
    public interface IContractorService
    {
        Contractor Create();
        Contractor Update(int contractorId);
        Contractor Get(int contractorId);
        IEnumerable<Contractor> GetAll(ApplicationUser loggedUser);
    }
}
