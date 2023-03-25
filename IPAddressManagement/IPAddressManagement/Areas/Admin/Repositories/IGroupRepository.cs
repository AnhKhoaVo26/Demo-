using IPAddressManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace IPAddressManagement.Areas.Admin.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<GroupUser> GetGroups();
        GroupUser GetGroupByID(int groupId);
        void InsertGroup(GroupUser group);
        void DeleteGroup(int groupId);  
        void UpdateGroup(GroupUser group);
        void Save();
        GroupUser GetGroupByName(string name);
        IEnumerable<GroupUser> SearchGroupByName(string name);
        bool ExistsGroupByName(string name);
    }

    public class GroupRepository : IGroupRepository
    {
        private MyDbContext context;

        public GroupRepository(MyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<GroupUser> GetGroups()
        {
            return context.Groups.ToList();
        }
        public GroupUser GetGroupByID(int groupId)
        {
            return context.Groups.Find(groupId);
        }
        public void InsertGroup(GroupUser group)
        {
            context.Groups.Add(group);
        }
        public void DeleteGroup(int groupId)
        {
            GroupUser gr = context.Groups.Find(groupId);
            context.Groups.Remove(gr);
        }
        public void UpdateGroup(GroupUser group)
        {
            context.Entry(group).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public GroupUser GetGroupByName(string name)
        {
            return context.Groups.Where(g => g.Name.Equals(name)).FirstOrDefault();
        }
        
        public bool ExistsGroupByName(string name)
        {
            return context.Groups.Any(g => g.Name.Equals(name));
        }

        public IEnumerable<GroupUser> SearchGroupByName(string name) // Admin ?? admin ?? aDmin ?? admiN
        {
            return context.Groups.Where(g => g.Name.ToUpper().Contains(name.ToUpper()));
        }
    }
}
