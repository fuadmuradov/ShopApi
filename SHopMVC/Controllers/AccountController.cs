using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHopMVC.DAL;
using SHopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(MyDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        //public async Task CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await roleManager.CreateAsync(new IdentityRole("Member"));
        //}

        //public async Task CreateUsers()
        //{
        //    AppUser user1 = new AppUser() { Fullname = "Fuad Muradov", UserName="fuadmuradov"};
        //    AppUser user2 = new AppUser() { Fullname = "Murad Muradov", UserName = "muradmuradov" };
        //    AppUser user3 = new AppUser() { Fullname = "Orxan Qarayev", UserName = "orxanqarayev" };
        //    AppUser user4 = new AppUser() { Fullname = "Nurlan Qarayev", UserName = "nurlanqarayev" };

        //    await userManager.CreateAsync(user1, "User@12345");
        //    await userManager.AddToRoleAsync(user1, "SuperAdmin");

        //    await userManager.CreateAsync(user2, "User@12345");
        //    await userManager.AddToRoleAsync(user2, "Admin");

        //    await userManager.CreateAsync(user3, "User@12345");
        //    await userManager .AddToRoleAsync(user3, "Admin");

        //    await userManager.CreateAsync(user4, "User@12345");
        //    await userManager .AddToRoleAsync(user4, "Member");

        //}

        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await userManager.FindByNameAsync(login.Username);
            if (user == null) return View();

            var result = await signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password Incorrect");
                return View();
            }


            return View();
        }



    }
}
