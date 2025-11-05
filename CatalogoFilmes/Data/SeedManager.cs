using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.AspNetCore.Identity;

namespace CatalogoFilmes.Data
{
    public class SeedManager
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            await SeedRoles(serviceProvider);
            await SeedAdminUser(serviceProvider);
        }
        private static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Roles>>();
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Roles { Role = roleName });
                }
            }
        }
        private static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Roles>>();
            string adminEmail = "admin@catalogofilmes.com";
            string adminPassword = "Admin@123";
            var adminUser = new Usuario {  Email = adminEmail, Nome = "Admin", CPF="00000000000", Celular="0000000000", RoleId = (await roleManager.FindByNameAsync("Admin")).Id };
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Falha ao criar usuário admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

      
    }
}