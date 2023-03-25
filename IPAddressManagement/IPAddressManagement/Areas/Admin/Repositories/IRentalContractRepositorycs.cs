using IPAddressManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace IPAddressManagement.Areas.Admin.Repositories
{
    public interface IRentalContractRepository
    {
        IEnumerable<RentalContract> GetRentalContracts();
        RentalContract GetRentalContractByID(int rentalcontractId);
        void InsertRentalContract(RentalContract rentalContract);
        void DeleteRentalContract(int rentalcontractId);
        void UpdateRentalContract(RentalContract rentalContract);
        void Save();
        RentalContract GetRenntalContractByName(string name);
        bool ExistsRenntalContractByName(string name);
    }

    public class RentalContractRepository : IRentalContractRepository
    {
        private MyDbContext context;

        public RentalContractRepository(MyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<RentalContract> GetRentalContracts()
        {
            return context.RentalContracts.ToList();
        }
        public RentalContract GetRentalContractByID(int rentalcontractId)
        {
            return context.RentalContracts.Find(rentalcontractId);
        }
        public void InsertRentalContract(RentalContract rentalContract)
        {
            context.RentalContracts.Add(rentalContract);
        }
        public void DeleteRentalContract(int rentalcontractId)
        {
            RentalContract gr = context.RentalContracts.Find(rentalcontractId);
            context.RentalContracts.Remove(gr);
        }
        public void UpdateRentalContract(RentalContract rentalContract)
        {
            context.Entry(rentalContract).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public RentalContract GetRenntalContractByName(string name)
        {
            return context.RentalContracts.Where(g => g.Name.Equals(name)).FirstOrDefault();
        }

        public bool ExistsRenntalContractByName(string name)
        {
            return context.RentalContracts.Any(g => g.Name.Equals(name));
        }
    }
}
