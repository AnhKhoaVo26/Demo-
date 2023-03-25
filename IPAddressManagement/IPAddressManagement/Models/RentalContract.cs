using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddressManagement.Models
{
    [Table("RentalContract ")]
    public class RentalContract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_RentalContract { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        //khoa ngoai
        public int? ID_IPAddress { get; set; }

        [ForeignKey("ID_IPAddress")]
        public IPAddresss IPAddresss { get; set; }

        public int? ID_Customer { get; set; }
        [ForeignKey("ID_Customer")]
        public Customer Customer { get; set; }

        public int? ID_User { get; set; }
        [ForeignKey("ID_User")]
        public User User { get; set; }
    }
}
