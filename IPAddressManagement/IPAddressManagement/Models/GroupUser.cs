using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPAddressManagement.Models
{
    [Table("Group")]
    public class GroupUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Group { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Decription { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
