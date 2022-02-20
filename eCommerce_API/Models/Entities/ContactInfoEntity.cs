using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_API.Models.Entities
{
    public class ContactInfoEntity
    {
        public ContactInfoEntity()
        {
        }

        public ContactInfoEntity(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        public ICollection<UserEntity>? UserEntities { get; set; }
    }
}
