using AddressBook.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Data
{
    public enum Roles
    {
        Admin,
        User
    }

    public static class ContextSeed
    {

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }

        public static async Task SeedDefaultUsersAsync(UserManager<AddressUser> userManager)
        {
            var defaultUser = new AddressUser
            {
                UserName = "nsfrahmann@gmail.com",
                Email = "nsfrahmann@gmail.com",
                FirstName = "Nathan",
                LastName = "Frahmann",
                EmailConfirmed = false
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "P@ssword1");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("____________________________________");
                Debug.WriteLine("Error Seeding Default Admin User");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("____________________________________");
                throw;
            }
        }
    }
}
