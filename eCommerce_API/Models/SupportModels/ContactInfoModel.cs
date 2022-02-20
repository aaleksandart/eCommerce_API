using System.ComponentModel.DataAnnotations;

namespace eCommerce_API.Models.SupportModels
{
    public class ContactInfoModel
    {
        public ContactInfoModel()
        {
        }

        public ContactInfoModel(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string? PhoneNumber { get; set; }
    }
}
