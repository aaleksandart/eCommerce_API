using eCommerce_API.Models.CreateModels;
using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.SupportModels;
using eCommerce_API.Models.UpdateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_API.Services
{
    public interface IUserService
    {
        Task<List<UserDisplayModel>> GetUsersAsync();
        Task<ActionResult<UserDisplayModel>> GetUserAsync(int id);
        Task<IActionResult> UpdateUserAsync(int id, UserUpdateModel updateUser);
        Task<ActionResult<UserDisplayModel>> CreateUserAsync(UserCreateModel createUser);
        Task<IActionResult> DeleteUserAsync(int id);
    }
    public class UserService : ControllerBase, IUserService
    {
        private readonly SqlContext _context;

        public UserService(SqlContext context)
        {
            _context = context;
        }
        public async Task<List<UserDisplayModel>> GetUsersAsync()
        {
            List<UserDisplayModel> userList = new List<UserDisplayModel>();
            var users = await _context.Users
                .Include(x => x.Address)
                .Include(x => x.ContactInfo)
                .ToListAsync();

            foreach (var user in users)
            {
                AddressModel address = new(
                    user.Address.Streetname,
                    user.Address.PostalCode,
                    user.Address.City,
                    user.Address.Country);

                ContactInfoModel contactinfo = new(
                    user.ContactInfo.PhoneNumber);

                userList.Add(new UserDisplayModel(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    address,
                    contactinfo));
            }
            return userList;
        }

        public async Task<ActionResult<UserDisplayModel>> GetUserAsync(int id)
        {
            var userEntity = await _context.Users
                .Include(x => x.Address)
                .Include(x => x.ContactInfo)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (userEntity == null)
                return NotFound();

            AddressModel address = new(
                userEntity.Address.Streetname,
                userEntity.Address.PostalCode,
                userEntity.Address.City,
                userEntity.Address.Country);

            ContactInfoModel contactinfo = new(
                userEntity.ContactInfo.PhoneNumber);

            UserDisplayModel user = new(
                userEntity.Id,
                userEntity.FirstName,
                userEntity.LastName,
                userEntity.Email,
                address,
                contactinfo);

            return user;
        }

        public async Task<IActionResult> UpdateUserAsync(int id, UserUpdateModel updateUser)
        {
            var existingUser = await _context.Users
                .Include(x => x.Address)
                .Include(x => x.ContactInfo)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (existingUser == null)
                return BadRequest("A user with that ID dont exist.");
            if (updateUser.PostalCode.Length != 5)
                return BadRequest("Input for postalcode needs to be 5 digits.");

            var address = await _context.Addresses
                .FirstOrDefaultAsync(
                x => x.Streetname == updateUser.StreetName
                && x.PostalCode == updateUser.PostalCode);

            if (address == null)
            {
                AddressEntity newAddress = new AddressEntity(
                   updateUser.StreetName,
                   updateUser.PostalCode,
                   updateUser.City,
                   updateUser.Country);

                existingUser.Address = newAddress;
                await _context.Addresses.AddAsync(newAddress);
                await _context.SaveChangesAsync();
            }
            
            var contactInfo = await _context.ContactInfo
                .Where(x => x.PhoneNumber == updateUser.PhoneNumber)
                .FirstOrDefaultAsync();

            if (contactInfo == null)
            {
                ContactInfoEntity newContactInfo = new(
                    updateUser.PhoneNumber);

                existingUser.ContactInfo = newContactInfo;
                await _context.ContactInfo.AddAsync(newContactInfo);
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(updateUser.FirstName))
                existingUser.FirstName = updateUser.FirstName;
            if (!string.IsNullOrEmpty(updateUser.LastName))
                existingUser.LastName = updateUser.LastName;

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        public async Task<ActionResult<UserDisplayModel>> CreateUserAsync(UserCreateModel createUser)
        {
            var user = await _context.Users
                .Include(x => x.Address)
                .Include(x => x.ContactInfo)
                .Where(x => x.Email == createUser.Email)
                .FirstOrDefaultAsync();

            var address = await _context.Addresses
                .Where(x => x.Streetname == createUser.StreetName && x.PostalCode == createUser.PostalCode)
                .FirstOrDefaultAsync();

            var contactInfo = await _context.ContactInfo
                .Where(x => x.PhoneNumber == createUser.PhoneNumber)
                .FirstOrDefaultAsync();

            if (user != null)
                return BadRequest("The email address already exists in the database.");

            if (address == null)
            {
                address = new AddressEntity(
                    createUser.StreetName, 
                    createUser.PostalCode, 
                    createUser.City, 
                    createUser.Country);

                if (createUser.PostalCode.Length != 5)
                    return BadRequest("Input for postalcode needs to be 5 digits.");

                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
            }
            if (contactInfo == null)
            {
                contactInfo = new ContactInfoEntity(createUser.PhoneNumber);
                await _context.ContactInfo.AddAsync(contactInfo);
                await _context.SaveChangesAsync();
            }

            user = new UserEntity(createUser.FirstName, createUser.LastName, createUser.Email, address.id, contactInfo.Id);
            user.EncryptPassword(createUser.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            ContactInfoModel displayContactInfo = new ContactInfoModel(user.ContactInfo.PhoneNumber);
            AddressModel displayAddress = new AddressModel(
                address.Streetname, 
                address.PostalCode, 
                address.City, 
                address.Country);

            return CreatedAtAction(
                "GetUser",
                new { id = user.Id },
                new UserDisplayModel(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    displayAddress,
                    displayContactInfo));
        }

        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var userDelete = await _context.Users.FindAsync(id);
            if (userDelete == null)
                return BadRequest("A user with that ID dont exist.");

            _context.Users.Remove(userDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
