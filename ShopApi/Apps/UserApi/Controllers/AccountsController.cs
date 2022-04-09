using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Apps.UserApi.DTOs.Account;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopApi.Apps.UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        //[HttpGet("roles")]
        //public async Task<IActionResult> SeedRoles()
        //{
        //    var result = await roleManager.CreateAsync(new IdentityRole("Member"));
        //     result = await roleManager.CreateAsync(new IdentityRole("Admin"));
        //     result = await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

        //    return Ok();
        //}

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await userManager.FindByNameAsync(registerDto.UserName);
            if (user != null) return StatusCode(409);

            user = new AppUser()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var roleresult = await userManager.AddToRoleAsync(user, "Member");
            if (!roleresult.Succeeded)
            {
                return BadRequest(roleresult.Errors);
            }


            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await userManager.FindByNameAsync(loginDto.UserName);

            if(user == null)
            {
                return BadRequest();
            }

            if(!await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return NotFound();
            }

            //JWT Token

           

            return Ok(new { token = "" });
        }

        [Authorize]
        public async Task<IActionResult> Get()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            AccountUserGetDto userGetDto = new AccountUserGetDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName
            };

            return Ok(userGetDto);
        }

    }
}
