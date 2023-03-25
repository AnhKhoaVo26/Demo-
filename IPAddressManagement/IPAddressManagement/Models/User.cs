using IPAddressManagement.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddressManagement.Models
{
    [Table("User")]
    public class User
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_User { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string PassWord { get; set; }

        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        public int? ID_Group { get; set; }
       
        [ForeignKey("ID_Group")]
        public GroupUser Group { get; set; }

        public virtual ICollection<RentalContract> RentalContracts { get; set; }
    }
}
