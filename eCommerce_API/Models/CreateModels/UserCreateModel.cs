namespace eCommerce_API.Models.CreateModels
{
    public class UserCreateModel
    {
        private string? firstName;
        private string? lastName;
        private string? email;
        private string? password;

        private string? phoneNumber;

        private string? streetName;
        private string? postalCode;
        private string? city;
        private string? country;
        
        
        public string? FirstName 
        {
            get { return firstName; }
            set { firstName = value?.Trim(); }
        }
        public string? LastName 
        {
            get { return lastName; }
            set { lastName = value?.Trim(); }
        }
        public string? Email 
        {
            get { return email; }
            set { email = value?.Trim(); }
        }
        public string? Password 
        {
            get { return password; }
            set { password = value?.Trim(); }
        }


        public string? PhoneNumber 
        {
            get { return phoneNumber; }
            set { phoneNumber = value?.Trim(); }
        }


        public string? StreetName 
        {
            get { return streetName; }
            set { streetName = value?.Trim(); }
        }
        public string? PostalCode 
        {
            get { return postalCode; }
            set { postalCode = value?.Replace(" ", ""); }
        }
        public string? City 
        {
            get { return city; }
            set { city = value?.Trim(); }
        }
        public string? Country 
        {
            get { return country; }
            set { country = value?.Trim(); }
        }
    }
}
