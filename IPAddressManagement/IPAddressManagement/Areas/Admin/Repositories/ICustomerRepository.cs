using IPAddressManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace IPAddressManagement.Areas.Admin.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerByID(int customerId);
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        void UpdateCustomer(Customer customer);
        void Save();
        Customer GetCustomerByEmail(string name);
        bool ExistsCustomerByEmail(string name);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private MyDbContext context;

        public CustomerRepository(MyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }
        public Customer GetCustomerByID(int customerId)
        {
            return context.Customers.Find(customerId);
        }
        public void InsertCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }
        public void DeleteCustomer(int customerId)
        {
            Customer gr = context.Customers.Find(customerId);
            context.Customers.Remove(gr);
        }
        public void UpdateCustomer(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public Customer GetCustomerByEmail(string name)
        {
            return context.Customers.Where(u => u.Email.Equals(name)).FirstOrDefault();
        }

        public bool ExistsCustomerByEmail(string name)
        {
            return context.Customers.Any(u => u.Email.Equals(name));
        }

    }
}
