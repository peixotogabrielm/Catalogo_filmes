using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Data
{
    public class SeedManager
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            await SeedRoles(serviceProvider).ConfigureAwait(false);
            await SeedAdminUser(serviceProvider).ConfigureAwait(false);
        }
        private static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);
            }
        }

    private static async Task SeedAdminUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Usuario>>();
            string adminEmail = "admin@catalogofilmes.com";
            string adminPassword = "Admin@123!";

            var existingUser = await userManager.FindByEmailAsync(adminEmail).ConfigureAwait(false);
            var existingUserByUserName = await userManager.FindByNameAsync(adminEmail).ConfigureAwait(false);
            if (existingUser == null && existingUserByUserName == null)
            {
                var adminUser = new Usuario
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nome = "Administrador",
                    Provider = "Local",
                    CPF = "00000000000",
                    Celular = "00000000000"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword).ConfigureAwait(false);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin").ConfigureAwait(false);
                else
                    throw new Exception("Erro ao criar admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            else
            {
                return;
            }
        }

    }
}