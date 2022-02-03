using BlogMVC.Data;
using BlogMVC.Enums;
using BlogMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;


        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            // Creat DB from the Migrations
            await _dbContext.Database.MigrateAsync();

            // 1. Seed a few Roles into the system
            await SeedRolesAsync();

            // 2. Seed a few User into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // If Roles already exist in the system, do nothing.
            if (_dbContext.Roles.Any())
            {
                return;
            }

            // Else create some Roles
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                // Use Role Manager to create Roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // If Users already exist in the system, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            // 1. Create new instance of BlogUser(admin)
            var adminUser = new BlogUser()
            {
                Email = "delashmuttwa@gmail.com",
                UserName = "delashmuttwa@gmail.com",
                FirstName = "Billy",
                LastName = "DeLashmutt",
                PhoneNumber = "(800) 555-1234",
                EmailConfirmed = true
            };

            // 2. Use UserManager to create new user that is defined by adminUser
            await _userManager.CreateAsync(adminUser, "Asdf123^");

            // 3. Add this new user to the Administrator Role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            // Create new BlogUser(Moderator)
            var modUser = new BlogUser()
            {
                Email = "delashmuttwa@vt.edu",
                UserName = "delashmuttwa@vt.edu",
                FirstName = "Bee",
                LastName = "Dee",
                PhoneNumber = "(800) 123-1234",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(modUser, "Asdf123^");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
