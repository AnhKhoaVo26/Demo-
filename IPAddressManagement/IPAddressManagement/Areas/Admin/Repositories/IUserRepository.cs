using IPAddressManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace IPAddressManagement.Areas.Admin.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
        User GetUserByEmail(string email);
        bool ExistsUserByEmail(string email);
    }

    public class UserRepository : IUserRepository
    {
        private MyDbContext context;

        public UserRepository(MyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.Include(d => d.Group).ToList();
        }
        public User GetUserByID(int userId)
        {
            return context.Users.Find(userId);
        }
        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }
        public void DeleteUser(int userId)
        {
            User u = context.Users.Find(userId);
            context.Users.Remove(u);
        }
        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public User GetUserByEmail(string email)
        {
            return context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }

        public bool ExistsUserByEmail(string email)
        {
            return context.Users.Any(u => u.Email.Equals(email));
        }
    }
}
