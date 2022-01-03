﻿using KingsStoreApi.Data.Implementations;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UserController(UserManager<User> userManager, IAuthenticationManager authenticationManager, SignInManager<User> signInManager, IUserService service, IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _userService = service;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            var result = await _userService.GetUserAsync(email);

            if (!result.Success)
                return NotFound(result.Message);
            var user = result.Object as User;

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();

            if (!result.Success)
                return NotFound(result.Message);

            var users = result.Object as IEnumerable<User>;
            return Ok(users);
        }
        [HttpGet("vendors")]
        public IActionResult GetAllVendors()
        {
            var result = _userService.GetAllVendors();

            if (!result.Success)
                return NotFound(result.Message);
            var vendors = result.Object as IEnumerable<User>;

            return Ok(vendors);
        }
        [HttpGet("activeUsers")]
        public IActionResult GetAllActiveUsers()
        {
            var result = _userService.GetAllActiveUsers();

            if (!result.Success)
                return NotFound(result.Message);
            var users = result.Object as IEnumerable<User>;

            return Ok(users);
        }
        [HttpGet("customers")]
        public IActionResult GetAllCustomers()
        {
            var result = _userService.GetAllCustomers();

            if (!result.Success)
                return NotFound(result.Message);
            var users = result.Object as IEnumerable<User>;

            return Ok(users);
        }
        [HttpPost("removeVendor")]
        public async Task<IActionResult> UnmakeUserAVendorAsync(string email)
        {
            var result = await _userService.UnMakeUserAVendorAsync(email);


            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("makeAdmin")]
        public async Task<IActionResult> MakeUserAnAdminAsync(string email)
        {
            var result = await _userService.MakeUserAnAdminAsync(email);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("removeAdmin")]
        public async Task<IActionResult> UnmakeUserAnAdminAsync(string email)
        {
            var result = await _userService.UnMakeUserAnAdminAsync(email);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("updatePic")]
        public async Task<IActionResult> UpdateUserProfilePic(UploadImageDTO model)
        {
            var result = await _userService.UpdateUserProfilePic(model);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("updateBio")]
        public async Task<IActionResult> UpdateUserBio(string newBio)
        {
            var result = await _userService.UpdateUserBio(newBio);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("updateName")]
        public async Task<IActionResult> UpdateUserFullName(UpdateFullNameDTO model)
        {
            var result = await _userService.UpdateUserFullName(model);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("removePic")]
        public async Task<IActionResult> RemoveUserProfilePicture(string email)
        {
            var result = await _userService.RemoveProfilePicture(email);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInDTO model)
        {
            var result = await _userService.LogIn(model);
            if (!result.Success)
            {
                return result.Message.Contains("User Not found") ?
                    NotFound(result.Message) : Unauthorized(result.Message);
            }

            var token = result.Object as string;
            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var registrationResult = await _userService.RegisterAsync(model);

            if (!registrationResult.Success)
                return BadRequest(registrationResult.Message);

            return Ok(registrationResult.Message);
        }

        [HttpPost("makeVendor")]
        public async Task<IActionResult> MakeUserAVendorAsync(string email)
        {
            var result = await _userService.MakeUserAVendorAsync(email);

            if (!result.Success)
            {
                return result.Message.Contains("not found") ?
                    NotFound(result.Message) : BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost("toggleActiveStatus")]
        public async Task<IActionResult> ToggleUserActivationStatusAsync(string email)
        {
            var result = await _userService.ToggleUserActivationStatusAsync(email);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("toggleSoftDelete")]
        public async Task<IActionResult> ToggleUserSoftDeleteAsync(string email)
        {
            var result = await _userService.ToggleUserSoftDeleteAsync(email);

            return Ok(result.Message);
        }
    }
}