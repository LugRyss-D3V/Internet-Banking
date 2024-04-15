
using InternetBanking.Core.Application.Enums;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Seeds
{
    public class DefaultAdministratorUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "adminuser",
                Email = "adminuser@email.com",
                FirstName = "John",
                LastName = "Doe",
                PersonalId = "000-0000000-0",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Status = true        
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Pa$$word123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Administrator.ToString());
                }
            }

        }
    }
}
