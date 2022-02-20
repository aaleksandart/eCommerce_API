using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_API.Models.Entities
{
    public class AddressEntity
    {
        public AddressEntity()
        {
        }

        public AddressEntity(string streetname, string postalCode, string city, string? country)
        {
            Streetname = streetname;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Streetname { get; set; }

        [Required]
        [Column(TypeName = "char(5)")]
        public string? PostalCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? City { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Country { get; set; }

        public ICollection<UserEntity>? UserEntities { get; set; }
    }
}
