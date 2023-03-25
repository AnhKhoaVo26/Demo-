using System.ComponentModel.DataAnnotations;

namespace IPAddressManagement.Areas.Admin.Models
{
    public class GroupViewModel
    {
        public int ID_Group { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Decription { get; set; }
       
    }
}
