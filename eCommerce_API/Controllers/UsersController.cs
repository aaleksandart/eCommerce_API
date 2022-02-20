#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce_API;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.SupportModels;
using eCommerce_API.Models.UpdateModels;
using eCommerce_API.Models.CreateModels;
using eCommerce_API.Filters;
using eCommerce_API.Services;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) =>
            _userService = userService;

        [HttpGet]
        [UserAccessApiKey]
        public async Task<ActionResult<IEnumerable<UserDisplayModel>>> GetUsers() =>
            await _userService.GetUsersAsync();

        [HttpGet("{id}")]
        [UserAccessApiKey]
        public async Task<ActionResult<UserDisplayModel>> GetUser(int id) =>
            await _userService.GetUserAsync(id);

        [HttpPut("{id}")]
        [UserAccessApiKey]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateModel userUpdate) =>
            await _userService.UpdateUserAsync(id, userUpdate);

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDisplayModel>> CreateUser(UserCreateModel newUser) =>
            await _userService.CreateUserAsync(newUser);

        [HttpDelete("{id}")]
        [UserAccessApiKey]
        public async Task<IActionResult> DeleteUser(int id) =>
            await _userService.DeleteUserAsync(id);
    }
}
