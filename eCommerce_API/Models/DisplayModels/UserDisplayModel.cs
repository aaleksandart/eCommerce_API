using eCommerce_API.Models.Entities;
using eCommerce_API.Models.SupportModels;

namespace eCommerce_API.Models.DisplayModels
{
    public class UserDisplayModel
    {
        public UserDisplayModel()
        {
        }

        public UserDisplayModel(int id, string? firstName, string? lastName, string? email, AddressModel? address, ContactInfoModel? contactInfo)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            ContactInfo = contactInfo;
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public AddressModel? Address { get; set; }
        public ContactInfoModel? ContactInfo { get; set; }
    }
}
