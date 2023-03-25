using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddressManagement.Models
{
    [Table("IPAddress")]
    public class IPAddresss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_IPAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string IPAddressName { get; set; }
       
        public Boolean Status { get; set; } 
        
        public DateTime CreatedDate { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public virtual ICollection<RentalContract> RentalContracts { get; set; }
    }
}
