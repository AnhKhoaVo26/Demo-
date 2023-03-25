using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace IPAddressManagement.Areas.Admin.Models
{
    public class CustomerViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [Required]

        public string Company { get; set; }
   
    }
}
