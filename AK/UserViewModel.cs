using IPAddressManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace IPAddressManagement.Areas.Admin.Models
{
    public class UserViewModel
    {
        public int ID_User { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }

        public string NewPassWord { get; set; }

        public string PhoneNumber { get; set; }

        public int ID_Group { get; set; }

    }
}
