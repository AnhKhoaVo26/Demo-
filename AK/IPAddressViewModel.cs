using IPAddressManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Areas.Admin.Models
{
    public class IPAddressViewModel
    {

        public int ID_IPAddress { get; set; }

        [Required]
        public string IPAddressName { get; set; }

        [Required]
        public bool Available { get; set; }

    }
}
