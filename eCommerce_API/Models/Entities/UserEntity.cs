using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce_API.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {
        public UserEntity()
        {
        }

        public UserEntity(string firstName, string lastName, string? email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public UserEntity(string firstName, string lastName, string email, int addressId, int contactInfoId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AddressId = addressId;
            ContactInfoId = contactInfoId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Email { get; set; }
        [Required]
        public byte[] Security { get; private set; }
        [Required]
        public byte[] SecurityLayer { get; private set; }


        [Required]
        public int AddressId { get; set; }
        public AddressEntity? Address { get; set; }

        [Required]
        public int ContactInfoId { get; set; }
        public ContactInfoEntity? ContactInfo { get; set; }

        public void EncryptPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                SecurityLayer = hmac.Key;
                Security = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool CompareEncryptedPassword(string password)
        {
            using (var hmac = new HMACSHA512(SecurityLayer))
            {
                var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < _hash.Length; i++)
                {
                    if (_hash[i] != Security[i])
                        return false;
                }
            }
            return true;
        }

    }
}
