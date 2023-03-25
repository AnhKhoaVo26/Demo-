using IPAddressManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace IPAddressManagement.Areas.Admin.Repositories
{
    public interface IIpsRepository
    {
        IEnumerable<IPAddresss> GetIps();
        IPAddresss GetIpByID(int ipId);
        void InsertIp(IPAddresss iPAddressid);
        void DeleteIp(int ipId);
        void UpdateIp(IPAddresss iPAddressid);
        void Save();
        IPAddresss GetIpByName(string name);
        bool ExistsIpByName(string name);
    }

    public class IpRepository : IIpsRepository
    {
        private MyDbContext context;

        public IpRepository(MyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<IPAddresss> GetIps()
        {
            return context.IPAddressses.ToList();
        }
        public IPAddresss GetIpByID(int ipId)
        {
            return context.IPAddressses.Find(ipId);
        }
        public void InsertIp(IPAddresss iPAddress)
        {
            context.IPAddressses.Add(iPAddress);
        }
        public void DeleteIp(int ipId)
        {
            IPAddresss gr = context.IPAddressses.Find(ipId);
            context.IPAddressses.Remove(gr);
        }
        public void UpdateIp(IPAddresss iPAddressid)
        {
            context.Entry(iPAddressid).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public IPAddresss GetIpByName(string name)
        {
            return context.IPAddressses.Where(g => g.IPAddressName.Equals(name)).FirstOrDefault();
        }

        public bool ExistsIpByName(string name)
        {
            return context.IPAddressses.Any(g => g.IPAddressName.Equals(name));
        }
    }
}
